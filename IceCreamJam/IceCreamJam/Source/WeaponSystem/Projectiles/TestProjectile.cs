﻿using IceCreamJam.Source.Content;
using Microsoft.Xna.Framework;

namespace IceCreamJam.Source.WeaponSystem.Projectiles {
    class TestProjectile : Projectile {

        public TestProjectile(Vector2 direction) : base(direction) {
            this.cost = 1;
            this.damage = 1;
            this.speed = 5;
            this.texturePath = ContentPaths.TestProjectile;
        }

        public override Vector2 CalculateVector() {
            return direction * speed;
        }

        public override void OnHit() {
            Scene.AddEntity(new TestProjectile(-direction) { Position = this.Position });
            base.OnHit();
        }
    }
}
