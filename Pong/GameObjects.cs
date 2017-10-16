using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Pong
{
    public class GameObjects
    {
        public Player playerLeft { get; set; }
        public Player playerRight { get; set; }
        public Ball Ball { get; set; }
        public ScoreScreen Score { get; set; }
        GameState gameState = GameState.Stopped;

        public void Update(GameTime gameTime)
        {

            KeyboardState keyState = Keyboard.GetState();

            if (gameState == GameState.Running)
            {
                Ball.Update(gameTime, this);
                playerLeft.Update(gameTime, this);
                playerRight.Update(gameTime, this);

                if (playerLeft.Score == 3 || playerRight.Score == 3)
                    gameState = GameState.Finished;

                Score.Update(playerRight.Score.ToString(), playerLeft.Score.ToString());
            }
            else if (keyState.IsKeyDown(Keys.Space))
            { 
                Reset();
                gameState = GameState.Running;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerLeft.Draw(spriteBatch);
            playerRight.Draw(spriteBatch);
            Score.Draw(spriteBatch);
            Ball.Draw(spriteBatch);
        }

        public void Dispose()
        {
            Ball.Dispose();
            playerLeft.Dispose();
            playerRight.Dispose();
        }

        public void Reset()
        {
            Ball.Reset();
            playerLeft.Reset();
            playerRight.Reset();
        }

        public void DrawScores(SpriteBatch spriteBatch, SpriteFont scoresFont, GraphicsDeviceManager graphics)
        {
            Vector2 textPosition = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 100, 20);

            Vector2 textSize = scoresFont.MeasureString("Press SPACE to Start");
            Vector2 TextMiddlePoint = new Vector2(textSize.X / 2, textSize.Y / 2);

            String startText = "Press SPACE to Start";
            String playerWinsText = "Player {0} Wins!";
            String playerLeftText = string.Format(playerWinsText, '1');
            String playerRightText = string.Format(playerWinsText, '2');

            if (gameState == GameState.Stopped)
                spriteBatch.DrawString(scoresFont, startText, GetBestPosition(startText, scoresFont, graphics), Color.White);
            else if (gameState == GameState.Finished)
            {
                if (playerLeft.Score == 3)
                    spriteBatch.DrawString(scoresFont, playerLeftText, GetBestPosition(startText, scoresFont, graphics), Color.White);
                if (playerRight.Score == 3)
                    spriteBatch.DrawString(scoresFont, playerLeftText, GetBestPosition(startText, scoresFont, graphics), Color.White);
            }
        }

        public Vector2 GetBestPosition(String text, SpriteFont spriteFont, GraphicsDeviceManager graphics)
        {
            Vector2 MiddlePoint = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            Vector2 textSize = spriteFont.MeasureString(text);
            return new Vector2((int)(MiddlePoint.X - textSize.X / 2), (int)(MiddlePoint.Y - textSize.Y));
        }
    }
}
