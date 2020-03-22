﻿using Nez;
using Nez.Sprites;

namespace IceCreamJam.Source.Entities {
    class SpriteEntity : Entity {
        private readonly string texturePath;
        public SpriteRenderer renderer;
        public bool defaultVisible = true;

        public SpriteEntity(string texturePath) {
            this.texturePath = texturePath;
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            var texture = Scene.Content.LoadTexture(texturePath);
            this.renderer = AddComponent(new SpriteRenderer(texture));

            ToggleVisible(defaultVisible);
        }

        public void ToggleVisible(bool visible) {
            renderer.Enabled = visible;
        }
    }
}