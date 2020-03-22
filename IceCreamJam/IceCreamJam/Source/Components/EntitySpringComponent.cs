using Microsoft.Xna.Framework;
using Nez;

namespace IceCreamJam.Source.Components {
    /// <summary>
    /// Moves it's entity to follow a target
    /// </summary>
    class EntitySpringComponent : Component, IUpdatable {

        public Entity targetEntity;
        public Vector2 offset;
        public float tension;
        public Vector2 TargetPosition => targetEntity.Position + offset;

        public EntitySpringComponent(Entity target, float tension) : this(target, Vector2.Zero, tension) { }

        public EntitySpringComponent(Entity target, Vector2 offset, float tension) {
            this.targetEntity = target;
            this.offset = offset;
            this.tension = Mathf.Clamp(tension, 0.1f, float.MaxValue);
        }

        public override void OnAddedToEntity() {
            base.OnAddedToEntity();
            Entity.Position = TargetPosition;
        }

        public void Update() {
            Entity.Position += (TargetPosition - Entity.Position) / tension;
        }

        public void Shock(Vector2 force) {
            Entity.Position += force;
        }
    }
}
