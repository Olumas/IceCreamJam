using IceCreamJam.Source.Components;
using IceCreamJam.Source.Content;
using IceCreamJam.Source.WeaponSystem;
using IceCreamJam.Source.WeaponSystem.Weapons;
using Nez;
using Nez.Sprites;

namespace IceCreamJam.Source.Entities {
    class Truck : Entity {

        public WeaponComponent weapons;

        public override void OnAddedToScene() {
            base.OnAddedToScene();

			var texture = Scene.Content.LoadTexture(ContentPaths.TruckSprite);
			AddComponent(new SpriteRenderer(texture));
			AddComponent(new BoxCollider());
			AddComponent(new ArcadeRigidbody() { ShouldUseGravity = false });
			AddComponent(new PlayerMovementComponent());

            weapons = AddComponent(new WeaponComponent(new TestWeapon(), new TestWeapon2()));
        }
    }
}
