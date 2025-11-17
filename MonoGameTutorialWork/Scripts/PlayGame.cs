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
        bool jumpIsPressed;

        public PlayGame()
        {
            level = new Levels();
            player = new Player(3, new Vector2(350, 100), level, new Rectangle(52*6, 72*4, 52, 72));
            enemy = new Enemy(new Vector2(350, 370), level, new Rectangle(52*3, 72*2, 52, 72));
            jumpIsPressed = false;
            
        }

        public e_gameStates Update(double Delta)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return e_gameStates.MENU;
            }

            player.JumpOrFall(2);

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (!jumpIsPressed)
                {
                    jumpIsPressed = true;
                    player.SetIsJumping();
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.W))
            {
                jumpIsPressed = false;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.SetAnimState(Creature.animState.LEFT);
                player.Left();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.SetAnimState(Creature.animState.RIGHT);
                player.Right();
            }

            player.SetCurrentFrame(Delta);
            if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D)
            {
                player.SetAnimState(Creature.animState.IDLE);
            }
            enemy.Chase(player);
            enemy.SetFrame(Delta);

            if (enemy.CollidesWith(player))
            {
                player.ReduceLives();
                System.Console.WriteLine("Player Lives = " + player.GetLives());
                player.ResetCurrentPos();
                enemy.ResetCurrentPos();
            }
            if(player.CollidesWith(enemy))
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
            player.Draw(spriteBatch
                //, new Rectangle(0, 0, 52, 72)
                );
            enemy.Draw(spriteBatch
               // , new Rectangle(0, 0, 52, 72)
                );

        }

        public void LoadContent(ContentManager CM, GraphicsDeviceManager graphicsDeviceManager)
        {
            player.LoadContent(CM, "chara6");
            enemy.LoadContent(CM, "orc2");
            level.LoadContent(CM, "Wall1", "hplat1");
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
