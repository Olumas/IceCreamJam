using IceCreamJam.Source.WeaponSystem.Projectiles;

namespace IceCreamJam.Source.WeaponSystem.Weapons {
    class TestWeapon : Weapon {
        public TestWeapon() {
            this.projectileType = typeof(TestProjectile);
            this.name = "TestWeapon";
            this.reloadTime = 0.1f;
        }

        public override void InitializeRenderer() { }
    }
}
