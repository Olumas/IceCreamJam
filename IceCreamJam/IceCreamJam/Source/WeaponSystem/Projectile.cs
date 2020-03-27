using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace IceCreamJam.Source.WeaponSystem {
    abstract class Projectile : Entity {
        public float cost;
        public float damage;
        public string texturePath;
        public float speed;

        public Vector2 direction;

        protected Mover moveComponent;
        public RenderableComponent renderer;

        public Projectile(Vector2 direction) {
            this.direction = direction;

            var target = (Position + direction);
            Rotation = Mathf.Atan2(target.Y, target.X);
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            SetupTextures();
            var b = AddComponent(new CircleCollider(4));
            b.LocalOffset = new Vector2(5, 0);
            b.PhysicsLayer = (int)Constants.PhysicsLayers.PlayerProjectiles;
            b.CollidesWithLayers = (int)(Constants.PhysicsLayers.Buildings | Constants.PhysicsLayers.NPC);

            this.moveComponent = AddComponent(new Mover());
        }

        public virtual void SetupTextures() {
            var texture = Scene.Content.LoadTexture(texturePath);
            this.renderer = AddComponent(new SpriteRenderer(texture) {
                RenderLayer = Constants.Layer_Bullets
            });
        }

        /// <summary>
        /// Calculate path (straight, boomerang, homing, aoe, etc)
        /// </summary>
        /// <returns>The vector to move the projectile by</returns>
        public abstract Vector2 CalculateVector();

        public override void Update() {
            base.Update();
            Move();
        }

        protected void Move() {
            var vector = CalculateVector();
            moveComponent.Move(vector, out var collisionResult);

            if(collisionResult.Collider != null)
                OnHit(collisionResult);
        }

        /// <summary>
        /// Override this to instantiate sub-projectiles
        /// </summary>
        public virtual void OnHit(CollisionResult result) {
            this.Destroy();
        }
    }
}
