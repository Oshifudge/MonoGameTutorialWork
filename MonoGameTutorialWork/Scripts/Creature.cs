using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MonoGameTutorialWork.Scripts
{
    class Creature
    {
        protected Vector2 initialPos;
        protected Vector2 currentPos;
        protected Texture2D Sprite;

        protected Rectangle spriteDimensions;
        protected Levels currentLevel;
        protected float moveSpeed = 1;

        public Creature(Vector2 position, Levels current, Rectangle spriteRectangle)
        {
            currentPos = position;
            initialPos = currentPos;
            currentLevel = current;
            spriteDimensions = spriteRectangle;
        }

        public virtual void Up()
        {
            if (!currentLevel.IsWall((int)currentPos.X,(int)currentPos.Y) && 
                !currentLevel.IsWall((int)currentPos.X+spriteDimensions.Width-1,(int)currentPos.Y))
            {
                currentPos.Y -= moveSpeed;
            }
            
            //levelScript.IsWall(currentPos.X, currentPos.Y);
        }

        public virtual void Down()
        {
            if (!currentLevel.IsWall((int)currentPos.X, (int)currentPos.Y + spriteDimensions.Height - 1) &&
                !currentLevel.IsWall((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y + spriteDimensions.Height - 1))
            {
                currentPos.Y += moveSpeed;
            }
        }

        public virtual void Left()
        {
            if (!currentLevel.IsWall((int)currentPos.X, (int)currentPos.Y) &&
                !currentLevel.IsWall((int)currentPos.X, (int)currentPos.Y + spriteDimensions.Height - 1))
            {
                currentPos.X -= moveSpeed;
            }
        }

        public virtual void Right()
        {
            if (!currentLevel.IsWall((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y) &&
                !currentLevel.IsWall((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y + spriteDimensions.Height - 1))
            {
                currentPos.X += moveSpeed;
            }
        }

        public Vector2 GetCurrentPos()
        {
            return currentPos;
        }

        public void ResetCurrentPos()
        {
            currentPos = initialPos;
        }

        public void LoadContent(ContentManager cm, string name)
        {
            Sprite = cm.Load<Texture2D>(name);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(Sprite, currentPos, rect, Color.White);
        }



    }
}
