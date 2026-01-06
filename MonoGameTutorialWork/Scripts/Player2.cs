using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTutorialWork.Scripts
{
    internal class Player2 : Creature
    {
        private int currentLives;
        private int initialLives;
        private int score;
        private Player player1;

        public Player2(int lives, Vector2 position, Levels currentLevel, Rectangle rectangle, Player p1) : base(position, currentLevel, rectangle)
        {
            moveSpeed = 1.5f;
            currentLives = lives;
            initialLives = lives;
            player1 = p1;
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

        public int GetScore()
        {
            return score;
        }

        public void AddScore(int scoreToAdd)
        {
           score += scoreToAdd;
        }

        public void AddLives(int livesToAdd)
        {
            currentLives += livesToAdd;
        }

        public override void Left()
        {
            base.Left();
            if (currentLevel.IsObstacle((int)currentPos.X, (int)currentPos.Y) ||
                currentLevel.IsObstacle((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y))
            {
                base.ResetCurrentPos();
                player1.ResetCurrentPos();
                ReduceLives();
            }
        }
        public override void Right()
        {
            base.Right();
            if (currentLevel.IsObstacle((int)currentPos.X, (int)currentPos.Y) ||
                currentLevel.IsObstacle((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y))
            {
                
                base.ResetCurrentPos();
                player1.ResetCurrentPos();
                ReduceLives();
            }

        }
    }
}
