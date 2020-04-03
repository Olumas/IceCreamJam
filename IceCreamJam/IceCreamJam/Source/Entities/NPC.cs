using IceCreamJam.Source.Components;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace IceCreamJam.Source.Entities {
    class NPC : Entity {
        private readonly string texturePath;
        private SpriteAnimator animator;
        private Mover mover;

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

            this.animator = AddComponent(animator);
            AddComponent(new NPCComponent());
            AddComponent(new HomingTargetComponent());
            this.mover = AddComponent(new Mover());
            AddComponent(new CircleCollider(10) { PhysicsLayer = (int)Constants.PhysicsLayers.NPC });
        }

        public void Move(Vector2 vector) {
            mover.Move(vector, out var result);
        }

        public void Flip(bool flip) {
            animator.FlipX = flip;
        }

        public void PauseWalk(bool pause) {
            if(pause)
                animator.Pause();
            else
                animator.UnPause();
        }
    }
}
