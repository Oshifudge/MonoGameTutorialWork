using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Content;
using MonoGameTutorialWork.Scripts;
using System;
using SharpDX.MediaFoundation;

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
        int storedP1Lives;
        int storedP2Lives;

        Texture2D background;
        Texture2D background2;
        Texture2D foreground;
        Vector2 currentPosition;

        public PlayGame()
        {
            
            level = new Levels();
            player = new Player(3, new Vector2(300, 1770), level, new Rectangle(0, 0, 32, 32));
            player2 = new Player2(3, new Vector2(300, 1210), level, new Rectangle(0, 0, 32, 32));
            enemy = new Enemy(new Vector2(2300, 1550), level, new Rectangle(0, 0, 64, 24));
            jumpIsPressed = false;
            currentPosition = new Vector2(0, 0);
            storedP1Lives = player.GetLives();
            storedP2Lives = player2.GetLives();
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
            level.GetDeltaTime(Delta);
            enemy.SetFrame(Delta);
            enemy.Chase(player);

            if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                player.SetAnimState(Creature.animState.IDLE);
            }
            if(Keyboard.GetState().IsKeyUp(Keys.J) && Keyboard.GetState().IsKeyUp(Keys.L))
            {
                player2.SetAnimState(Creature.animState.IDLE);
            }

            

            
            if (enemy.CollidesWith(player))
            {
                player.ReduceLives();
                //System.Console.WriteLine("Player Lives = " + player.GetLives());
                player.ResetCurrentPos();
                enemy.ResetCurrentPos();
            }
            if(player.CollidesWith(enemy))
            {
                player.ReduceLives();
                //System.Console.WriteLine("Player Lives = " + player.GetLives());
                player.ResetCurrentPos();
                enemy.ResetCurrentPos();
            }
            if (enemy.CollidesWith(player2))
            {
                player2.ReduceLives();
                //System.Console.WriteLine("Player Lives = " + player.GetLives());
                player2.ResetCurrentPos();
                enemy.ResetCurrentPos();
            }
            if (player2.CollidesWith(enemy))
            {
                player2.ReduceLives();
                player2.ResetCurrentPos();
                enemy.ResetCurrentPos();
            }

            //if (player2.CollidesWithCrystal(level.crystal))
            //{
            //    for(int num = 0; num < level.Crystals.Count; num++)
            //    {
            //        Crystal crys = level.Crystals[num];
            //        player2.GetCurrentPos();
            //        crys.GetCurrentPos();
            //        if(player2.GetCurrentPos().X <= crys.GetCurrentPos().X + player2.GetSpriteDimensions().Width - 1 &&
            //           player2.GetCurrentPos().X + player2.GetSpriteDimensions().Width - 1 >= crys.GetCurrentPos().X &&
            //           player2.GetCurrentPos().Y <= crys.GetCurrentPos().Y + player2.GetSpriteDimensions().Height - 1 &&
            //           player2.GetCurrentPos().Y + player2.GetSpriteDimensions().Height - 1 >= crys.GetCurrentPos().Y)
            //        Console.WriteLine("Player 2 Collected Crystal");
            //    }
                
            //}

            for (int num = 0; num < level.Crystals.Count; num++)
            {
                Crystal crys = level.Crystals[num];
                player2.GetCurrentPos();
                crys.GetCurrentPos();
                if (player2.GetCurrentPos().X <= crys.GetCurrentPos().X + player2.GetSpriteDimensions().Width + 30 &&
                   player2.GetCurrentPos().X + player2.GetSpriteDimensions().Width + 30 >= crys.GetCurrentPos().X &&
                   player2.GetCurrentPos().Y <= crys.GetCurrentPos().Y + player2.GetSpriteDimensions().Height + 30 &&
                   player2.GetCurrentPos().Y + player2.GetSpriteDimensions().Height + 10 >= crys.GetCurrentPos().Y &&
                   crys.currentPos.X <= player2.GetCurrentPos().X + crys.GetSpriteDimensions().Width + 30 &&
                   crys.currentPos.X + crys.GetSpriteDimensions().Width +30 >= player2.GetCurrentPos().X &&
                   crys.currentPos.Y <= player2.GetCurrentPos().Y + crys.GetSpriteDimensions().Height + 30 &&
                   crys.currentPos.Y + crys.GetSpriteDimensions().Height + 30 >= player2.GetCurrentPos().Y)
                {
                    Console.WriteLine("Player 2 Collected Crystal");
                    player2.AddScore(10);
                    level.Crystals.RemoveAt(num);
                }
            }

                if (player.GetLives() == 0 || player2.GetLives() == 0)
            {
                //level.ResetLevels();
                player.ResetLives();
                player2.ResetLives();
                return e_gameStates.GAMEOVER;
                ResetAll(graphicsDevice);
            }

            //storedP1Lives = player.GetLives();
            //storedP2Lives = player2.GetLives();
            if (!(player.GetLives()  == storedP1Lives))
            {
                player2.ResetCurrentPos();
                storedP1Lives = player.GetLives();
                storedP2Lives = player2.GetLives();
            }

            if (!(player2.GetLives() == storedP2Lives))
            {
                player.ResetCurrentPos();
                storedP1Lives = player.GetLives();
                storedP2Lives = player2.GetLives();
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
            
            //gameHUD.SetMessage("who else up playing they game");
            //gameHUD.DrawString(spriteBatch, new Vector2((GetLevelWH().X / 2) - 512, 0), Color.Blue);
            //gameHUD.SetMessage("P1 Lives ");
            gameHUD.DrawString(spriteBatch,"P1 Lives " , new Vector2(20, 0), Color.OrangeRed);
            gameHUD.DrawHearts(spriteBatch, new Vector2(10, 10), player.GetLives(), Color.Red);
            //gameHUD.SetMessage("P2 Lives ");
            gameHUD.DrawString(spriteBatch, "P2 Lives ", new Vector2(1000, 0), Color.Blue);
            gameHUD.DrawHearts(spriteBatch, new Vector2(1000, 10), player2.GetLives(), Color.LightSkyBlue);
            gameHUD.DrawScore(spriteBatch, player.GetScore() + player2.GetScore(), Color.DeepPink);
            spriteBatch.End();
        }

        public void LoadContent(ContentManager CM, GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
        {
            level.LoadContent(CM, "GreyWall", "GreyPlatform", "SingleObstacle2x", "SingleObstacleFrame22x", "CrystalPickup", "HeartPickup");
            player.LoadContent(CM, "Player1SpriteSheet2x");
            player2.LoadContent(CM, "Player2SpriteSheet2x");
            //obstacle.LoadContent(CM, "SingleObstacle2x");
            enemy.LoadContent(CM, "FlyingEnemySprites");
            
            
            //graphicsDeviceManager.PreferredBackBufferWidth = (int)level.GetLevelSize().X;
            //graphicsDeviceManager.PreferredBackBufferHeight = (int)level.GetLevelSize().Y;
            graphicsDeviceManager.PreferredBackBufferHeight = 1080;
            graphicsDeviceManager.PreferredBackBufferWidth = 1920;
            //renderTarget = new RenderTarget2D(graphicsDevice, (int)level.GetLevelSize().X, (int)level.GetLevelSize().Y);
            renderTarget = new RenderTarget2D(graphicsDevice,  (int)level.GetLevelSize().X, (int)level.GetLevelSize().Y);
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
           player2.ResetLives();
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
            enemy.Draw(spriteBatch);
           spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);
        }

    }
}
