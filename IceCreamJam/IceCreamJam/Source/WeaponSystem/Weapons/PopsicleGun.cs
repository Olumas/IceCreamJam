using IceCreamJam.Source.Content;
using IceCreamJam.Source.Entities;
using IceCreamJam.Source.WeaponSystem.Projectiles;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Textures;

namespace IceCreamJam.Source.WeaponSystem.Weapons {
    class PopsicleGun : Weapon {

        private AnimatedEntity shatterFX;
        private SpriteEntity loadedPopsicle;

        public PopsicleGun() {
            this.projectileType = typeof(Popsicle);
            this.name = "PopsicleGun";
            this.reloadTime = 1f;
            this.texturePath = ContentPaths.Popsicle_Base;
            this.weaponMountOffset = new Vector2(0, -5);
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            var popsicleTexture = Scene.Content.LoadTexture(ContentPaths.Popsicle + "Popsicle_Basic.png");
            this.loadedPopsicle = Scene.AddEntity(new SpriteEntity(popsicleTexture, Constants.Layer_Weapon, 0.5f));
            this.loadedPopsicle.ToggleVisible(this.defaultVisible);

            this.shatterFX = Scene.AddEntity(new AnimatedEntity());
            AddFXAnimation();
            shatterFX.ToggleVisible(this.defaultVisible);
            shatterFX.animator.RenderLayer = Constants.Layer_Weapon;
            shatterFX.animator.LayerDepth = 0.4f;
            shatterFX.animator.OnAnimationCompletedEvent += (s) => LoadNewPopsicle();
        }

        private void AddFXAnimation() {
            var texture = Scene.Content.LoadTexture(ContentPaths.Popsicle_Shatter);
            var sprites = Sprite.SpritesFromAtlas(texture, 26, 23);
            sprites.Reverse();
            shatterFX.animator.AddAnimation("Shatter", Constants.GlobalFPS * 4, sprites.ToArray());
        }

        public override Projectile InstantiateProjectile(Vector2 pos) {
            var scene = weaponComponent.Entity.Scene;
            var dir = Vector2.Normalize(scene.Camera.MouseToWorldPoint() - (this.Position));

            return new Popsicle(dir) { Position = this.Position };
        }

        public override void OnUnequipped() {
            base.OnUnequipped();
            shatterFX.ToggleVisible(false);
            loadedPopsicle.ToggleVisible(false);
        }

        public override void Shoot() {
            base.Shoot();

            loadedPopsicle.ToggleVisible(false);

            shatterFX.ToggleVisible(true);
            shatterFX.animator.Play("Shatter", Nez.Sprites.SpriteAnimator.LoopMode.Once);
        }

        private void LoadNewPopsicle() {
            loadedPopsicle.ToggleVisible(true);
            shatterFX.ToggleVisible(false);
        }

        public override void Update() {
            base.Update();
            var dir = Vector2.Normalize(Scene.Camera.MouseToWorldPoint() - this.Position);
            shatterFX.Rotation = loadedPopsicle.Rotation = Mathf.Atan2(dir.Y, dir.X);
            shatterFX.Position = loadedPopsicle.Position = this.Position + this.weaponMountOffset; // (dir * 10);
        }
    }
}
