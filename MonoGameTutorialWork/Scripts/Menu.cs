using System.Diagnostics.Eventing.Reader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTutorialWork.Scripts
{

    internal class Menu
    {
        Vector2 screenWH;

        public Menu(Vector2 dimensions)
        {
            screenWH = dimensions;
        }

        public e_gameStates Update(Game1 game)
        {
            if (Mouse.GetState().X >= 0 && Mouse.GetState().X <= screenWH.X - 1 &&
                Mouse.GetState().Y >= 0 && Mouse.GetState().Y <= screenWH.Y - 1 &&
                Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                game.Exit();
            }

            if (Mouse.GetState().X >= 0 && Mouse.GetState().X <= screenWH.X - 1 &&
                Mouse.GetState().Y >= 0 && Mouse.GetState().Y <= screenWH.Y - 1 &&
                Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                return e_gameStates.GAME;
            }
            else
                return e_gameStates.MENU;
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.DarkGreen);
        }

    }
}
