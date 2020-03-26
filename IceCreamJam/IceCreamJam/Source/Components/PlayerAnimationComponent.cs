using IceCreamJam.Source.Content;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System.Collections.Generic;

namespace IceCreamJam.Source.Components {
	class PlayerAnimationComponent : Component {

		private List<Sprite> sprites;
		public SpriteAnimator Animator { get; private set; }
		private PlayerDirection direction;

		public float idleFPS = 4;

		public override void OnAddedToEntity() {
			this.Animator = Entity.GetComponent<SpriteAnimator>();
			this.sprites = LoadTruckSprites();
			SetupAnimations();

			this.direction = Entity.GetComponent<PlayerDirection>();
			direction.OnDirectionChange += this.Direction_OnDirectionChange;
		}

		private void Direction_OnDirectionChange(Direction8 newDir) {
			Animator.Play("dir" + (int)newDir);
		}

		private List<Sprite> LoadTruckSprites() {
			var sprites = new List<Sprite>();
			for (int i = 0; i < 8; i++) {
				sprites.Add(new Sprite(Entity.Scene.Content.LoadTexture(ContentPaths.Truck + i + "a.png")));
				sprites.Add(new Sprite(Entity.Scene.Content.LoadTexture(ContentPaths.Truck + i + "b.png")));
			}
			return sprites;
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
