using IceCreamJam.Source.Components;
using IceCreamJam.Source.Content;
using IceCreamJam.Source.WeaponSystem;
using IceCreamJam.Source.WeaponSystem.Weapons;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System.IO;

namespace IceCreamJam.Source.Entities {
	class Truck : Entity {

		public override void OnAddedToScene() {
			this.Name = "Truck";

			var dir = AddComponent(new PlayerDirection());

			var animator = AddComponent(new SpriteAnimator());
			AddComponent(new PlayerAnimationComponent());

			animator.RenderLayer = Constants.Layer_Truck;
			animator.LayerDepth = 1;

			//var collider = AddComponent(new BoxCollider());
			var collider = AddComponent(new PolygonCollider());
			collider.PhysicsLayer = (int)Constants.PhysicsLayers.Player;
			collider.CollidesWithLayers = (int)Constants.PhysicsLayers.Buildings;
			
			var colliderManager = AddComponent(new ColliderManager(ContentPaths.Content + "truckcollision.json"));
			dir.OnDirectionChange += i => colliderManager.SetIndex((int)i);

			AddComponent(new ArcadeRigidbody() { ShouldUseGravity = false, Elasticity = 0 });
			AddComponent(new PlayerMovementComponent());

			AddComponent(new WeaponComponent(new ScoopGun(), new PopsicleGun()));
		}
	}
}
