using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTutorialWork.Scripts;

namespace MonoGameTutorialWork.Scripts
{
    internal class Obstacle
    {
        public int count = 5;
        private Rectangle spriteDimensions;
        private Texture2D sprite;
        private Vector2 currentPos;


        public Obstacle(Vector2 position, Rectangle spriteRectangle)
        {
            currentPos = position;
            spriteDimensions = spriteRectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, currentPos, new Rectangle(spriteDimensions.X, spriteDimensions.Y, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);

        }

        public void LoadContent(ContentManager cM, string spriteSheetName)
        {
            sprite = cM.Load<Texture2D>(spriteSheetName);
        }
    }

}
