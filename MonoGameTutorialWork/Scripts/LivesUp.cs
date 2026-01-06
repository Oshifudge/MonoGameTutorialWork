using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Content;
using MonoGameTutorialWork.Scripts;
using System;

namespace MonoGameTutorialWork.Scripts
{
    class LivesUp
    {
        public int count = 5;
        private Rectangle spriteDimensions;
        private Texture2D sprite;
        public Vector2 currentPos;

        private int animFrameIndex;
        private double currentFrameTime;
        private double frameTimeLimit;


        public LivesUp(Vector2 position, Rectangle spriteRectangle, Texture2D livesUpTexture)
        {
            currentPos = position;
            spriteDimensions = spriteRectangle;
            sprite = livesUpTexture;
            animFrameIndex = 1;
            currentFrameTime = 0.0f;
            frameTimeLimit = 0.8f;
        }

        public void Draw(double deltaTime, SpriteBatch spriteBatch, Texture2D livesUpTexture)
        {
            currentFrameTime += deltaTime;
            if (currentFrameTime > frameTimeLimit)
            {
                animFrameIndex++;
                if (animFrameIndex > 2)
                    animFrameIndex = 1;
                currentFrameTime = 0.0f;
            }
            if (animFrameIndex == 1)
                spriteBatch.Draw(livesUpTexture, currentPos, new Rectangle(0, 0, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);
            else if (animFrameIndex == 2)
            {
                spriteBatch.Draw(livesUpTexture, currentPos, new Rectangle(spriteDimensions.Width, 0, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);
            }
        }

        public Vector2 GetCurrentPos()
        {
            return currentPos;
        }

        public Rectangle GetSpriteDimensions()
        {
            return spriteDimensions;
        }
    }
}
