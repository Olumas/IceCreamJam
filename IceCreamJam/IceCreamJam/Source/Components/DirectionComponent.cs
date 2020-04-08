using Microsoft.Xna.Framework;
using Nez;
using System;

namespace IceCreamJam.Source.Components {
	class DirectionComponent : Component {
		private Direction8 direction;

		internal Direction8 Direction {
			get => this.direction; set {
				if (this.direction != value) {
					OnDirectionChange?.Invoke(value);
				}
				this.direction = value;
			}
		}

		public event Action<Direction8> OnDirectionChange;	
	}

	public enum Direction8 {
		East, SouthEast, South, SouthWest, West, NorthWest, North, NorthEast,
	}

	public static class Direction8Ext {
		public static Vector2 ToVector2(this Direction8 d) {
			switch (d) {
				case Direction8.East:
					return new Vector2(1, 0);
				case Direction8.SouthEast:
					return new Vector2(1, 1);
				case Direction8.South:
					return new Vector2(0, 1);
				case Direction8.SouthWest:
					return new Vector2(-1, 1);
				case Direction8.West:
					return new Vector2(-1, 0);
				case Direction8.NorthWest:
					return new Vector2(-1, -1);
				case Direction8.North:
					return new Vector2(0, -1);
				case Direction8.NorthEast:
					return new Vector2(1, -1);
				default:
					throw new ArgumentOutOfRangeException(nameof(d));
			}
		}
	}
}
