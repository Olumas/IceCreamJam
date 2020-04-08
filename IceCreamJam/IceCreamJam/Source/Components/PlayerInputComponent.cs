using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;

namespace IceCreamJam.Source.Components {
	class PlayerInputComponent : Component, IUpdatable {
		DirectionComponent direction;

		public override void OnAddedToEntity() {
			direction = Entity.GetComponent<DirectionComponent>();
		}

		public void Update() {
			bool? isNorth = InputManager.yAxis.Value == 0 ? null : (bool?)(InputManager.yAxis.Value < 0);
			bool? isWest = InputManager.xAxis.Value == 0 ? null : (bool?)(InputManager.xAxis.Value < 0);
			var newDir = NewDirection(isNorth, isWest);
			if (newDir.HasValue && direction.Direction != newDir.Value) {
				direction.Direction = newDir.Value;
			}
		}

		private Direction8? NewDirection(bool? isNorth, bool? isWest) {
			if (!isNorth.HasValue && !isWest.HasValue) return null;
			if (isNorth.HasValue) {
				if (isNorth.Value) {
					if (isWest.HasValue) {
						return isWest.Value ? (Direction8?)Direction8.NorthWest : (Direction8?)Direction8.NorthEast;
					} else {
						return Direction8.North;
					}
				} else {
					if (isWest.HasValue) {
						return isWest.Value ? (Direction8?)Direction8.SouthWest : (Direction8?)Direction8.SouthEast;
					} else {
						return Direction8.South;
					}
				}
			} else {
				return isWest.Value ? (Direction8?)Direction8.West : (Direction8?)Direction8.East;
			}
		}
	}
}
