using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;

namespace CatapultGame
{
    class GameplayScreen : GameScreen
    {
        // Texture Members
        Texture2D foregroundTexture;
        SpriteFont hudFont;

        // Rendering members
        Vector2 cloud1Position;
        Vector2 cloud2Position;

        Vector2 playerOneHUDPosition;
        Vector2 playerTwoHUDPosition;
        Vector2 windArrowPosition;
        Vector2 playerOneHealthBarPosition;
        Vector2 playerTwoHealthBarPosition;
        Vector2 healthBarFullSize;

        // Gameplay members
        Human playerOne;
        Player playerTwo;
        Vector2 wind;
        bool changeTurn;
        bool isFirstPlayerTurn;
        bool gameOver;
        Random random;
        const int minWind = 0;
        const int maxWind = 10;

        // Helper members
        bool isDragging;


        public void LoadAssets()
        {
            // Load textures
            foregroundTexture =
                Load<Texture2D>("background.jpg");

            // Define initial cloud position
            cloud2Position = new Vector2(64, 90);

            // TODO: Define intial HUD positions and Initialize human & AI players
            // Define initial HUD positions
            playerOneHUDPosition = new Vector2(7, 7);
            playerTwoHUDPosition = new Vector2(613, 7);
            windArrowPosition = new Vector2(345, 46);
            Vector2 healthBarOffset = new Vector2(25, 82);
            playerOneHealthBarPosition = playerOneHUDPosition + healthBarOffset;
            playerTwoHealthBarPosition = playerTwoHUDPosition + healthBarOffset;
            healthBarFullSize = new Vector2(130, 20);

            // Initialize human & AI players
            playerOne = new Human(ScreenManager.Game, ScreenManager.SpriteBatch,
                PlayerSide.Left);
            playerOne.Initialize();
        }

        public override void LoadContent()
        {
            LoadAssets();
            // TODO: Start the game
            Start();

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            ScreenManager.SpriteBatch.Begin();

            // Render all parts of the screen
            DrawBackground();
            // DrawComputer(gameTime);
            // TODO: Draw players
            DrawPlayerTwo(gameTime);
            DrawPlayerOne(gameTime);

            DrawHud();

            ScreenManager.SpriteBatch.End();
        }

        private void DrawBackground()
        {
            // Clear the background
            ScreenManager.Game.GraphicsDevice.Clear(Color.White);

            // Draw the Castle, trees, and foreground 
            ScreenManager.SpriteBatch.Draw(foregroundTexture,
                Vector2.Zero, Color.White);
        }

        void Start()
        {
            // Set initial wind direction
            wind = Vector2.Zero;
            isFirstPlayerTurn = false;
            changeTurn = true;
        }

        // A simple helper to draw shadowed text.
        void DrawString(SpriteFont font, string text, Vector2 position, Color color)
        {
            ScreenManager.SpriteBatch.DrawString(font, text,
                new Vector2(position.X + 1, position.Y + 1), Color.Black);
            ScreenManager.SpriteBatch.DrawString(font, text, position, color);
        }

