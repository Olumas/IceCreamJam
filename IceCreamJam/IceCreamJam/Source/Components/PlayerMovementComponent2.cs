using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;
using Microsoft.Xna.Framework;

namespace IceCreamJam.Source.Components {
	class PlayerMovementComponent2 : Component, IUpdatable {
		ArcadeRigidbody rb;
		public override void OnAddedToEntity() {
			this.rb = Entity.GetComponent<ArcadeRigidbody>();
		}

		public void Update() {
			if(InputManager.down)
		}
	}
}
