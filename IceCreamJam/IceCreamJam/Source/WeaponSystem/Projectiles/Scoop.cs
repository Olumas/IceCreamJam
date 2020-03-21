using IceCreamJam.Source.Content;
using Microsoft.Xna.Framework;
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
