using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MonoGameTutorialWork.Scripts
{
    enum e_gameStates
    {
        MENU, GAME, GAMEOVER
    }

    internal class SceneManager
    {
        private e_gameStates e_State;
        private Menu menu;
        private PlayGame play;
        private GameOver gameOver;
        private HUD hudOverlay;

        public SceneManager()
        {
            e_State = e_gameStates.MENU;
            play = new PlayGame();
            gameOver = new GameOver();
            hudOverlay = new HUD();
        }

        public void Update(Game1 game, GameTime time, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager)
        {
            double deltaTime = time.ElapsedGameTime.TotalSeconds;

            switch (e_State)
            {
                case e_gameStates.MENU:
                    {
                        SwitchState(menu.Update(game));
                        hudOverlay.SetMessage("in my menus");
                        break;
                    }
                case e_gameStates.GAME:
                    {
                        SwitchState(play.Update(deltaTime, game, graphicsDevice, graphicsDeviceManager));
                        SetMessage("who else up playing they game and we on level" + play.GetLevelNumber());
                        break;
                    }
                case e_gameStates.GAMEOVER:
                    {
                        SwitchState(gameOver.Update(deltaTime));
                        hudOverlay.SetMessage("game over LOL");
                        break;
                    }
                default: break;
            }
        }


        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, GraphicsDeviceManager graphicsDeviceManager)
        {
            switch (e_State)
            {
               case e_gameStates.MENU:
                    spriteBatch.Begin();
                    menu.Draw(graphicsDevice);
                    hudOverlay.DrawString(spriteBatch,"Little red thing and little blue thing game" ,new Vector2((play.GetLevelWH().X / 2) - 1400, 0), Color.Black);
                    hudOverlay.DrawString(spriteBatch, "Click to play !", new Vector2((play.GetLevelWH().X / 2) - 1400, 500), Color.Black);
                    spriteBatch.End();
                    break;
                case e_gameStates.GAME:
                    
                    play.Draw(graphicsDevice, spriteBatch, hudOverlay, graphicsDeviceManager);
                    
                    break;
                case e_gameStates.GAMEOVER:
                    spriteBatch.Begin();
                    gameOver.Draw(graphicsDevice);
                    hudOverlay.DrawString(spriteBatch, "Game Over :(", new Vector2((play.GetLevelWH().X / 2) - 1400, 0), Color.Black);
                    spriteBatch.End();
                    break;
                default: break;
            }
        }

        public void SwitchState(e_gameStates State)
        {
            e_State = State;
        }

        public void LoadContent(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
        {
            play.LoadContent(contentManager, graphicsDeviceManager, graphicsDevice);
            hudOverlay.LoadContent(contentManager);
            menu = new Menu(new Vector2(graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight));
        }

        void SetMessage(string inputText)
        {
             
        }

    }
}
