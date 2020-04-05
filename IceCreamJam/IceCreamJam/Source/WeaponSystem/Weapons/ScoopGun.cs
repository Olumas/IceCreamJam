using IceCreamJam.Source.Components;
using IceCreamJam.Source.Content;
using IceCreamJam.Source.Entities;
using IceCreamJam.Source.WeaponSystem.Projectiles;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Textures;
using System;

namespace IceCreamJam.Source.WeaponSystem.Weapons {
    class ScoopGun : Weapon {

        private Scoop.ScoopType type = Scoop.ScoopType.Chocolate;
        private SpriteEntity coneDecal;
        private EntitySpringComponent coneSpring;
        private AnimatedEntity shootFX;

        public ScoopGun() {
            this.projectileType = typeof(Scoop);
            this.name = "ScoopGun";
            this.reloadTime = 0.1f;
            this.texturePath = ContentPaths.Scoop_Base;
            this.weaponMountOffset = new Vector2(0, -5);
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();
            var coneTexture = Scene.Content.LoadTexture(ContentPaths.Scoop_Cone);
            coneDecal = Scene.AddEntity(new SpriteEntity(coneTexture, Constants.Layer_WeaponOver, 0.5f));
            this.coneSpring = coneDecal.AddComponent(new EntitySpringComponent(this, weaponMountOffset, 5));
            coneDecal.ToggleVisible(this.defaultVisible);

            shootFX = Scene.AddEntity(new AnimatedEntity());
            AddFXAnimation();
            shootFX.ToggleVisible(this.defaultVisible);
        }

        private void AddFXAnimation() {
            void AddAnimation(Scoop.ScoopType type) {
                var texture = Scene.Content.LoadTexture(ContentPaths.Scoop + $"Scoop_FX_{(char)type}.png");
                var sprites = Sprite.SpritesFromAtlas(texture, 8, 23);
                shootFX.animator.AddAnimation(Enum.GetName(typeof(Scoop.ScoopType), type), Constants.GlobalFPS, sprites.ToArray());
            }

            AddAnimation(Scoop.ScoopType.Chocolate);
            AddAnimation(Scoop.ScoopType.Vanilla);
            AddAnimation(Scoop.ScoopType.Strawberry);

            shootFX.animator.RenderLayer = Constants.Layer_WeaponOver;
            shootFX.animator.LayerDepth = 0.6f;
        }

        public override void OnEquipped() {
            base.OnEquipped();
            coneDecal.Position = coneSpring.TargetPosition;
            coneSpring.RelativePosition = Vector2.Zero;

            coneDecal.ToggleVisible(true);
            shootFX.ToggleVisible(true);
        }

        public override void OnUnequipped() {
            base.OnUnequipped();
            coneDecal.ToggleVisible(false);
            shootFX.ToggleVisible(false);
        }

        public override Projectile InstantiateProjectile(Vector2 pos) {
            var scene = weaponComponent.Entity.Scene;
            var dir = Vector2.Normalize(scene.Camera.MouseToWorldPoint() - coneSpring.TargetPosition);

            type = Scoop.GetNext(type);
            var s = Pool<Scoop>.Obtain();
            s.Initialize(dir, pos + this.weaponMountOffset + dir * 4, type);

            // Shock the cone
            coneSpring.Shock(-dir * 3);

            return s;
        }

        public override void Shoot() {
            base.Shoot();

            // Trigger shoot Fx
            shootFX.animator.Play(Enum.GetName(typeof(Scoop.ScoopType), type), Nez.Sprites.SpriteAnimator.LoopMode.ClampForever);
            shootFX.ToggleVisible(true);
        }

        public override void Update() {
            base.Update();

            // Update cone and Shoot fx
            var dir = Vector2.Normalize(Scene.Camera.MouseToWorldPoint() - coneSpring.TargetPosition);
            coneDecal.Rotation = shootFX.Rotation = Mathf.Atan2(dir.Y, dir.X);

            // Offset fx from cone
            shootFX.Position = coneSpring.TargetPosition + coneSpring.RelativePosition + dir * 8;
        }
    }
}
