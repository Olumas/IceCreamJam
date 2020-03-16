using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace IceCreamJam.Source.Components {
	class PlayerMovementComponent : Component, IUpdatable {
		ArcadeRigidbody rb;
		
		private float acceleration = 400;
		private float deceleration = 100;

		/// <summary>
		/// Turn speed in degrees per second
		/// </summary>
		private float turnSpeed = 180;

		public float turnFriction = 0.999f;

		public float dragConstant = 0.00005f;
		public float idleFriction = 0.0007f;

		public float minimumSpeed = 50f;

		public override void OnAddedToEntity() {
			this.rb = Entity.GetComponent<ArcadeRigidbody>();
		}

		public void Update() {
			if (InputManager.steer.Value != 0) {
				float deltaDegrees = InputManager.steer.Value * turnSpeed * Time.DeltaTime;
				Entity.RotationDegrees += deltaDegrees;
				rb.Velocity = Mathf.RotateAround(rb.Velocity, Vector2.Zero, deltaDegrees) * turnFriction;
			}
			if (InputManager.drive.Value > 0) {
				rb.Velocity += Mathf.AngleToVector(Transform.LocalRotation, InputManager.drive.Value * acceleration * Time.DeltaTime);
			} else if (InputManager.drive.Value < 0) {
				rb.Velocity += Mathf.AngleToVector(Transform.LocalRotation, InputManager.drive.Value * deceleration * Time.DeltaTime);
			}

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
