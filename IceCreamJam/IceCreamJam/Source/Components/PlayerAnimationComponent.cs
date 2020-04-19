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
		public float dashFPS = 18;

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
			List<Sprite> sprites = new List<Sprite>();
			Nez.Systems.NezContentManager content = Entity.Scene.Content;
			sprites.AddRange(Sprite.SpritesFromAtlas(content.LoadTexture(ContentPaths.Truck + "truck.png"), 64, 64));
			sprites.AddRange(Sprite.SpritesFromAtlas(content.LoadTexture(ContentPaths.Truck + "truckdash1.png"), 64, 64));
			sprites.AddRange(Sprite.SpritesFromAtlas(content.LoadTexture(ContentPaths.Truck + "truckdash2.png"), 64, 64));
			sprites.AddRange(Sprite.SpritesFromAtlas(content.LoadTexture(ContentPaths.Truck + "truckdash3.png"), 64, 64));
			return sprites;
		}

		private void SetupAnimations() {
			for (int i = 0; i < 8; i++) {
				Animator.AddAnimation("dir" + i, idleFPS, sprites[i * 2], sprites[i * 2 + 1]);
				Animator.AddAnimation("dash" + i, dashFPS, /*sprites[i * 2],*/ sprites[16 + i], sprites[24 + i], sprites[32 + i]);
			}
		}

		public override void OnEnabled() {
			Direction_OnDirectionChange(direction.Direction);
		}
	}
}
