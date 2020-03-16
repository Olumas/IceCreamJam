using IceCreamJam.Source.Content;
using Microsoft.Xna.Framework;

namespace IceCreamJam.Source.WeaponSystem.Projectiles {
    class TestProjectile2 : Projectile {

        public TestProjectile2(Vector2 direction) : base(direction) {
            this.cost = 1;
            this.damage = 1;
            this.texturePath = ContentPaths.TestProjectile2;
        }

        public override Vector2 CalculateVector() {
            return direction * 3;
        }
    }
}
