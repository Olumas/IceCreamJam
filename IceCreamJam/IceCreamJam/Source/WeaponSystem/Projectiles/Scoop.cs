using IceCreamJam.Source.Content;
using IceCreamJam.Source.Entities;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace IceCreamJam.Source.WeaponSystem.Projectiles {
    class Scoop : Projectile {

        private ScoopType type;

        // Custom constructor, must override InstantiateProjectile on this weapon!
        public Scoop(Vector2 direction, ScoopType type) : base(direction) {
            this.cost = 1;
            this.damage = 1;
            this.speed = 3;
            this.type = type;
        }

        public override void SetupTextures() {
            var animator = AddComponent(new SpriteAnimator() {
                RenderLayer = Constants.Layer_Bullets
            });

            var texture = Scene.Content.LoadTexture(ContentPaths.Scoop + $"Scoop_{(char)type}.png");
            var sprites = Sprite.SpritesFromAtlas(texture, 20, 9, 0);

            animator.AddAnimation("Fly", Constants.GlobalFPS, sprites.ToArray()).Play("Fly");
            this.renderer = animator;
        }

        public override Vector2 CalculateVector() {
            return direction * speed;
        }

        public override void OnHit(CollisionResult result) {
            var hitFX = Scene.AddEntity(new AnimatedEntity());

            hitFX.Position = this.Position;
            hitFX.Rotation = Random.NextAngle();

            var texture = Scene.Content.LoadTexture(ContentPaths.Scoop_Splat);
            var sprites = Sprite.SpritesFromAtlas(texture, 25, 25);

            hitFX.animator.AddAnimation("hit", Constants.GlobalFPS * 2,
                sprites.GetRange(TypeIndex(type) * 6, 6).ToArray()
            );

            hitFX.animator.Play("hit", SpriteAnimator.LoopMode.ClampForever);
            hitFX.animator.OnAnimationCompletedEvent += (s) => hitFX.Destroy();

            // Destroy the parent scoop
            base.OnHit(result);
        }

        public static ScoopType GetNext(ScoopType t) {
            if(t == (ScoopType)'C') return (ScoopType)'V';
            if(t == (ScoopType)'V') return (ScoopType)'S';
            return (ScoopType)'C';
        }

        public static int TypeIndex(ScoopType t) {
            if(t == (ScoopType)'C') return 0;
            if(t == (ScoopType)'V') return 1;
            return 2;
        }

        public enum ScoopType {
            Chocolate = 'C',
            Vanilla = 'V',
            Strawberry = 'S'
        }
    }
}
