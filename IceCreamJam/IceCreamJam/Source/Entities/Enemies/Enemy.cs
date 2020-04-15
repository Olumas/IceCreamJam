using IceCreamJam.Source.Components;
using Nez;

namespace IceCreamJam.Source.Entities.Enemies {
    abstract class Enemy : NPC {

        public RenderableComponent renderer;
        public static Truck truck;
        protected Mover mover;

        public float health;

        public Enemy() {
            
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();
            AddComponent(new HomingTargetComponent());
            this.mover = AddComponent(new Mover());
        }

        public override void Update() {
            base.Update();

            if(truck == null)
                truck = (Truck)Scene.FindEntity("Truck");
        }

        public virtual void Damage(float damage) {
            this.health -= damage;

            if(this.health <= 0)
                Destroy();
        }
    }
}
