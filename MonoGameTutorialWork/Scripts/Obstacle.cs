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


        public Obstacle(Vector2 position, Rectangle spriteRectangle, Texture2D obstacleTexture)
        {
            currentPos = position;
            spriteDimensions = spriteRectangle;
            sprite = obstacleTexture;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D obstacleTexture2)
        {
            spriteBatch.Draw(obstacleTexture2, currentPos, new Rectangle(spriteDimensions.X, spriteDimensions.Y, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);

        }

        //public void LoadContent(ContentManager cM, string spriteSheetName)
        //{
        //    sprite = cM.Load<Texture2D>(spriteSheetName);
        //}
    }

}
