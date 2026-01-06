using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTutorialWork.Scripts
{
    internal class GameOver
    {
        private double timeLimit;
        private double totalTime;

        public void Draw(GraphicsDevice graphicsDevice)
        {
            //make red
            graphicsDevice.Clear(Color.PaleVioletRed);
        }

        public GameOver()
        {
            //set numbers
            totalTime = 0.0f;
            timeLimit = 10.0f;
        }

        public e_gameStates Update(double deltaTime)
        {
            totalTime += deltaTime;

            if (totalTime >= timeLimit)
            {
                totalTime = 0.0f;
                return e_gameStates.MENU;
            }

            return e_gameStates.GAMEOVER;
        }
    }
}
