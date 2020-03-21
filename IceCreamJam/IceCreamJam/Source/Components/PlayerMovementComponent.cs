using Microsoft.Xna.Framework;
using Nez;

namespace IceCreamJam.Source.Components {
	class PlayerMovementComponent : Component, IUpdatable {
		ArcadeRigidbody rb;
		PlayerDirection direction;
		
		private const float acceleration = 1000;
		private const float deceleration = 600;

		public const float dragConstant = 0.00010f;
		public const float idleFriction = 0.0007f;
		public const float turnFriction = 0.69f;

		public const float minimumSpeed = 50f;

		public override void OnAddedToEntity() {
			this.rb = Entity.GetComponent<ArcadeRigidbody>();
			direction = Entity.GetComponent<PlayerDirection>();
		}

		public void Update() {
			bool? isNorth = InputManager.drive.Value == 0 ? null : (bool?)(InputManager.drive.Value < 0);
			bool? isWest = InputManager.steer.Value == 0 ? null : (bool?)(InputManager.steer.Value < 0);
			//Debug.Log((isNorth.HasValue ? isNorth.Value+"" : "null") + " " + (isWest.HasValue ? isWest.Value+"" : "null"));
			var newDir = NewDirection(isNorth, isWest);
			if (newDir.HasValue) {
				direction.Direction = newDir.Value;
			}
		}

		private PlayerDirection.Direction8? NewDirection(bool? isNorth, bool? isWest) {
			if (!isNorth.HasValue && !isWest.HasValue) return null;
			if (isNorth.HasValue) {
				if (isNorth.Value) {
					if (isWest.HasValue) {
						if (isWest.Value) return PlayerDirection.Direction8.NorthWest;
						else return PlayerDirection.Direction8.NorthEast;
					} else {
						return PlayerDirection.Direction8.North;
					}
				} else {
					if (isWest.HasValue) {
						if (isWest.Value) return PlayerDirection.Direction8.SouthWest;
						else return PlayerDirection.Direction8.SouthEast;
					} else {
						return PlayerDirection.Direction8.South;
					}
				}
			} else {
				if (isWest.Value) return PlayerDirection.Direction8.West;
				else return PlayerDirection.Direction8.East;
			}
		}

		public override void DebugRender(Batcher batcher) {
			batcher.DrawLineAngle(Transform.Position, Transform.Rotation, 10, Color.Red);
		}
	}
}
