using IceCreamJam.Source.Scenes.Entities;
using Nez;

namespace IceCreamJam.Source.Scenes {
    class MainScene : Scene {

        public override void Initialize() {
            base.Initialize();
        }

        public override void OnStart() {
            base.OnStart();

            AddEntity(new Truck());
        }
    }
}
