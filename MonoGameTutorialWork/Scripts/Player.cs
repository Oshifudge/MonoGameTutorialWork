using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTutorialWork.Scripts
{
    internal class Player : Creature
    {
        private int currentLives;
        private int initialLives;
        private int score;

        public Player(int lives, Vector2 position) : base(position)
        {
            currentLives = lives;
            initialLives = currentLives;
            score = 0;
        }

        public int GetLives()
        {
            return currentLives;
        }

        public void ReduceLives()
        {
            currentLives--;
        }
        public void ResetLives()
        {
            currentLives = initialLives;
        }

        public override void Up()
        {
            currentPos.Y -= 1.5f;
        }
        public override void Down()
        {
            currentPos.Y += 1.5f;
        }
        public override void Left()
        {
            currentPos.X -= 1.5f;
        }
        public override void Right()
        {
            currentPos.X += 1.5f;
        }
    }
}
