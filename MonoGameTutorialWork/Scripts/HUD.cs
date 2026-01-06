using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms.Automation;


namespace MonoGameTutorialWork.Scripts
{
    internal class HUD
    {
        private SpriteFont mainFont;
        private string message;
        private Texture2D heartIcon;


        public void LoadContent(ContentManager contentManager)
        {
            heartIcon = contentManager.Load<Texture2D>("hud_heartFull");
            mainFont = contentManager.Load<SpriteFont>("MainFont");
        }
        public void SetMessage(string inputMessage)
        {
            message = inputMessage;
        }

        public void DrawString(SpriteBatch spriteBatch, string displayMessage,Vector2 pos, Color col)
        {
            spriteBatch.DrawString(mainFont, displayMessage, pos, col);
        }

        public void DrawHearts(SpriteBatch spriteBatch, Vector2 pos, int lives, Color col)
        {
            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(heartIcon,
                    new Vector2(pos.X + mainFont.MeasureString(message).X + i * heartIcon.Width, pos.Y),
                    new Rectangle(0, 0, heartIcon.Width, heartIcon.Height), col);
            }
        }

        public void DrawScore(SpriteBatch spriteBatch, int score, Color col)
        {
            spriteBatch.DrawString(mainFont, "Score: " + score.ToString(), new Vector2(1680, 10), col);
        }

        public void EndGame(SpriteBatch spriteBatch, string endMessage, int score)
        {
            Vector2 size = mainFont.MeasureString(endMessage);
            Vector2 pos = new Vector2((1920 - size.X) / 2, (1080 - size.Y) / 2);
            spriteBatch.DrawString(mainFont, endMessage + "You scored:" + score.ToString(), pos, Color.LawnGreen);
        }
    }
}
