using IceCreamJam.Source.WeaponSystem.Projectiles;
using Microsoft.Xna.Framework;
using Nez;

namespace IceCreamJam.Source.WeaponSystem.Weapons {
    class BananaBigGun : Weapon {

        public BananaBigGun() {
            this.projectileType = typeof(BananaBig);
            this.name = "BananaBigGun";
            this.reloadTime = 0.5f;
        }

        public override Projectile InstantiateProjectile(Vector2 pos) {
            var scene = weaponComponent.Entity.Scene;
            var dir = Vector2.Normalize(scene.Camera.MouseToWorldPoint() - (weaponComponent.Entity.Position + weaponMountOffset));

            var b = Pool<BananaBig>.Obtain();
            b.Initialize(dir, pos + this.weaponMountOffset + dir * 4);

            return b;
        }

        public override void InitializeRenderer() { }
    }
}