        // A simple helper to draw shadowed text.
        void DrawString(SpriteFont font, string text, Vector2 position, Color color, float fontScale)
        {
            ScreenManager.SpriteBatch.DrawString(font, text,
                new Vector2(position.X + 1, position.Y + 1),
                Color.Black, 0, new Vector2(0, font.LineSpacing / 2),
                fontScale, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.DrawString(font, text, position,
                color, 0, new Vector2(0, font.LineSpacing / 2),
                fontScale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draw the HUD, which consists of the score elements and the GAME OVER tag.
        /// </summary>
        void DrawHud()
        {
            // Draw Player Hud
            ScreenManager.SpriteBatch.Draw(GetWeaponTexture(playerOne),
                playerOneHUDPosition + new Vector2(33, 35), Color.White);
            DrawString(hudFont, playerOne.Score.ToString(),
                playerOneHUDPosition + new Vector2(123, 35), Color.White);
            DrawString(hudFont, playerOne.Name,
                playerOneHUDPosition + new Vector2(40, 1), Color.Blue);

            Rectangle rect = new Rectangle((int)playerOneHealthBarPosition.X,
                                    (int)playerOneHealthBarPosition.Y,
                                    (int)healthBarFullSize.X * playerOne.Health / 100,
                                    (int)healthBarFullSize.Y);
            Rectangle underRect = new Rectangle(rect.X, rect.Y, rect.Width + 1,
                                                rect.Height + 1);


            // Draw Computer Hud
            ScreenManager.SpriteBatch.Draw(GetWeaponTexture(playerTwo),
                playerTwoHUDPosition + new Vector2(33, 35), Color.White);
            DrawString(hudFont, playerTwo.Score.ToString(),
                playerTwoHUDPosition + new Vector2(123, 35), Color.White);
            DrawString(hudFont, playerTwo.Name,
                playerTwoHUDPosition + new Vector2(40, 1), Color.Red);

            rect = new Rectangle((int)playerTwoHealthBarPosition.X,
                                 (int)playerTwoHealthBarPosition.Y,
                                 (int)healthBarFullSize.X * playerTwo.Health / 100,
                                 (int)healthBarFullSize.Y);
            underRect = new Rectangle(rect.X, rect.Y, rect.Width + 1,
                                      rect.Height + 1);


            // Draw Wind direction
            string text = "WIND";
            Vector2 size = hudFont.MeasureString(text);
            Vector2 windarrowScale = new Vector2(wind.Y / 10, 1);


            DrawString(hudFont, text,
                       windArrowPosition - new Vector2(0, size.Y), Color.Black);
            if (wind.Y == 0)
            {
                text = "NONE";
                DrawString(hudFont, text, windArrowPosition, Color.Black);
            }

            // If first player turn
            if (isFirstPlayerTurn)
            {
                // Prepare first player prompt message
                text = "Release to Fire!";
                size = hudFont.MeasureString(text);
            }


            DrawString(hudFont, text,
                new Vector2(
                    ScreenManager.GraphicsDevice.Viewport.Width / 2 - size.X / 2,
                    ScreenManager.GraphicsDevice.Viewport.Height - size.Y),
                    Color.Green);

        }


        void DrawPlayerOne(GameTime gameTime)
        {
            if (!gameOver)
                playerOne.Draw(gameTime);
        }

        void DrawPlayerTwo(GameTime gameTime)
        {
            if (!gameOver)
                playerTwo.Draw(gameTime);
        }

        public GameplayScreen(bool twoHumans)
        {
            EnabledGestures = GestureType.FreeDrag |
                GestureType.DragComplete |
                GestureType.Tap;

            random = new Random();

        }

        /// <summary>
        /// Runs one frame of update for the game.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check it one of the players reached 5 and stop the game
            if ((playerOne.Catapult.GameOver || playerTwo.Catapult.GameOver) &&
                (gameOver == false))
            {
                gameOver = true;

                if (playerOne.Score > playerTwo.Score)
                {
                    // TODO: Play win sound 
                }
                else
                {
                    // TODO: Play lose sound
                }

                return;
            }

            if (changeTurn)
            {
                // Update wind
                wind = new Vector2(random.Next(-1, 2),
                    random.Next(minWind, maxWind + 1));

                // Set new wind value to the players and 
                playerOne.Catapult.Wind = playerTwo.Catapult.Wind =
                    wind.X > 0 ? wind.Y : -wind.Y;
                changeTurn = false;
            }

            // Update the players
            playerOne.Update(gameTime);
            playerTwo.Update(gameTime);

            // Updates the clouds position
            UpdateClouds(elapsed);

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        private void UpdateClouds(float elapsedTime)
        {
            // Move the clouds according to the wind
            int windDirection = wind.X > 0 ? 1 : -1;

            cloud1Position += new Vector2(24.0f, 0.0f) * elapsedTime *
                windDirection * wind.Y;
            if (cloud1Position.X > ScreenManager.GraphicsDevice.Viewport.Width)
                cloud1Position.X = -cloud1Texture.Width * 2.0f;
            else if (cloud1Position.X < -cloud1Texture.Width * 2.0f)
                cloud1Position.X = ScreenManager.GraphicsDevice.Viewport.Width;

            cloud2Position += new Vector2(16.0f, 0.0f) * elapsedTime *
                windDirection * wind.Y;
            if (cloud2Position.X > ScreenManager.GraphicsDevice.Viewport.Width)
                cloud2Position.X = -cloud2Texture.Width * 2.0f;
            else if (cloud2Position.X < -cloud2Texture.Width * 2.0f)
                cloud2Position.X = ScreenManager.GraphicsDevice.Viewport.Width;
        }

        /// <summary>
        /// Input helper method provided by GameScreen.  Packages up the various /// input values for ease of use.
        /// </summary>
        /// <param name="input">The state of the gamepads</param>
        public override void HandleInput(InputState input)
        {
            PlayerIndex player;

            if (input == null)
                throw new ArgumentNullException("input");

            if (input.IsNewKeyPress(Keys.F12, out player))
            {
                if (!Windows.UI.ViewManagement.ApplicationView
                        .GetForCurrentView().IsFullScreen)
                    Windows.UI.ViewManagement.ApplicationView
                        .GetForCurrentView().TryEnterFullScreenMode();
                else
                    Windows.UI.ViewManagement.ApplicationView
                        .GetForCurrentView().ExitFullScreenMode();
            }

            if (gameOver)
            {
                if (input.IsPauseGame())
                {
                    FinishCurrentGame();
                }

                if (input.IsNewKeyPress(Keys.Space, out player)
                    || input.IsNewKeyPress(Keys.Enter, out player))
                {
                    FinishCurrentGame();
                }

                if (input.IsNewGamePadButtonPress(Buttons.A, out player)
                    || input.IsNewGamePadButtonPress(Buttons.Start, out player))
                {
                    FinishCurrentGame();
                }

                //if (input.IsNewMouseButtonPress(MouseButtons.LeftButton,
                //                                out player))
                //{
                //    FinishCurrentGame();
                //}

                foreach (GestureSample gestureSample in input.Gestures)
                {
                    if (gestureSample.GestureType == GestureType.Tap)
                    {
                        FinishCurrentGame();
                    }
                }

                return;
            }

            if (input.IsPauseGame())
            {
                PauseCurrentGame();
            }
            else if (isFirstPlayerTurn &&
                (playerOne.Catapult.CurrentState == CatapultState.Idle ||
                    playerOne.Catapult.CurrentState == CatapultState.Aiming))
            {
                //Read keyboard input
                playerOne.HandleKeybordInput(input.CurrentKeyboardState);

                //Read gamepad input
                playerOne.HandleGamePadInput(input.CurrentGamePadState);

                //Read mouse input
                playerOne.HandleMouseInput(input.CurrentMouseState,
                                           input.LastMouseState);

                if (input.Gestures.Count > 0)
                {
                    // Read all available gestures
                    foreach (GestureSample gestureSample in input.Gestures)
                    {
                        if (gestureSample.GestureType == GestureType.FreeDrag)
                            isDragging = true;
                        else if (gestureSample.GestureType
                                 == GestureType.DragComplete)
                            isDragging = false;

                        playerOne.HandleInput(gestureSample);
                    }
                }
            }
        }


        private void FinishCurrentGame()
        {
            ExitScreen();
        }

        private void PauseCurrentGame()
        {
            // TODO: Pause the game
        }

    }
}


