using IceCreamJam.Source.Content;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace IceCreamJam.Source.WeaponSystem.Projectiles {
    class Popsicle : HomingProjectile {

        public Popsicle(Vector2 direction) : base(direction) {
            this.speed = 2;
            this.targetHeading = direction;
        }
        
        public override void SetupTextures() {
            var animator = AddComponent(new SpriteAnimator() {
                RenderLayer = Constants.Layer_Bullets
            });

            var texture = Scene.Content.LoadTexture(ContentPaths.Popsicle + "Popsicle_Fly.png");
            var sprites = Sprite.SpritesFromAtlas(texture, 20, 9, 0);

            animator.AddAnimation("Fly", sprites.ToArray()).Play("Fly");
            this.renderer = animator;
        }

        public override void DebugRender(Batcher batcher) {
            base.DebugRender(batcher);

            if(target != null)
			    batcher.DrawLineAngle(Transform.Position, Mathf.AngleBetweenVectors(this.Position, target.Position), 10, Color.Red);

            batcher.DrawCircle(this.Position, 100f, Color.Red);
        }
    }
}
