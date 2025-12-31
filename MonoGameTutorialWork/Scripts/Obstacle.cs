using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTutorialWork.Scripts;

namespace MonoGameTutorialWork.Scripts
{
    internal class Obstacle : Creature
    {
        public Obstacle(Vector2 position, Levels currentLevel, Rectangle rectangle) : base(position, currentLevel, rectangle)
        {
            moveSpeed = 1;
        }
    }

}
