using IceCreamJam.Source.Components;
using Nez;

namespace IceCreamJam.Source.Entities {
    class NPC : Entity {
        public override void OnAddedToScene() {
            base.OnAddedToScene();

            AddComponent(new NPCComponent());
        }
    }
}
