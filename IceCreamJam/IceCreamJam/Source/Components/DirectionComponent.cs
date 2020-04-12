using Microsoft.Xna.Framework;
using Nez;
using System;

namespace IceCreamJam.Source.Components {
	class DirectionComponent : Component {
		[Inspectable]
		private Direction8 direction;

		[Inspectable]
		internal Direction8 PreviousDirection { get; private set; }
		internal Direction8 Direction {
			get => this.direction; set {
				if (this.direction != value) {
					OnDirectionChange?.Invoke(value);
				}
				this.PreviousDirection = direction;
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

		public static Direction8 RotateCW(this Direction8 d, int increments) {
			return (Direction8)Utility.Mod((int)d + increments, 8);
		}

		public static int Difference(this Direction8 d, Direction8 o) {
			var num = Utility.Mod(o - d, 8);
			if (num > 4) num -= 8;
			return num;
		}
	}
}
