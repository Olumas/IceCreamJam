using IceCreamJam.Source.Components;
using IceCreamJam.Source.WeaponSystem;
using Nez;

namespace IceCreamJam.Source.Systems {
    class ProjectileLifeSystem : EntityProcessingSystem {
        public ProjectileLifeSystem(Matcher matcher) : base(matcher) { }

        public override void Process(Entity entity) {


            var projectile = (entity as Projectile);
            var lifespan = projectile.GetComponent<ProjectileLifeComponent>();

            if(lifespan.isFinished)
                return;

            if((lifespan.startTime + lifespan.lifetime) - Time.TotalTime < 0.01f) {
                projectile.OnHit(null);
                lifespan.isFinished = true;
            }


        }
    }
}
