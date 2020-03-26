using IceCreamJam.Source.Content;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace IceCreamJam.Source.Entities.Enemies {
    class Doctor : Enemy {
        public Doctor() {

        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();
            SetupAnimations();

            var b = AddComponent(new BoxCollider());
            b.PhysicsLayer = (int)Constants.PhysicsLayers.NPC;
        }

        private void SetupAnimations() {
            var animator = new SpriteAnimator();

            var runTexture = Scene.Content.LoadTexture(ContentPaths.Doctor + "DocRun.png");
            var runSprites = Sprite.SpritesFromAtlas(runTexture, 81, 60);

            animator.AddAnimation("Run", runSprites.ToArray());

            var attackTexture = Scene.Content.LoadTexture(ContentPaths.Doctor + "DocAttack.png");
            var attackSprites = Sprite.SpritesFromAtlas(attackTexture, 54, 30);

            for(int i = 0; i < 3; i++)
                animator.AddAnimation($"Attack{i}", attackSprites.GetRange(i * 7, 7).ToArray());

            this.renderer = animator;
            AddComponent(animator);
            animator.Play("Attack1");
        }
    }
}
