using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace IceCreamJam.Source.Entities {
    class NPC : Entity {
        private readonly string texturePath;
        public NPC(string texturePath) {
            this.texturePath = texturePath;
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            var texture = Scene.Content.LoadTexture(texturePath);
            var sprites = Sprite.SpritesFromAtlas(texture, 81, 60);

            var animator = new SpriteAnimator();
            animator.AddAnimation("Walk", 6, sprites.ToArray());
            animator.RenderLayer = Constants.Layer_NPC;
            animator.Play("Walk");

            AddComponent(animator);
        }
    }
}
