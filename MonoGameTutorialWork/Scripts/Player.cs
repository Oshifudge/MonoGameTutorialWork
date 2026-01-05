using System;
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
        private Player2 player2;

        public Player(int lives, Vector2 position, Levels currentLevel, Rectangle rectangle) : base(position, currentLevel, rectangle)
        {
            moveSpeed = 1.5f;
            currentLives = lives;
            initialLives = lives;
            
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

        public void SetPlayer2(Player2 p2)
        {
            player2 = p2;
        }

        //public override void Up()
        //{

        //    if (currentLevel.IsObstacle((int)currentPos.X, (int)currentPos.Y) ||
        //        currentLevel.IsObstacle((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y))
        //    {
        //        ReduceLives;
        //    }


        //}
        //public override void Down()
        //{
        //    if (currentLevel.IsObstacle((int)currentPos.X, (int)currentPos.Y) ||
        //        currentLevel.IsObstacle((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y))
        //    {
        //        ReduceLives;
        //    }

        //}
        public override void Left()
        {
            base.Left();
            if (currentLevel.IsObstacle((int)currentPos.X, (int)currentPos.Y) ||
                currentLevel.IsObstacle((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y))
            {
                base.ResetCurrentPos();
                player2.ResetCurrentPos();
                ReduceLives();
            }
            //if (currentLevel.IsCrystal((int)currentPos.X, (int)currentPos.Y) ||
            //    currentLevel.IsCrystal((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y))
            //{
            //    score += 10;
            //}

        }
        public override void Right()
        {
            base.Right();
            if (currentLevel.IsObstacle((int)currentPos.X, (int)currentPos.Y) ||
                currentLevel.IsObstacle((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y))
            {
                base.ResetCurrentPos();
                player2.ResetCurrentPos();
                ReduceLives();
            }
            //if(currentLevel.IsCrystal((int)currentPos.X, (int)currentPos.Y) ||
            //    currentLevel.IsCrystal((int)currentPos.X + spriteDimensions.Width - 1, (int)currentPos.Y))
            //{
            //    score += 10;
            //}

        }
    }
}
