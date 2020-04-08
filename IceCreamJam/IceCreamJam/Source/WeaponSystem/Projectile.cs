using IceCreamJam.Source.Components;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace IceCreamJam.Source.WeaponSystem {
    abstract class Projectile : Entity, IPoolable {
        public float cost;
        public float damage;
        public string texturePath;
        public float speed;
        public float lifetime;

        public Vector2 direction;

        protected Mover moveComponent;
        public RenderableComponent renderer;
        public ProjectileLifeComponent lifeComponent;

        public bool IsNewProjectile = true;

        public Projectile() {}

        public virtual void Initialize(Vector2 direction, Vector2 position, float lifetime) {
            Initialize(direction, position);
            this.lifetime = lifetime;
        }

        public virtual void Initialize(Vector2 direction, Vector2 position) {
            this.direction = direction;
            this.Position = position;
            this.Rotation = Mathf.Atan2(direction.Y, direction.X);

            // Set this projectile to be re-used
            if(!IsNewProjectile) {
                this.SetEnabled(true);
                lifeComponent.Start();
            }

        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            SetupTextures();
            var b = AddComponent(new CircleCollider(4));
            b.LocalOffset = new Vector2(5, 0);
            b.PhysicsLayer = (int)Constants.PhysicsLayers.PlayerProjectiles;
            b.CollidesWithLayers = (int)(Constants.PhysicsLayers.Buildings | Constants.PhysicsLayers.NPC);

            if(lifetime != 0f) {
                this.lifeComponent = AddComponent(new ProjectileLifeComponent(lifetime));
                lifeComponent.Start();
            }

            this.moveComponent = AddComponent(new Mover());
            this.IsNewProjectile = false;
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
        public abstract void OnHit(CollisionResult? result);
            // Pool<T>.Free(this); 
            // ^^ Every projectile must have this!

        public void Reset() {
            // All important fields should be reset in Initialize,
            // when the projectile is called from the pool and given a new 
            // position and direction.

            // Disable the entity to stop rendering and colliding
            this.SetEnabled(false);
        }
    }
}
