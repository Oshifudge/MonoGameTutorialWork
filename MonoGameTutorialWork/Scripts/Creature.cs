using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;

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

        protected bool isJumping;
        protected int maxHeight;
        protected int currentHeight;

        public Creature(Vector2 position, Levels current, Rectangle spriteRectangle)
        {
            currentPos = position;
            initialPos = currentPos;
            currentLevel = current;
            spriteDimensions = spriteRectangle;
            isJumping = false;
            currentHeight = 0;
            maxHeight = 150;
        }

        public virtual void Up(int inputSpeed)
        {
            if (!currentLevel.IsWall((int)currentPos.X, (int)currentPos.Y) &&
                !currentLevel.IsWall((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y))
            {
                currentPos.Y -= inputSpeed;
            }
            else
            {
                if (isJumping)
                    isJumping = false;
            }
            
            //levelScript.IsWall(currentPos.X, currentPos.Y);
        }

        public virtual void Down(int inputSpeed)
        {
            bool onFloor = currentLevel.IsWall((int)currentPos.X, (int)currentPos.Y + (spriteDimensions.Height - 1) + inputSpeed) ||
                currentLevel.IsWall((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y + spriteDimensions.Height - 1);
            bool onPlatform = currentLevel.IsPlatform((int)currentPos.X, (int)currentPos.Y + (spriteDimensions.Height - 1) + inputSpeed) ||
                currentLevel.IsPlatform((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y + spriteDimensions.Height - 1);
            bool sameRow = currentLevel.IsInSameRow((int)currentPos.Y + (spriteDimensions.Height - 1), (int)currentPos.Y + (spriteDimensions.Height - 1) + inputSpeed);

            if  (!onFloor && !(onPlatform && !sameRow))
            {
                currentPos.Y += inputSpeed;
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

        public void SetIsJumping()
        {
            bool onFloor = currentLevel.IsWall((int)currentPos.X, (int)currentPos.Y + (spriteDimensions.Height)) ||
                currentLevel.IsWall((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y + spriteDimensions.Height - 1);
            bool onPlatform = currentLevel.IsPlatform((int)currentPos.X, (int)currentPos.Y + (spriteDimensions.Height)) ||
                currentLevel.IsPlatform((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y + spriteDimensions.Height - 1);
            bool sameRow = currentLevel.IsInSameRow((int)currentPos.Y + (spriteDimensions.Height - 1), (int)currentPos.Y + (spriteDimensions.Height - 1) + 1);

            if(onFloor || (onPlatform & !sameRow))
            {
                isJumping = true;
                currentHeight = 0;
            }
        }

        public void JumpOrFall(int inputSpeed)
        {
            if(isJumping)
            {
                Up(2);
                currentHeight += inputSpeed;

                if(currentHeight >= maxHeight)
                    isJumping = false;
            }
            else
            {
                Down(2);
                currentHeight -= inputSpeed;
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

        protected Vector2 getSpriteFrameDimensions()
        {
            return new Vector2(Sprite.Width, Sprite.Height);
        }

        public bool CollidesWith(Creature creature)
        {
            if (currentPos.X <= creature.currentPos.X + spriteDimensions.Width - 1 &&
                currentPos.X + spriteDimensions.Width - 1 >= creature.currentPos.X &&
                currentPos.Y <= creature.currentPos.Y + spriteDimensions.Height - 1 &&
                currentPos.Y + spriteDimensions.Height - 1 >= creature.currentPos.Y)
                return true;
            else
                return false;
        }
    }
}
