using IceCreamJam.Source.Components;
using Nez;

namespace IceCreamJam.Source.Entities.Enemies {
    abstract class Enemy : Entity {

        public RenderableComponent renderer;

        public Enemy() {
            
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();
            AddComponent(new HomingTargetComponent());
        }
    }
}
