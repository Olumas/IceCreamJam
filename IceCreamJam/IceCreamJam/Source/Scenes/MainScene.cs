using IceCreamJam.Source.Entities;
using Nez;
using Microsoft.Xna.Framework;
using IceCreamJam.Source.Content;

namespace IceCreamJam.Source.Scenes {
    class MainScene : Scene {

        TilemapLoader loader;
        Entity truck;

        public override void Initialize() {
            base.Initialize();

            loader = AddSceneComponent(new TilemapLoader());
        }

        public override void OnStart() {
            base.OnStart();

            truck = AddEntity(new Truck() { Position = new Vector2(Screen.Width / 2, Screen.Height / 2) } );
            loader.Load(ContentPaths.Test1);
        }

        public override void Update() {
            base.Update();

            Camera.Position += (truck.Position - Camera.Position)/10;
        }
    }
}
