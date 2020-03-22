using Microsoft.Xna.Framework;
using Nez;

namespace IceCreamJam.Source.Components {
	class PlayerMovementComponent : Component, IUpdatable {
		Collider collider;
		ArcadeRigidbody rb;
		PlayerDirection direction;

		public float acceleration = 40f;
		public float deceleration = 30f;

		public float moveSpeed = 80f;
		public float maxSpeed = 200f;

		public float targetSpeed = 0f;
		public Vector2 currentDirectionVector = new Vector2(1, 0);

		public override void OnAddedToEntity() {
			collider = Entity.GetComponent<Collider>();
			rb = Entity.GetComponent<ArcadeRigidbody>();
			direction = Entity.GetComponent<PlayerDirection>();
		}

		public void Update() {
			bool? isNorth = InputManager.yAxis.Value == 0 ? null : (bool?)(InputManager.yAxis.Value < 0);
			bool? isWest = InputManager.xAxis.Value == 0 ? null : (bool?)(InputManager.xAxis.Value < 0);
			var newDir = NewDirection(isNorth, isWest);
			if (newDir.HasValue && direction.Direction != newDir.Value) {
				direction.Direction = newDir.Value;
				currentDirectionVector = new Vector2(InputManager.xAxis.Value, InputManager.yAxis.Value);
				currentDirectionVector.Normalize();
			}

			if (newDir.HasValue) {
				targetSpeed += acceleration * Time.DeltaTime;
				if (targetSpeed >= maxSpeed) targetSpeed = maxSpeed;
			} else {
				if (targetSpeed > 0) targetSpeed -= deceleration * Time.DeltaTime;
				if (targetSpeed <= 0) targetSpeed = 0;
			}

			Vector2 targetVelocity = currentDirectionVector * targetSpeed;
			rb.Velocity = targetVelocity;

			Vector2 targetMovement = currentDirectionVector * targetSpeed * Time.DeltaTime;
			if (collider.CollidesWithAny(ref targetMovement, out CollisionResult result)) {
				if (result.Collider.PhysicsLayer.IsFlagSet((int)Constants.PhysicsLayers.Buildings)) {
					targetSpeed = 0;
				}
			}
		}

		private Direction8? NewDirection(bool? isNorth, bool? isWest) {
			if (!isNorth.HasValue && !isWest.HasValue) return null;
			if (isNorth.HasValue) {
				if (isNorth.Value) {
					if (isWest.HasValue) {
						if (isWest.Value) return Direction8.NorthWest;
						else return Direction8.NorthEast;
					} else {
						return Direction8.North;
					}
				} else {
					if (isWest.HasValue) {
						if (isWest.Value) return Direction8.SouthWest;
						else return Direction8.SouthEast;
					} else {
						return Direction8.South;
					}
				}
			} else {
				if (isWest.Value) return Direction8.West;
				else return Direction8.East;
			}
		}

		public override void DebugRender(Batcher batcher) {
			batcher.DrawLineAngle(Transform.Position, Transform.Rotation, 10, Color.Red);
		}
	}
}
