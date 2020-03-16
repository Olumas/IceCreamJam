using IceCreamJam.Source.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IceCreamJam {
    public class Game1 : Nez.Core {
        public Game1() {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();
            Scene = new MainScene();
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime) {
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
