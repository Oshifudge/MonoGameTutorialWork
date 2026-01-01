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

        public Player2(int lives, Vector2 position, Levels currentLevel, Rectangle rectangle) : base(position, currentLevel, rectangle)
        {
            moveSpeed = 1.5f;
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

        //new void Up()
        //{

        //    //levelScript.IsWall(currentPos.X, currentPos.Y);

        //}
        //new void Down()
        //{

        //    //levelScript.IsWall(currentPos.X, currentPos.Y);

        //}
        public override void Left()
        {
            base.Left();
            if (currentLevel.IsObstacle((int)currentPos.X, (int)currentPos.Y) ||
                currentLevel.IsObstacle((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y))
            {
                base.ResetCurrentPos();
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
                ReduceLives();
            }

        }
    }
}
