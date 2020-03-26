using IceCreamJam.Source.Systems;
using Microsoft.Xna.Framework;
using Nez;

namespace IceCreamJam.Source.WeaponSystem {
    class HomingProjectile : Projectile {

        public float turnSpeed = 0.05f;
        public Entity target;
        public Vector2 targetHeading;
        public static HomingProjectileSystem homingSystem;

        public HomingProjectile(Vector2 direction) : base(direction) { }

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            if(homingSystem == null)
                homingSystem = Scene.GetEntityProcessor<HomingProjectileSystem>();

            homingSystem.projectiles.Add(this);
        }

        public override void OnHit() {
            base.OnHit();
            homingSystem.projectiles.Remove(this);
        }
        public override Vector2 CalculateVector() {
            return targetHeading * speed;
        }

        public override void Update() {
            base.Update();

            if(target == null)
                return;

            // Get angle towards target
            var targetAngle = Mathf.AngleBetweenVectors(this.Position, target.Position);
            float newAngle = Mathf.ApproachAngleRadians(this.Rotation, targetAngle, turnSpeed);

            // Calculate a new heading based upon the new angle
            var newHeading = new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
            newHeading.Normalize();
            targetHeading = newHeading;

            // Rotate projectile to face target
            this.Rotation = newAngle;
        }
    }
}
