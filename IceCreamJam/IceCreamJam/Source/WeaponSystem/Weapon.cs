using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;
using System.Collections;

namespace IceCreamJam.Source.WeaponSystem {
    abstract class Weapon : Entity {

        public Type projectileType;
        public string name;
        public float reloadTime;
        public bool canShoot = true;
        public string texturePath;
        public Vector2 weaponMountOffset;

        public bool defaultVisible = false;
        private ICoroutine reloadCoroutine;
        public Component weaponComponent;
        public RenderableComponent renderer;

        public Weapon() {
            projectileType = typeof(Projectile);
        }

        public virtual void OnEquipped() {
            SetVisible(true);
        }
        public virtual void OnUnequipped() {
            SetVisible(false);
        }

        public void SetVisible(bool visible) {
            if(renderer != null)
                renderer.SetEnabled(visible);
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            InitializeRenderer();
            SetVisible(defaultVisible);
        }

        public virtual void InitializeRenderer() {
            var texture = Scene.Content.LoadTexture(texturePath);
            this.renderer = new SpriteRenderer(texture) {
                RenderLayer = Constants.Layer_Weapon,
                LayerDepth = 1f
            };

            AddComponent(renderer);
        }

        public virtual void Shoot() {
            if(canShoot) {
                canShoot = false;
                reloadCoroutine = Core.StartCoroutine(ReloadTimer());

                // Instantiate a projectile using the weapon's projectile type
                var p = InstantiateProjectile(this.Position);
                weaponComponent.Entity.Scene.AddEntity(p);
            }
        }

        public virtual Projectile InstantiateProjectile(Vector2 pos) {
            var scene = weaponComponent.Entity.Scene;
            var dir = Vector2.Normalize(scene.Camera.MouseToWorldPoint() - (weaponComponent.Entity.Position + weaponMountOffset));

            var p = (Projectile)Activator.CreateInstance(projectileType, dir);
            p.Position = pos;
            return p;
        }

        IEnumerator ReloadTimer() {
            yield return Coroutine.WaitForSeconds(reloadTime);
            canShoot = true;
        }
    }
}
