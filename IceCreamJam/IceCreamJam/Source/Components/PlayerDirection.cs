using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;

namespace IceCreamJam.Source.Components {
	class PlayerDirection : Component {
		private Direction8 direction = Direction8.West;

		internal Direction8 Direction {
			get => this.direction; set {
				if (this.direction != value) {
					OnDirectionChange?.Invoke(value);
				}
				this.direction = value;
			}
		}

		public event Action<Direction8> OnDirectionChange;
		public enum Direction8 {
			East, SouthEast, South, SouthWest, West, NorthWest, North, NorthEast, 
		}
	}
}
