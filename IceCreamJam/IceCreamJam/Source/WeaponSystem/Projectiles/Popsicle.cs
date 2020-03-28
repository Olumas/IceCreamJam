using IceCreamJam.Source.Content;
using IceCreamJam.Source.Entities;
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

        public override void OnHit(CollisionResult result) {
            base.OnHit(result);

            var hitFX = Scene.AddEntity(new AnimatedEntity());
            var dir = CalculateVector();
            hitFX.Rotation = Mathf.Atan2(dir.Y, dir.X);
            hitFX.Position = this.Position;

            var texture = Scene.Content.LoadTexture(ContentPaths.Popsicle_Shatter);
            var sprites = Sprite.SpritesFromAtlas(texture, 32, 23);

            hitFX.animator.RenderLayer = Constants.Layer_Bullets;
            hitFX.animator.LayerDepth = 0.4f;

            hitFX.animator.AddAnimation("hit", Constants.GlobalFPS * 2, sprites.ToArray());
            hitFX.animator.Play("hit", SpriteAnimator.LoopMode.Once);

            var stickTexture = Scene.Content.LoadTexture(ContentPaths.Popsicle_Stick);
            var stick = Scene.AddEntity(new SpriteEntity(stickTexture, Constants.Layer_Bullets, 0.5f) {
                Rotation = Mathf.Atan2(dir.Y, dir.X),
                Position = this.Position - dir * 2,
            });

            hitFX.animator.OnAnimationCompletedEvent += (s) => {
                hitFX.Destroy();
                stick.Destroy();
            };

            // Destroy the parent popsicle
            base.OnHit(result);
        }

        public override void DebugRender(Batcher batcher) {
            base.DebugRender(batcher);

            if(target != null)
			    batcher.DrawLineAngle(Transform.Position, Mathf.AngleBetweenVectors(this.Position, target.Position), 10, Color.Red);

            batcher.DrawCircle(this.Position, 100f, Color.Red);
        }
    }
}
