using Nez;
using Nez.Sprites;

namespace IceCreamJam.Source.Entities {
    class AnimatedEntity : Entity {
        public SpriteAnimator animator;
        public AnimatedEntity() {
            this.animator = AddComponent(new SpriteAnimator());
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();
        }

        public void ToggleVisible(bool visible) {
            animator.Enabled = visible;
        }
    }
}
