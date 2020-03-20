﻿using IceCreamJam.Source.Components;
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

            this.Name = "Truck";

			var texture = Scene.Content.LoadTexture(ContentPaths.TruckSprite);
			AddComponent(new SpriteRenderer(texture));
            var collider = AddComponent(new BoxCollider());
            collider.PhysicsLayer = (int)Constants.PhysicsLayers.Player;
            collider.CollidesWithLayers = (int)Constants.PhysicsLayers.Buildings;

            var rb = AddComponent(new ArcadeRigidbody() { ShouldUseGravity = false });
            rb.Elasticity = 0;

			AddComponent(new PlayerMovementComponent());

            weapons = AddComponent(new WeaponComponent(new TestWeapon(), new TestWeapon2()));
        }
    }
}
