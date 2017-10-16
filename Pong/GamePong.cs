using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{

    public enum GameState
    {
        Stopped,
        Running,
        Finished
    }

    public class GamePong : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

        private const int gameWidth = 800;
        private const int gameHeight = 600;
        private GameObjects gameObjects;

        SpriteFont scoresFont;
        private Texture2D dividerTexture;
        private SpriteFont font;

        Color BackgroundColor = Color.Black;


        public GamePong()
		{
            graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = gameWidth;
            graphics.PreferredBackBufferHeight = gameHeight;
        }

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scoresFont = Content.Load<SpriteFont>("font");
            var bounds = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            font = Content.Load<SpriteFont>("font");
            dividerTexture = TextureGenerator.CreateTexture(GraphicsDevice, 10, 10);
            var baseTexture = TextureGenerator.CreateTexture(GraphicsDevice, 10, 100);
            Player playerLeft = new Player(baseTexture, new Vector2(20, gameHeight / 2 - 50), bounds, 1);
            Player playerRight = new Player(baseTexture, new Vector2(gameWidth - 40, gameHeight / 2 - 50), bounds, 2);

            gameObjects = new GameObjects
            {
                playerLeft = playerLeft,
                playerRight = playerRight,
                Ball = new Ball(TextureGenerator.CreateTexture(GraphicsDevice, 10, 10), new Vector2(gameWidth / 2, gameHeight / 2), bounds),
                //SoundManager = soundManager,
                Score = new ScoreScreen(font, new Vector2(gameWidth / 2 - (int)(gameWidth * 0.30), 20), new Vector2(gameWidth / 2 + (int)(gameWidth * 0.30), 20))
            };


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameObjects.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            DrawDivider(spriteBatch);
            gameObjects.DrawScores(spriteBatch, scoresFont, graphics);
            gameObjects.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawDivider(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < gameHeight / 30; i++)
            {
                spriteBatch.Draw(dividerTexture, new Vector2(gameWidth / 2, (i * 30) + 10), Color.White);
            }
        }
    }
}
