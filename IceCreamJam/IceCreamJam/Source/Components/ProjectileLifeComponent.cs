using Nez;

namespace IceCreamJam.Source.Components {
    class ProjectileLifeComponent : Component {
        public float lifetime;
        public float startTime;
        public bool isFinished = true;
        
        public ProjectileLifeComponent(float lifetime) {
            this.lifetime = lifetime;
            Start();
        }

        public void Start() {
            startTime = Time.TotalTime;
            isFinished = false;
        }
    }
}
