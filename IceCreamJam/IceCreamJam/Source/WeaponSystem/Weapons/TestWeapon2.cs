using IceCreamJam.Source.WeaponSystem.Projectiles;

namespace IceCreamJam.Source.WeaponSystem.Weapons {
    class TestWeapon2 : Weapon {
        public TestWeapon2() {
            this.projectileType = typeof(TestProjectile2);
            this.name = "TestWeapon2";
            this.reloadTime = 0.5f;
        }

        public override void InitializeRenderer() { }
    }
}
