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
        Player2 player2;
        Levels level;
        Obstacle obstacle;
        bool jumpIsPressed;
        private RenderTarget2D renderTarget;

        Texture2D background;
        Texture2D background2;
        Texture2D foreground;
        Vector2 currentPosition;

        public PlayGame()
        {
            
            level = new Levels();
            player = new Player(3, new Vector2(300, 1200), level, new Rectangle(0, 0, 32, 32));
            player2 = new Player2(3, new Vector2(300, 800), level, new Rectangle(0, 0, 32, 32));
            enemy = new Enemy(new Vector2(350, 370), level, new Rectangle(52*3, 72*2, 52, 72));
            jumpIsPressed = false;
            currentPosition = new Vector2(0, 0);
        }

        public e_gameStates Update(double Delta, Game1 game, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return e_gameStates.MENU;
            }

            player.JumpOrFall(2);
            player2.JumpOrFall(2);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (!jumpIsPressed)
                {
                    jumpIsPressed = true;
                    player.SetIsJumping();
                    player2.SetIsJumping();
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                jumpIsPressed = false;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.SetAnimState(Creature.animState.LEFT);
                player.Left();
                if(player.GetCanScroll())
                {
                    if(player.GetCurrentPos().X < (int)GetLevelWH().X - graphicsDeviceManager.PreferredBackBufferWidth/2 &&
                        player.GetCurrentPos().X > graphicsDeviceManager.PreferredBackBufferWidth/2)
                    {
                        currentPosition.X += player.GetSpeed();
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.SetAnimState(Creature.animState.RIGHT);
                player.Right();
                if (player.GetCanScroll())
                {
                    if (player.GetCurrentPos().X < (int)GetLevelWH().X - graphicsDeviceManager.PreferredBackBufferWidth / 2 &&
                        player.GetCurrentPos().X > graphicsDeviceManager.PreferredBackBufferWidth / 2)
                    {
                        currentPosition.X -= player.GetSpeed();
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                player2.SetAnimState(Creature.animState.LEFT);
                player2.Left();
                //if (player.GetCanScroll())
                //{
                //    if (player.GetCurrentPos().X < (int)GetLevelWH().X - graphicsDeviceManager.PreferredBackBufferWidth / 2 &&
                //        player.GetCurrentPos().X > graphicsDeviceManager.PreferredBackBufferWidth / 2)
                //    {
                        currentPosition.X += player2.GetSpeed();
                //    }
                //}
            }
            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                player2.SetAnimState(Creature.animState.RIGHT);
                player2.Right();
                //if (player.GetCanScroll())
                //{
                //    if (player.GetCurrentPos().X < (int)GetLevelWH().X - graphicsDeviceManager.PreferredBackBufferWidth / 2 &&
                //        player.GetCurrentPos().X > graphicsDeviceManager.PreferredBackBufferWidth / 2)
                //    {
                        currentPosition.X -= player2.GetSpeed();
                //    }
                //}
            }

            player.SetFrame(Delta);
            player2.SetFrame(Delta);
            level.AnimateObstacles(Delta);


            if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                player.SetAnimState(Creature.animState.IDLE);
            }
            if(Keyboard.GetState().IsKeyUp(Keys.J) && Keyboard.GetState().IsKeyUp(Keys.L))
            {
                player2.SetAnimState(Creature.animState.IDLE);
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
            int recty = (int)player.GetCurrentPos().Y - graphicsDeviceManager.PreferredBackBufferHeight / 2;
            if (recty > (int)GetLevelWH().Y - graphicsDeviceManager.PreferredBackBufferHeight)
                recty = (int)GetLevelWH().Y - graphicsDeviceManager.PreferredBackBufferHeight;
            else if (recty < 0)
                recty = 0;

            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, new Vector2(0,0), new Rectangle(rectx, recty, graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight), Color.White);
            
            gameHUD.SetMessage("who else up playing they game");
            gameHUD.DrawString(spriteBatch, new Vector2((GetLevelWH().X / 2) - 512, 0), Color.Blue);
            gameHUD.SetMessage("Lives:");
            gameHUD.DrawString(spriteBatch, new Vector2(20, 0), Color.Blue);
            gameHUD.DrawHearts(spriteBatch, new Vector2(10, 0), player.GetLives());
            spriteBatch.End();
        }

        public void LoadContent(ContentManager CM, GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
        {
            level.LoadContent(CM, "GreyWall", "GreyPlatform", "SingleObstacle2x", "SingleObstacleFrame22x");
            player.LoadContent(CM, "Player1SpriteSheet2x");
            player2.LoadContent(CM, "Player2SpriteSheet2x");
            obstacle.LoadContent(CM, "SingleObstacle2x");
            enemy.LoadContent(CM, "orc2");
            
            
            //graphicsDeviceManager.PreferredBackBufferWidth = (int)level.GetLevelSize().X;
            //graphicsDeviceManager.PreferredBackBufferHeight = (int)level.GetLevelSize().Y;
            graphicsDeviceManager.PreferredBackBufferHeight = 1080;
            graphicsDeviceManager.PreferredBackBufferWidth = 1920;
            renderTarget = new RenderTarget2D(graphicsDevice, (int)level.GetLevelSize().X, (int)level.GetLevelSize().Y);
            //Console.WriteLine("thing is " + graphicsDeviceManager.PreferredBackBufferWidth);
            //Console.WriteLine("thing 2 is " + graphicsDeviceManager.PreferredBackBufferHeight);
            
            graphicsDeviceManager.ApplyChanges();
            background = CM.Load<Texture2D>("backgroundCastle");
            background2 = CM.Load<Texture2D>("castlegrey");
            foreground = CM.Load<Texture2D>("tree31");
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
           player2.ResetCurrentPos();
           player.ResetLives();
           level.ResetLevels();
           renderTarget = new RenderTarget2D(graphicsDevice, (int)level.GetLevelSize().X, (int)level.GetLevelSize().Y);
        }

        private void DrawRengerTarget(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            for(int i = 0; i < 2; i++)
            {
                spriteBatch.Draw(background, new Vector2((background.Width * i) + currentPosition.X * 0.1f, 0), Color.White);
                //background.Draw(spriteBatch, new Vector2((background.Width * i) + currentPosition.X * 0.1f, 0), Color.White);
                //new Vector2((background2.Width * i) + currentPosition.X * 0.1f, 0);
            }
            for (int i = 0; i < 12; i++)
            {
                spriteBatch.Draw(background2, new Vector2((1000 * i) + currentPosition.X * 0.3f, 0), Color.White);
                //background2.Draw(spriteBatch, new Vector2((1000 * i) + currentPosition.X * 0.3f, 0), Color.White);
                //new Vector2((background2.Width * i) + currentPosition.X * 0.3f, 0);
            }

            level.Draw(spriteBatch);
            player.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            obstacle.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
           spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);
        }

    }
}
