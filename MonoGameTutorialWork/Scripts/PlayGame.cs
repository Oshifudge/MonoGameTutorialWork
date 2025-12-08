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
        private RenderTarget2D renderTarget;

        public PlayGame()
        {
            level = new Levels();
            player = new Player(3, new Vector2(350, 100), level, new Rectangle(0, 0, 52, 72));
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

            player.SetFrame(Delta);
            if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
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

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, HUD gameHUD, GraphicsDeviceManager graphicsDeviceManager)
        {
            DrawRengerTarget(spriteBatch, graphicsDevice);
            graphicsDevice.Clear(Color.LightGreen);

            int rectx = (int)player.GetCurrentPos().X - graphicsDeviceManager.PreferredBackBufferWidth / 2;
            if (rectx > (int)GetLevelWH().X - graphicsDeviceManager.PreferredBackBufferWidth)
                rectx = (int)GetLevelWH().X - graphicsDeviceManager.PreferredBackBufferWidth;
            else if (rectx <0)
                rectx = 0;
            int recty = (int)player.GetCurrentPos().Y - graphicsDeviceManager.PreferredBackBufferWidth / 2;
            if (recty > (int)GetLevelWH().Y - graphicsDeviceManager.PreferredBackBufferWidth)
                recty = (int)GetLevelWH().Y - graphicsDeviceManager.PreferredBackBufferWidth;
            else if (recty < 0)
                recty = 0;

            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, new Rectangle(rectx, recty, graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight), Color.Bisque);
            
            gameHUD.SetMessage("who else up playing they game");
            gameHUD.DrawString(spriteBatch, new Vector2((GetLevelWH().X / 2) - 512, 0), Color.Blue);
            gameHUD.SetMessage("Lives:");
            gameHUD.DrawString(spriteBatch, new Vector2(20, 0), Color.Blue);
            gameHUD.DrawHearts(spriteBatch, new Vector2(10, 0), player.GetLives());
            spriteBatch.End();

        }

        public void LoadContent(ContentManager CM, GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
        {
            player.LoadContent(CM, "chara6");
            enemy.LoadContent(CM, "orc2");
            level.LoadContent(CM, "Wall1", "hplat1");
            renderTarget = new RenderTarget2D(graphicsDevice, (int)level.GetLevelSize().X, (int)level.GetLevelSize().Y);
            graphicsDeviceManager.PreferredBackBufferWidth = (int)level.GetLevelSize().X;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)level.GetLevelSize().Y;
            graphicsDeviceManager.ApplyChanges();
        }

        public Vector2 GetLevelWH()
        {
            return level.GetLevelSize();
        }

        public int GetLevelNumber()
        {
            return level.currentLevel;
        }

        private void ResetAll(GraphicsDevice graphicsDevice)
        {
           enemy.ResetCurrentPos();
           player.ResetCurrentPos();
           player.ResetLives();
           level.ResetLevels();
           renderTarget = new RenderTarget2D(graphicsDevice, (int)level.GetLevelSize().X, (int)level.GetLevelSize().Y);
        }

        private void DrawRengerTarget(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(Color.FloralWhite);
            spriteBatch.Begin();
            level.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);
        }
    }
}
