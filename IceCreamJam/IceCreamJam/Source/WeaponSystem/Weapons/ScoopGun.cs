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
            this.weaponMountOffset = new Vector2(0, -3);
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();
            coneDecal = Scene.AddEntity(new SpriteEntity(ContentPaths.Scoop_Cone));
            this.coneSpring = coneDecal.AddComponent(new EntitySpringComponent(this, weaponMountOffset, 5));
            coneDecal.defaultVisible = false;

            shootFX = Scene.AddEntity(new AnimatedEntity());
            AddFXAnimation();
            shootFX.ToggleVisible(false);
        }

        private void AddFXAnimation() {
            void AddAnimation(Scoop.ScoopType type) {
                Sprite[] sprites = new Sprite[4];

                for(int i = 0; i < 3; i++)
                    sprites[i] = new Sprite(Scene.Content.LoadTexture(ContentPaths.Scoop + $"Scoop_FX_{i}{(char)type}.png"));
                sprites[3] = new Sprite(Scene.Content.LoadTexture(ContentPaths.Scoop + $"Scoop_FX_{0}{(char)Scoop.GetNext(type)}.png"));

                shootFX.animator.AddAnimation(Enum.GetName(typeof(Scoop.ScoopType), type), Constants.GlobalFPS, sprites);
            }

            AddAnimation(Scoop.ScoopType.Chocolate);
            AddAnimation(Scoop.ScoopType.Vanilla);
            AddAnimation(Scoop.ScoopType.Strawberry);
        }

        public override void OnEquipped() {
            base.OnEquipped();
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
            var dir = Vector2.Normalize(scene.Camera.MouseToWorldPoint() - (coneSpring.TargetPosition));

            type = Scoop.GetNext(type);
            var s = new Scoop(dir, type);
            s.Position = pos + dir * 4; // Line up scoop with cone

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
            shootFX.Position = coneDecal.Position + dir * 16;
        }
    }
}
