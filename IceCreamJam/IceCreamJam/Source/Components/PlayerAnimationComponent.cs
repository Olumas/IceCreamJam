using IceCreamJam.Source.Content;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System.Collections.Generic;

namespace IceCreamJam.Source.Components {
	class PlayerAnimationComponent : Component {

		private List<Sprite> sprites;
		public SpriteAnimator Animator { get; private set; }
		private DirectionComponent direction;

		public float idleFPS = 4;

		public override void OnAddedToEntity() {
			this.Animator = Entity.GetComponent<SpriteAnimator>();
			this.sprites = LoadTruckSprites();
			SetupAnimations();

			this.direction = Entity.GetComponent<DirectionComponent>();
			direction.OnDirectionChange += this.Direction_OnDirectionChange;
		}

		private void Direction_OnDirectionChange(Direction8 newDir) {
			Animator.Play("dir" + (int)newDir);
		}

		private List<Sprite> LoadTruckSprites() {
			return Sprite.SpritesFromAtlas(Entity.Scene.Content.LoadTexture(ContentPaths.Truck + "truck.png"), 64, 64);
		}

		private void SetupAnimations() {
			for (int i = 0; i < 8; i++) {
				Animator.AddAnimation("dir" + i, idleFPS, sprites[i * 2], sprites[i * 2 + 1]);
			}
		}

		public override void OnEnabled() {
			Direction_OnDirectionChange(direction.Direction);
		}
	}
}
