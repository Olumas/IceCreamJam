using IceCreamJam.Source.Content;
using Microsoft.Xna.Framework;
using Nez;

namespace IceCreamJam.Source.WeaponSystem.EnemyWeapons {
    class DoctorKnife : EnemyProjectile {
        public DoctorKnife() {
            this.Name = "DoctorKnife";
        }

        public override void Initialize(Vector2 direction, Vector2 position) {
            base.Initialize(direction, position);

            this.speed = 2;
            this.lifetime = 1;
            this.texturePath = ContentPaths.Doctor + "DocKnife.png";
        }

        public override void OnHit(CollisionResult? result) {
            base.OnHit(result);
            Pool<DoctorKnife>.Free(this);
        }
    }
}
