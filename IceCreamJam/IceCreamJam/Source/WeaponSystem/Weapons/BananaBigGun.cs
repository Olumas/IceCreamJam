using IceCreamJam.Source.Content;
using IceCreamJam.Source.WeaponSystem.Projectiles;
using Microsoft.Xna.Framework;
using Nez;

namespace IceCreamJam.Source.WeaponSystem.Weapons {
    class BananaBigGun : Weapon {

        public BananaBigGun() {
            this.projectileType = typeof(BananaBig);
            this.name = "BananaBigGun";
            this.reloadTime = 0.5f;
            this.texturePath = ContentPaths.Banana_Base_Big;
        }

        public override void InitializeRenderer() {
            base.InitializeRenderer();
            this.renderer.LocalOffset = new Vector2(0, -5);
        }

        public override Projectile InstantiateProjectile(Vector2 pos) {
            var scene = weaponComponent.Entity.Scene;
            var dir = Vector2.Normalize(scene.Camera.MouseToWorldPoint() - weaponComponent.Entity.Position);

            var b = Pool<BananaBig>.Obtain();
            b.Initialize(dir, pos);

            return b;
        }
    }
}
