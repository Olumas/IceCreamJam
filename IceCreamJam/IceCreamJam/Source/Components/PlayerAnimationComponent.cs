using Nez;
using Microsoft.Xna.Framework;
using Nez.Textures;
using System.Collections.Generic;
using IceCreamJam.Source.Content;
using Nez.Sprites;

namespace IceCreamJam.Source.Components {
	class PlayerAnimationComponent : Component {

		private List<Sprite> sprites;
		SpriteAnimator animator;
		PlayerDirection direction;

		public float idleFPS = 4;

		public override void OnAddedToEntity() {
			this.animator = Entity.GetComponent<SpriteAnimator>();
			this.sprites = LoadTruckSprites();
			SetupAnimations();

			this.direction = Entity.GetComponent<PlayerDirection>();
			direction.OnDirectionChange += this.Direction_OnDirectionChange;
			Direction_OnDirectionChange(direction.Direction);
		}

		private void Direction_OnDirectionChange(Direction8 newDir) {
			animator.Play("dir" + (int)newDir);
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
				animator.AddAnimation("dir" + i, idleFPS, sprites[i * 2], sprites[i * 2 + 1]);
			}
		}
	}
}
