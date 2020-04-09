using Microsoft.Xna.Framework;
using Nez;

namespace IceCreamJam.Source.Components {
	class PlayerMovementComponent : Component, IUpdatable {
		Collider collider;
		ArcadeRigidbody rb;
		DirectionComponent direction;

		public float acceleration = 40f;
		public float deceleration = 30f;

		public float moveSpeed = 80f;
		public float maxSpeed = 200f;

		public float targetSpeed = 0f;
		public Vector2 currentDirectionVector = new Vector2(1, 0);

		public override void OnAddedToEntity() {
			collider = Entity.GetComponent<Collider>();
			rb = Entity.GetComponent<ArcadeRigidbody>();
			direction = Entity.GetComponent<DirectionComponent>();

			direction.OnDirectionChange += this.Direction_OnDirectionChange;
		}

		private void Direction_OnDirectionChange(Direction8 obj) {
			currentDirectionVector = obj.ToVector2();
		}

		public void Update() {
			if (InputManager.xAxis != 0 || InputManager.yAxis != 0) {
				targetSpeed += acceleration * Time.DeltaTime;
				if (targetSpeed >= maxSpeed) targetSpeed = maxSpeed;
			} else {
				if (targetSpeed > 0) targetSpeed -= deceleration * Time.DeltaTime;
				if (targetSpeed <= 0) targetSpeed = 0;
			}

			Vector2 targetVelocity = currentDirectionVector.Normalized() * targetSpeed;
			rb.Velocity = targetVelocity;

			Vector2 targetMovement = targetVelocity * Time.DeltaTime;
			if (collider.CollidesWithAny(ref targetMovement, out CollisionResult result)) {
				if (result.Collider.PhysicsLayer.IsFlagSet((int)Constants.PhysicsLayers.Buildings)) {
					targetSpeed = 0;
				}
			}
		}

		public override void DebugRender(Batcher batcher) {
			batcher.DrawLineAngle(Transform.Position, Transform.Rotation, 10, Color.Red);
		}
	}
}
