using IceCreamJam.Source.Components;
using IceCreamJam.Source.Content;
using IceCreamJam.Source.WeaponSystem;
using IceCreamJam.Source.WeaponSystem.Weapons;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System.Collections.Generic;

namespace IceCreamJam.Source.Entities {
    class Truck : Entity {

        public WeaponComponent weapons;

        private ArcadeRigidbody rb;
        private PlayerMovementComponent moveComponent;

        private SpriteAnimator animator;
        private List<Sprite> sprites;

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            this.Name = "Truck";

            LoadSprites();
            this.animator = AddComponent(new SpriteAnimator(sprites[0]));

            var collider = AddComponent(new BoxCollider());
            collider.PhysicsLayer = (int)Constants.PhysicsLayers.Player;
            collider.CollidesWithLayers = (int)Constants.PhysicsLayers.Buildings;

            this.rb = AddComponent(new ArcadeRigidbody() { ShouldUseGravity = false, Elasticity = 0 });
			this.moveComponent = AddComponent(new PlayerMovementComponent());

            weapons = AddComponent(new WeaponComponent(new TestWeapon(), new ScoopGun()));
        }

        private void LoadSprites() {
            sprites = new List<Sprite>();

            for(int i = 0; i < 8; i++) {
                sprites.Add(new Sprite(Scene.Content.LoadTexture(ContentPaths.Truck + i + "a.png")));
            }
        }

        public override void Update() {
            base.Update();

            var deg = -moveComponent.rotationDegrees % 360;
            int spriteIndex = (int)(deg / 45);
            spriteIndex = Utility.Mod(spriteIndex, 8);
            animator.SetSprite(sprites[spriteIndex]);

        }
    }
}
