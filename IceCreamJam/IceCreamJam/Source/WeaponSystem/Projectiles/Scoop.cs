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
            var animator = AddComponent(new SpriteAnimator());
            animator.AddAnimation("Fly", Constants.GlobalFPS,
                new Sprite(Scene.Content.LoadTexture(ContentPaths.Scoop + "Scoop_0" + (char)this.type + ".png")),
                new Sprite(Scene.Content.LoadTexture(ContentPaths.Scoop + "Scoop_1" + (char)this.type + ".png"))
            );

            animator.Play("Fly", SpriteAnimator.LoopMode.Loop);

            this.renderer = animator;
        }

        public override Vector2 CalculateVector() {
            return direction * speed;
        }

        public override void OnHit() {
            base.OnHit();

            var hitFX = Scene.AddEntity(new AnimatedEntity());

            hitFX.Position = this.Position;
            var dir = -direction;
            dir.Normalize();
            hitFX.Rotation = Mathf.Atan2(dir.Y, dir.X);

            hitFX.animator.AddAnimation("hit", Constants.GlobalFPS,
                new Sprite(Scene.Content.LoadTexture(ContentPaths.Sprites + "Empty.png")),
                new Sprite(Scene.Content.LoadTexture(ContentPaths.Scoop + $"Scoop_FX_1{(char)type}.png")),
                new Sprite(Scene.Content.LoadTexture(ContentPaths.Scoop + $"Scoop_FX_2{(char)type}.png"))
            ); ;

            hitFX.animator.Play("hit", SpriteAnimator.LoopMode.Once);
            hitFX.animator.OnAnimationCompletedEvent += (s) => hitFX.Destroy();
        }

        public static ScoopType GetNext(ScoopType t) {
            if(t == (ScoopType)'C') return (ScoopType)'V';
            if(t == (ScoopType)'V') return (ScoopType)'S';
            return (ScoopType)'C';
        }

        public enum ScoopType {
            Chocolate = 'C',
            Vanilla = 'V',
            Strawberry = 'S'
        }
    }
}
