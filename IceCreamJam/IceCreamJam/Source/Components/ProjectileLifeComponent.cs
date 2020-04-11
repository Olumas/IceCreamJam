using IceCreamJam.Source.WeaponSystem;
using Nez;

namespace IceCreamJam.Source.Components {
    class ProjectileLifeComponent : Component, IUpdatable {
        public float lifetime;
        public float startTime;
        public bool isFinished = true;
        public float progress;
        
        public ProjectileLifeComponent(float lifetime) {
            this.lifetime = lifetime;
            Start();
        }

        public void Start() {
            startTime = Time.TotalTime;
            isFinished = false;
        }

        public void Update() {
            if(isFinished)
                return;

            var remainingTime = (startTime + lifetime);
            progress = remainingTime / lifetime;

            if(remainingTime - Time.TotalTime < 0.01f) {
                (Entity as Projectile).OnHit(null);
                isFinished = true;
                progress = 1;
            }
        }
    }
}
