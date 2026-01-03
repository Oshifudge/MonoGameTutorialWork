using System.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTutorialWork.Scripts;

namespace MonoGameTutorialWork.Scripts
{
    internal class Enemy : Creature
    {
        public Enemy(Vector2 position, Levels currentLevel, Rectangle rectangle) : base(position, currentLevel, rectangle)
        {
            moveSpeed = 1;
        }

        public void Chase(Player player)
        {
            if (currentPos.Y > player.GetCurrentPos().Y)
            {
                Up(1);
                //currentAnimState = animState.RIGHT;
                currentAnimState = animState.RIGHT;
            }
            if (currentPos.Y < player.GetCurrentPos().Y)
            {
                Down(1);
                //currentAnimState = animState.IDLE;
                currentAnimState = animState.RIGHT;

            }
            if (currentPos.X > player.GetCurrentPos().X)
            {
                Left();
                //currentAnimState = animState.LEFT;
                currentAnimState = animState.RIGHT;

            }
            if (currentPos.X < player.GetCurrentPos().X)
            {
                Right();
                //currentAnimState = animState.RIGHT;
                currentAnimState = animState.RIGHT;

            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, currentPos, new Rectangle(spriteDimensions.X + animFrameIndex * spriteDimensions.Width, spriteDimensions.Y * spriteDimensions.Height, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);
        }
    }
}
