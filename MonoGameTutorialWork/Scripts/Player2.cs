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
        new void Left()
        {

            //levelScript.IsWall(currentPos.X, currentPos.Y);

        }
        new void Right()
        {

            //levelScript.IsWall(currentPos.X, currentPos.Y);

        }
    }
}
