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
        private AnimatedEntity shootFX;

        private readonly Vector2 BaseOffset = new Vector2(0, -3);

        public ScoopGun() {
            this.projectileType = typeof(Scoop);
            this.name = "ConeShooter";
            this.reloadTime = 0.1f;
            this.texturePath = ContentPaths.Scoop_Base;
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();
            coneDecal = Scene.AddEntity(new SpriteEntity(ContentPaths.Scoop_Cone));
            shootFX = Scene.AddEntity(new AnimatedEntity());
            AddFXAnimation();
            shootFX.ToggleVisible(false);
        }

        private void AddFXAnimation() {
            void AddAnimation(Scoop.ScoopType type) {
                Sprite[] sprites = new Sprite[3];

                for(int i = 0; i < 3; i++)
                    sprites[i] = new Sprite(Scene.Content.LoadTexture(ContentPaths.Scoop + $"Scoop_FX_{i}{(char)type}.png"));

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

        public override Projectile InstantiateProjectile(Vector2 dir, Vector2 pos) {
            type = Scoop.GetNext(type);
            var s = new Scoop(dir, type);
            s.Position = pos + BaseOffset + dir * 4; // Line up scoop with cone
            return s;
        }

        public override void Shoot() {
            base.Shoot();

            // Trigger shoot Fx
            shootFX.animator.Play(Enum.GetName(typeof(Scoop.ScoopType), type), Nez.Sprites.SpriteAnimator.LoopMode.Once);
            shootFX.ToggleVisible(true);
        }

        public override void Update() {
            base.Update();

            var basePosition = this.Position + BaseOffset;
            // Update cone and Shoot fx
            var dir = Vector2.Normalize(Scene.Camera.MouseToWorldPoint() - weaponComponent.Entity.Position);
            coneDecal.Rotation = shootFX.Rotation = Mathf.Atan2(dir.Y, dir.X);
            coneDecal.Position = basePosition;

            shootFX.Position = basePosition + dir * 16;

            if(shootFX.animator.AnimationState == Nez.Sprites.SpriteAnimator.State.Completed)
                shootFX.ToggleVisible(false);
        }
    }
}
