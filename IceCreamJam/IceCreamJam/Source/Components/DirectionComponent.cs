﻿using Microsoft.Xna.Framework;
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
}