using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Content;
using MonoGameTutorialWork.Scripts;

namespace MonoGameTutorialWork.Scripts
{
    internal class PlayGame
    {
        Enemy enemy;
        Player player;
        Levels level;

        public PlayGame()
        {
            level = new Levels();
            player = new Player(3, new Vector2(350, 100), level, new Rectangle(0, 0, 52, 72));
            enemy = new Enemy(new Vector2(350, 370), level, new Rectangle(0, 0, 52, 72));
            
        }

        public e_gameStates Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return e_gameStates.MENU;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                player.Up();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                player.Down();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.Left();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.Right();
            }

            enemy.Chase(player);

            if (enemy.Caught(player))
            {
                player.ReduceLives();
                System.Console.WriteLine("Player Lives = " + player.GetLives());
                player.ResetCurrentPos();
                enemy.ResetCurrentPos();
            }
            if (player.GetLives() == 0)
            {
                level.ResetLevels();
                return e_gameStates.GAMEOVER;
            }


            return e_gameStates.GAME;


        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Turquoise);
            level.Draw(spriteBatch);
            player.Draw(spriteBatch, new Rectangle(0, 0, 52, 72));
            enemy.Draw(spriteBatch, new Rectangle(0, 0, 52, 72));

        }

        public void LoadContent(ContentManager CM, GraphicsDeviceManager graphicsDeviceManager)
        {
            player.LoadContent(CM, "chara6");
            enemy.LoadContent(CM, "orc2");
            level.LoadContent(CM, "Wall1");
            graphicsDeviceManager.PreferredBackBufferWidth = (int)level.GetLevelSize().X;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)level.GetLevelSize().Y;
            graphicsDeviceManager.ApplyChanges();
        }

        public Vector2 GetScreenWH()
        {
            return level.GetLevelSize();
        }

        public int GetLevelNumber()
        {
            return level.currentLevel;
        }
    }
}
