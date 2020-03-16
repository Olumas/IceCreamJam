using IceCreamJam.Source.Entities;
using Nez;
using Microsoft.Xna.Framework;

namespace IceCreamJam.Source.Scenes {
    class MainScene : Scene {

        public override void Initialize() {
            base.Initialize();
        }

        public override void OnStart() {
            base.OnStart();

            var truck = AddEntity(new Truck()).Position = new Vector2(Screen.Width / 2, Screen.Height/2);
        }
    }
}
