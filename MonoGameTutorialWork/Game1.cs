using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTutorialWork.Scripts;
using Microsoft.Xna.Framework.Content;

namespace MonoGameTutorialWork
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SceneManager _sceneManager;
        private Levels _levels;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //_graphics.PreferredBackBufferHeight = 768;
            //_graphics.PreferredBackBufferWidth = 1024;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _sceneManager = new SceneManager();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _sceneManager.LoadContent(Content, _graphics, GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //  Exit();

            // TODO: Add your update logic here
            _sceneManager.Update(this, gameTime, GraphicsDevice, _graphics);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _sceneManager.Draw(GraphicsDevice, _spriteBatch, _graphics);
            base.Draw(gameTime);

        }
    }
}
