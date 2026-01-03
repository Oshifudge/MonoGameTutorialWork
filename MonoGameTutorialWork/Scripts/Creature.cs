using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;
using System;
using System.Net.Sockets;

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

        protected bool canScroll;

        public enum animState
        {
            LEFT, RIGHT, UP, DOWN, IDLE
        }

        protected int animFrameIndex;
        protected double currentFrameTime;
        protected double frameTimeLimit;
        protected animState currentAnimState;

        public Creature(Vector2 position, Levels current, Rectangle spriteRectangle)
        {
            currentPos = position;
            initialPos = currentPos;
            currentLevel = current;
            spriteDimensions = spriteRectangle;
            isJumping = false;
            currentHeight = 0;
            maxHeight = 150;
            animFrameIndex = 0;
            currentFrameTime = 0.0f;
            frameTimeLimit = 0.4f;
            currentAnimState = animState.IDLE;
            canScroll = false;
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
                currentLevel.IsWall((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y + (spriteDimensions.Height - 1) + moveSpeed);
            bool onPlatform = currentLevel.IsPlatform((int)currentPos.X, (int)currentPos.Y + (spriteDimensions.Height - 1) + inputSpeed) ||
                currentLevel.IsPlatform((int)currentPos.X + (spriteDimensions.Width - 1), (int)currentPos.Y + (spriteDimensions.Height - 1) + inputSpeed);
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
                canScroll = true;
            }
            else
            {
                canScroll = false;
            }
        }

        public virtual void Right()
        {
            if (!currentLevel.IsWall((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y) &&
                !currentLevel.IsWall((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y + spriteDimensions.Height - 1))
            {
                currentPos.X += moveSpeed;
                canScroll = true;
            }
            else
            {
                canScroll = false;
            }
        }

        public void SetIsJumping()
        {
            bool onFloor = currentLevel.IsWall((int)currentPos.X, (int)currentPos.Y + (spriteDimensions.Height) + moveSpeed) ||
                currentLevel.IsWall((int)currentPos.X + (spriteDimensions.Width - 1), (int)currentPos.Y + (spriteDimensions.Height - 1) +moveSpeed);
            bool onPlatform = currentLevel.IsPlatform((int)currentPos.X, (int)currentPos.Y + (spriteDimensions.Height) + moveSpeed) ||
                currentLevel.IsPlatform((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y + (spriteDimensions.Height - 1) + moveSpeed);
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            switch(currentAnimState)
            {
                case animState.IDLE:
                    //new Rectangle(spriteDimensions.X + animFrameIndex * spriteDimensions.Width, spriteDimensions.Y + spriteDimensions.Height, spriteDimensions.Width, spriteDimensions.Height);
                    spriteBatch.Draw(Sprite, currentPos, new Rectangle(spriteDimensions.X, spriteDimensions.Y, spriteDimensions.Width, spriteDimensions.Height), color:Color.White);
                        break;
                case animState.RIGHT:
                    spriteBatch.Draw(Sprite, currentPos, new Rectangle(spriteDimensions.X + animFrameIndex * spriteDimensions.Width, spriteDimensions.Y * animFrameIndex +2 * spriteDimensions.Height, spriteDimensions.Width, spriteDimensions.Height), color: Color.White); 
                        break;
                case animState.LEFT:
                    spriteBatch.Draw(Sprite, currentPos, new Rectangle(spriteDimensions.X + animFrameIndex * spriteDimensions.Width, spriteDimensions.Y + spriteDimensions.Height, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);
                        break;
                case animState.UP:
                    spriteBatch.Draw(Sprite, currentPos, new Rectangle(spriteDimensions.X + animFrameIndex * spriteDimensions.Width, spriteDimensions.Y * animFrameIndex + 3 * spriteDimensions.Height, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);
                    break;
                case animState.DOWN:
                    spriteBatch.Draw(Sprite, currentPos, new Rectangle(spriteDimensions.X + animFrameIndex * spriteDimensions.Width, spriteDimensions.Y * animFrameIndex * spriteDimensions.Height, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);
                    break;
                default:
                    spriteBatch.Draw(Sprite, currentPos, new Rectangle(spriteDimensions.X, spriteDimensions.Y, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);
                    break;

            }
            
            //spriteBatch.Draw(Sprite, currentPos, rect, Color.White);
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

        public void SetAnimState(animState state)
        {
            currentAnimState = state;
        }

        protected void SetCurrentFrame(double deltaTime)
        {
            currentFrameTime += deltaTime;
            if (currentFrameTime > frameTimeLimit)
            {
                animFrameIndex++;
                if(animFrameIndex > 5)
                    animFrameIndex = 0;
                currentFrameTime = 0.0f;
            }
        }

        public void SetFrame(double inputDeltaTime)
        {
            SetCurrentFrame(inputDeltaTime);
        }

        public float GetSpeed()
        {
            return moveSpeed;
        }

        public bool GetCanScroll()
        {
            return canScroll;
        }
    }
}
