using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTutorialWork.Scripts;

namespace MonoGameTutorialWork.Scripts
{
    internal class Enemy : Creature
    {
        public Enemy(int lives, Vector2 position, int currentLevel, Rectangle rectangle) : base(position, currentLevel, rectangle)
        {

        }

        public void Chase(Player player)
        {
            if (currentPos.Y < player.GetCurrentPos().Y)
            {
                Up();
            }
            if (currentPos.Y > player.GetCurrentPos().Y)
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

        public bool Caught(Player player)
        {
            if (currentPos == player.GetCurrentPos())
                return true;
            else
                return false;
        }
    }
}
