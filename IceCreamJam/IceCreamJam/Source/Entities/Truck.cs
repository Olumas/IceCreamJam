using IceCreamJam.Source.Components;
using IceCreamJam.Source.WeaponSystem;
using IceCreamJam.Source.WeaponSystem.Weapons;
using Nez;
using Nez.Sprites;

namespace IceCreamJam.Source.Entities {
	class Truck : Entity {

		public override void OnAddedToScene() {
			base.OnAddedToScene();

			this.Name = "Truck";

			AddComponent(new PlayerDirection());

			var animator = AddComponent(new SpriteAnimator());
			AddComponent(new PlayerAnimationComponent());

			animator.RenderLayer = Constants.Layer_Truck;
			animator.LayerDepth = 1;

			//var collider = AddComponent(new BoxCollider());
			var collider = AddComponent(new PolygonCollider());
			collider.PhysicsLayer = (int)Constants.PhysicsLayers.Player;
			collider.CollidesWithLayers = (int)Constants.PhysicsLayers.Buildings;

			AddComponent(new ArcadeRigidbody() { ShouldUseGravity = false, Elasticity = 0 });
			AddComponent(new PlayerMovementComponent());

			AddComponent(new WeaponComponent(new ScoopGun(), new TestWeapon()));
		}
	}
}
