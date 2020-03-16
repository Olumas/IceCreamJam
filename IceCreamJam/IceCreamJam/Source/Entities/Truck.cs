using IceCreamJam.Source.Content;
using Nez;
using Nez.Sprites;

namespace IceCreamJam.Source.Entities {
    class Truck : Entity {
        public override void OnAddedToScene() {
            base.OnAddedToScene();

            var texture = Scene.Content.LoadTexture(ContentPaths.TruckSprite);
            AddComponent(new SpriteRenderer(texture));
        }


    }
}
