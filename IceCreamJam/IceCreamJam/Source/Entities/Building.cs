using IceCreamJam.Source.Content;
using Nez;
using Nez.Sprites;

namespace IceCreamJam.Source.Entities {
    class Building : Entity {
        public override void OnAddedToScene() {
            base.OnAddedToScene();


            var texture = Scene.Content.LoadTexture(ContentPaths.TruckSprite);
            AddComponent(new SpriteRenderer(texture));
            AddComponent(new BoxCollider());
        }
    }
}
