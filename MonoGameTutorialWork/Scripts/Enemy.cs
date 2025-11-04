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
                Up();
            }
            if (currentPos.Y < player.GetCurrentPos().Y)
            {
                Down();
            }
            if (currentPos.X > player.GetCurrentPos().X)
            {
                Left();
            }
            if (currentPos.X < player.GetCurrentPos().X)
            {
                Right();
            }

        }
    }
}
