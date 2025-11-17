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
                Up(2);
                currentAnimState = animState.UP;
            }
            if (currentPos.Y < player.GetCurrentPos().Y)
            {
                Down(2);
                currentAnimState = animState.DOWN;

            }
            if (currentPos.X > player.GetCurrentPos().X)
            {
                Left();
                currentAnimState = animState.LEFT;

            }
            if (currentPos.X < player.GetCurrentPos().X)
            {
                Right();
                currentAnimState = animState.RIGHT;

            }

        }
    }
}
