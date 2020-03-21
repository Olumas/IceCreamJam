using Nez;
using Nez.Sprites;

namespace IceCreamJam.Source.Entities {
    class SpriteEntity : Entity {
        private string texturePath;
        private SpriteRenderer renderer;
        public SpriteEntity(string texturePath) {
            this.texturePath = texturePath;
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            var texture = Scene.Content.LoadTexture(texturePath);
            this.renderer = AddComponent(new SpriteRenderer(texture));
            renderer.LayerDepth = 0;
        }

        public void ToggleVisible(bool visible) {
            renderer.Enabled = visible;
        }
    }
}
