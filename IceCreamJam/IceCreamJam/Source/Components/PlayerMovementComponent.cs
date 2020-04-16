using Microsoft.Xna.Framework;
using Nez;

namespace IceCreamJam.Source.Components {
	class PlayerMovementComponent : Component, IUpdatable {
		Collider collider;
		ArcadeRigidbody rb;
		DirectionComponent direction;
		PlayerInputComponent playerInput;

		public float acceleration = 40f;
		public float coastDeceleration = 30f;
		public float brakeDeceleration = 60f;
		public float maxSpeed = 200f;

		// base seconds per increment
		public float turnTime = 0.125f;
		[Inspectable]
		private float turningTimer = 0;

		private Direction8 targetHeading;
		private Vector2 currentDirectionVector = new Vector2(1, 0);

		[Inspectable]
		private float speed = 0f;

		public Direction8 CurrentHeading {
			get => direction.Direction; set => direction.Direction = value;
		}

		public override void OnAddedToEntity() {
			collider = Entity.GetComponent<Collider>();
			rb = Entity.GetComponent<ArcadeRigidbody>();
			direction = Entity.GetComponent<DirectionComponent>();
			playerInput = Entity.GetComponent<PlayerInputComponent>();

			playerInput.OnInputStart += this.PlayerInput_OnInputStart; ;
			direction.OnDirectionChange += this.Direction_OnDirectionChange;
		}

		private void PlayerInput_OnInputStart(Direction8 obj) {
			targetHeading = obj;
		}

		private void Direction_OnDirectionChange(Direction8 obj) {
			currentDirectionVector = obj.ToVector2().Normalized();
		}

		public void Update() {
			if (InputManager.brake) {
				speed = Mathf.Approach(speed, 0, brakeDeceleration * Time.DeltaTime);
			} else if (playerInput.InputHeld && CurrentHeading == targetHeading) {
				speed = Mathf.Approach(speed, maxSpeed, acceleration * Time.DeltaTime);
			} else {
				speed = Mathf.Approach(speed, 0, coastDeceleration * Time.DeltaTime);
			}

			// facing different direction from input
			if (playerInput.InputHeld && CurrentHeading != targetHeading) {
				int difference = CurrentHeading.Difference(targetHeading);
				if (speed == 0) {
					CurrentHeading = CurrentHeading.Rotate(difference);
				} else {
					if (turningTimer >= turnTime) {
						CurrentHeading = CurrentHeading.Rotate(System.Math.Sign(difference));
						turningTimer -= turnTime;
					} else {
						turningTimer += Time.DeltaTime + (Time.DeltaTime * speed / maxSpeed);
					}
				}
			} else {
				turningTimer = 0;
			}

			Vector2 currentVelocity = currentDirectionVector * speed;
			rb.Velocity = currentVelocity;

			// TODO: remove hack to instantly stop movement when rammed into a building
			Vector2 movement = currentVelocity * Time.DeltaTime;
			if (collider.CollidesWithAny(ref movement, out CollisionResult result)) {
				if (result.Collider.PhysicsLayer.IsFlagSet((int)Constants.PhysicsLayers.Buildings)) {
					speed = 0;
				}
			}
		}

		public override void DebugRender(Batcher batcher) {
			batcher.DrawLineAngle(Transform.Position, Transform.Rotation, 10, Color.Red);
		}
	}
}
