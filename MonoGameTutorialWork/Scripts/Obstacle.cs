using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Content;
using MonoGameTutorialWork.Scripts;

namespace MonoGameTutorialWork.Scripts
{
    internal class Obstacle
    {
        public int count = 5;
        private Rectangle spriteDimensions;
        private Texture2D sprite;
        private Vector2 currentPos;

        private int animFrameIndex;
        private double currentFrameTime;
        private double frameTimeLimit;


        public Obstacle(Vector2 position, Rectangle spriteRectangle, Texture2D obstacleTexture)
        {
            currentPos = position;
            spriteDimensions = spriteRectangle;
            sprite = obstacleTexture;
            animFrameIndex = 0;
            currentFrameTime = 0.0f;
            frameTimeLimit = 0.4f;
        }

        public void Draw(double deltaTime, SpriteBatch spriteBatch, Texture2D obstacleTexture, Texture2D obstacleTexture2)
        {
            currentFrameTime += deltaTime;
            if (currentFrameTime > frameTimeLimit)
            {
                animFrameIndex++;
                if (animFrameIndex > 1)
                    animFrameIndex = 0;
                currentFrameTime = 0.0f;
            }
            if(animFrameIndex == 0)
                spriteBatch.Draw(obstacleTexture, currentPos, new Rectangle(spriteDimensions.X, spriteDimensions.Y, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);
            else if(animFrameIndex == 1)
                spriteBatch.Draw(obstacleTexture2, currentPos, new Rectangle(spriteDimensions.X, spriteDimensions.Y, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);

        }
    }

}
