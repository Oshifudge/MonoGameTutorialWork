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

        private SpriteFont MainFont;
        private string textToShow;

        public SceneManager()
        {
            e_State = e_gameStates.MENU;
            play = new PlayGame();
            gameOver = new GameOver();
        }

        public void Update(Game1 game, GameTime time)
        {
            double deltaTime = time.ElapsedGameTime.TotalSeconds;

            switch (e_State)
            {
                case e_gameStates.MENU:
                    {
                        SwitchState(menu.Update(game));
                        SetMessage("in my menus");
                        break;
                    }
                case e_gameStates.GAME:
                    {
                        SwitchState(play.Update());
                        SetMessage("who else up playing they game and we on level" + play.GetLevelNumber());
                        break;
                    }
                case e_gameStates.GAMEOVER:
                    {
                        SwitchState(gameOver.Update(deltaTime));
                        SetMessage("game over LOL");
                        break;
                    }
                default: break;
            }
        }


        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            switch (e_State)
            {
                case e_gameStates.MENU:
                    menu.Draw(graphicsDevice);
                    spriteBatch.DrawString(MainFont, textToShow, new Vector2((play.GetScreenWH().X / 2) - 256, 0), Color.Black);
                    break;
                case e_gameStates.GAME:
                    play.Draw(graphicsDevice, spriteBatch);
                    break;
                case e_gameStates.GAMEOVER:
                    gameOver.Draw(graphicsDevice);
                    break;
                default: break;
            }
            spriteBatch.End();
        }

        public void SwitchState(e_gameStates State)
        {
            e_State = State;
        }

        public void LoadContent(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager)
        {
            play.LoadContent(contentManager, graphicsDeviceManager);
            MainFont = contentManager.Load<SpriteFont>("MainFont");
            menu = new Menu(play.GetScreenWH());
        }

        void SetMessage(string inputText)
        {
            textToShow = inputText;
        }

    }
}
