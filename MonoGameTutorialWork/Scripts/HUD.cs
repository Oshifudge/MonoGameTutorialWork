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
            LoadContent(contentManager);
            heartIcon = contentManager.Load<Texture2D>("hud_heartFull");
            mainFont = contentManager.Load<SpriteFont>("MainFont");
        }
        public void SetMessage(string inputMessage)
        {
            message = inputMessage;
        }

        public void DrawString(SpriteBatch spriteBatch, Vector2 pos, Color col)
        {
            spriteBatch.DrawString(mainFont, message, pos, col);
        }

        public void DrawHearts(SpriteBatch spriteBatch, Vector2 pos, int lives)
        {
            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(heartIcon,
                    new Vector2(pos.X + mainFont.MeasureString(message).X + lives * heartIcon.Width, pos.Y),
                    new Rectangle(0, 0, heartIcon.Width, heartIcon.Height), Color.White);
            }
        }
        

        
    }
}
