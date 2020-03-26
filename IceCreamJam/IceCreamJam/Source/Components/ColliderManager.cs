using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;
using Microsoft.Xna.Framework;
using Nez.PhysicsShapes;
using IceCreamJam.Source.Content;
using Nez.Sprites;

namespace IceCreamJam.Source.Components {
	class ColliderManager : Component {
		PolygonCollider collider;

		public int ColliderIndex {
			get => this.colliderIndex; set {
				SetIndex(value);
			}
		}

		string file;
		int colliderIndex;
		List<Vector2[]> polygons;

		public ColliderManager(string file, int startingIndex = 0) {
			this.file = file;
			this.colliderIndex = startingIndex;
		}

		public ColliderManager(List<Shape> shape, int startingIndex = 0) {
			this.colliderIndex = startingIndex;
		}

		public ColliderManager(List<Shape> shapes, PolygonCollider collider, int startingIndex = 0) {
			this.colliderIndex = startingIndex;
			this.collider = collider;
		}

		public override void OnAddedToEntity() {
			polygons = Entity.Scene.Content.LoadPolygons(file);

			if (collider == null)
				this.collider = Entity.GetComponent<PolygonCollider>();
		}

		public override void OnEnabled() {
			SetIndex(colliderIndex);
		}

		public void SetIndex(int i) {
			colliderIndex = i;
			((Polygon)collider.Shape).SetPoints(polygons[colliderIndex]);
		}
	}
}
