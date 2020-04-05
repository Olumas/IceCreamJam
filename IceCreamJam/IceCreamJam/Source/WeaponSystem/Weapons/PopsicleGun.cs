using IceCreamJam.Source.Content;
using IceCreamJam.Source.Entities;
using IceCreamJam.Source.WeaponSystem.Projectiles;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Textures;
using System.Collections;

namespace IceCreamJam.Source.WeaponSystem.Weapons {
    class PopsicleGun : Weapon {

        private AnimatedEntity shatterFX;
        private SpriteEntity loadedPopsicle;
        private ICoroutine reformCoroutine;
        private float reformTime; // Time before the popsicle reforms 

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
            this.loadedPopsicle = Scene.AddEntity(new SpriteEntity(popsicleTexture, Constants.Layer_WeaponOver, 0.5f));
            this.loadedPopsicle.ToggleVisible(this.defaultVisible);

            this.shatterFX = Scene.AddEntity(new AnimatedEntity());
            AddFXAnimation();
            shatterFX.ToggleVisible(false);
            shatterFX.animator.RenderLayer = Constants.Layer_WeaponOver;
            shatterFX.animator.LayerDepth = 0.4f;
            shatterFX.animator.OnAnimationCompletedEvent += (s) => ReloadPopsicle();
        }

        private void AddFXAnimation() {
            var texture = Scene.Content.LoadTexture(ContentPaths.Popsicle_Shatter);
            var sprites = Sprite.SpritesFromAtlas(texture, 32, 23);

            sprites.Reverse();
            shatterFX.animator.AddAnimation("Reform", Constants.GlobalFPS * 4, sprites.ToArray());

            // Calculate delay before reforming popsicle

            //reformTime = reloadTime - (sprites.Count / (Constants.GlobalFPS * 4));
            reformTime = reloadTime - 0.15f; // Jank approximation that looks good enough
        }

        public override Projectile InstantiateProjectile(Vector2 pos) {
            var scene = weaponComponent.Entity.Scene;
            var dir = Vector2.Normalize(scene.Camera.MouseToWorldPoint() - (this.Position));

            var p = Pool<Popsicle>.Obtain();
            p.Initialize(dir, this.Position + this.weaponMountOffset);
            return p;
        }

        public override void OnUnequipped() {
            base.OnUnequipped();
            shatterFX.ToggleVisible(false);
            loadedPopsicle.ToggleVisible(false);
        }

        public override void OnShoot() {
            base.Shoot();

            reformCoroutine = Core.StartCoroutine(ReloadTimer());
        }

        IEnumerator ReloadTimer() {
            loadedPopsicle.ToggleVisible(false);
            yield return Coroutine.WaitForSeconds(reformTime);
            // Reform popsicle
            shatterFX.ToggleVisible(true);
            shatterFX.animator.Play("Reform", Nez.Sprites.SpriteAnimator.LoopMode.ClampForever);
            loadedPopsicle.ToggleVisible(false);
        }

        private void ReloadPopsicle() {
            shatterFX.ToggleVisible(false);
            loadedPopsicle.ToggleVisible(!InputManager.shoot.IsDown);
        }

        public override void Update() {
            base.Update();
            var dir = Vector2.Normalize(Scene.Camera.MouseToWorldPoint() - this.Position);
            shatterFX.Rotation = loadedPopsicle.Rotation = Mathf.Atan2(dir.Y, dir.X);
            shatterFX.Position = loadedPopsicle.Position = this.Position + this.weaponMountOffset;
        }
    }
}
