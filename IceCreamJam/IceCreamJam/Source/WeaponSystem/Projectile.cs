using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace IceCreamJam.Source.WeaponSystem {
    abstract class Projectile : Entity {
        public float cost;
        public float damage;
        public string texturePath;

        public Vector2 direction;

        protected Mover moveComponent;
        public SpriteRenderer renderer;

        public Projectile(Vector2 direction) {
            this.direction = direction;

            var target = (Position + direction);
            Rotation = Mathf.Atan2(target.Y, target.X);
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            var texture = Scene.Content.LoadTexture(texturePath);
            this.renderer = AddComponent(new SpriteRenderer(texture));
            this.moveComponent = AddComponent(new Mover());
        }

        // Override this to instantiate sub-projectiles
        public virtual void OnHit() {
            this.Destroy();
        }

        // Calculate path (straight, boomerang, homing, aoe, etc)
        public abstract Vector2 CalculateVector();

        public override void Update() {
            base.Update();
            Move();
        }

        protected void Move() {
            var vector = CalculateVector();
            moveComponent.Move(vector, out var collisionResult);

            // TODO: Check if other is enemy using collisionResult
            // If so, call OnHit();
        }
    }
}
