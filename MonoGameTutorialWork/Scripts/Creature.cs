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
        protected int currentLevel;
        protected Rectangle spriteDimensions;
        protected Levels levelScript;

        public Creature(Vector2 position, int currentLevel, Rectangle spriteDimensions)
        {
            currentPos = position;
            initialPos = currentPos;
            this.currentLevel = currentLevel;
            this.spriteDimensions = spriteDimensions;
        }

        public virtual void Up()
        {
            currentPos.Y += 1.0f;
            GetCurrentPos();
            levelScript.IsWall(currentPos.X, currentPos.Y);
        }

        public virtual void Down()
        {
            currentPos.Y -= 1.0f;
            GetCurrentPos();
            levelScript.IsWall(currentPos.X, currentPos.Y);

        }

        public virtual void Left()
        {
            currentPos.X -= 1.0f;
            GetCurrentPos();
            levelScript.IsWall(currentPos.X, currentPos.Y);

        }

        public virtual void Right()
        {
            currentPos.X += 1.0f;
            GetCurrentPos();
            levelScript.IsWall(currentPos.X, currentPos.Y);

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
