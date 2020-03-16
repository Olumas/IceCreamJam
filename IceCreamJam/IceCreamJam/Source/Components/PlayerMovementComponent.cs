using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace IceCreamJam.Source.Components {
	class PlayerMovementComponent : Component, IUpdatable {
		ArcadeRigidbody rb;

		private VirtualAxis forward;
		private VirtualAxis steer;

		private VirtualJoystick joystick;
		
		private float acceleration = 400;
		private float deceleration = 100;

		/// <summary>
		/// Turn speed in degrees per second
		/// </summary>
		private float turnSpeed = 180;

		public float turnFriction = 0.999f;

		public float dragConstant = 0.00005f;
		public float idleFriction = 0.001f;

		public float minimumSpeed = 40f;

		public override void Initialize() {
			SetupVirtualInput();
		}

		public override void OnAddedToEntity() {
			this.rb = Entity.GetComponent<ArcadeRigidbody>();
		}

		public void Update() {
			if (joystick.Value.X != 0) {
				float deltaDegrees = joystick.Value.X * turnSpeed * Time.DeltaTime;
				Entity.RotationDegrees += deltaDegrees;
				rb.Velocity = Mathf.RotateAround(rb.Velocity, Vector2.Zero, deltaDegrees) * turnFriction;
			}
			if (joystick.Value.Y < 0) {
				rb.Velocity += Mathf.AngleToVector(Transform.LocalRotation, -joystick.Value.Y * acceleration * Time.DeltaTime);
			} else if (joystick.Value.Y > 0) {
				rb.Velocity += Mathf.AngleToVector(Transform.LocalRotation, -joystick.Value.Y * deceleration * Time.DeltaTime);
			}

			rb.Velocity -= dragConstant * rb.Velocity * rb.Velocity.Length();
			rb.Velocity -= idleFriction * rb.Velocity;
			if (joystick.Value.Y == 0) {
				rb.Velocity -= idleFriction * rb.Velocity;
				if (rb.Velocity.LengthSquared() <= minimumSpeed * minimumSpeed) {
					rb.Velocity = Vector2.Zero;
				}
			}
			

			Debug.Log(rb.Velocity);
		}

		private void SetupVirtualInput() {
			joystick = new VirtualJoystick(false);
			joystick.AddKeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.A, Keys.D, Keys.W, Keys.S);
			joystick.AddGamePadLeftStick();
		}

		public override void DebugRender(Batcher batcher) {
			batcher.DrawLineAngle(Transform.Position, Transform.Rotation, 10, Color.Red);
		}
	}
}
