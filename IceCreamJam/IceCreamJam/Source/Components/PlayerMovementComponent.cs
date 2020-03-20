using Microsoft.Xna.Framework;
using Nez;

namespace IceCreamJam.Source.Components {
	class PlayerMovementComponent : Component, IUpdatable {
		ArcadeRigidbody rb;
		
		private const float acceleration = 1000;
		private const float deceleration = 600;

		/// <summary>
		/// Turn speed in degrees per second
		/// </summary>
		private const float turnSpeed = 50;
		private float angularVelocity = 0;

		public const float dragConstant = 0.00010f;
		public const float idleFriction = 0.0007f;
		public const float turnFriction = 0.69f;

		public const float minimumSpeed = 50f;
		bool withDirection;

		public override void OnAddedToEntity() {
			this.rb = Entity.GetComponent<ArcadeRigidbody>();
		}

		public void Update() {
			if (InputManager.steer.Value != 0) {
				angularVelocity += turnSpeed * InputManager.steer.Value * Time.DeltaTime;
			}
			//if (InputManager.up.IsPressed) {
			//	withDirection = rb.Velocity.
			//}
			if (InputManager.drive.Value < 0) {
				rb.Velocity += Mathf.AngleToVector(Transform.Rotation, InputManager.drive.Value * acceleration * Time.DeltaTime);
			} else if (InputManager.drive.Value > 0) {
				rb.Velocity += Mathf.AngleToVector(Transform.Rotation, InputManager.drive.Value * deceleration * Time.DeltaTime);
			}

			// Apply angular velocity
			angularVelocity *= turnFriction;
			var terminal = 312f; // Approximate max speed
			var deltaDegrees = angularVelocity * (rb.Velocity.Length() / terminal);
			Entity.RotationDegrees += deltaDegrees;
			rb.Velocity = Mathf.RotateAround(rb.Velocity, Vector2.Zero, deltaDegrees);

			//Debug.Log((rb.Velocity.Length() / terminal));
			Debug.Log(InputManager.steer.Value);

			rb.Velocity -= dragConstant * rb.Velocity * rb.Velocity.Length();
			rb.Velocity -= idleFriction * rb.Velocity;
			if (InputManager.drive.Value == 0) {
				rb.Velocity -= idleFriction * rb.Velocity;
				if (rb.Velocity.LengthSquared() <= minimumSpeed * minimumSpeed) {
					rb.Velocity = Vector2.Zero;
				}
			}
		}

		public override void DebugRender(Batcher batcher) {
			batcher.DrawLineAngle(Transform.Position, Transform.Rotation, 10, Color.Red);
		}
	}
}
