using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Glubb
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TheGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public TheGame()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                // TODO: Implement preferred dimensions
            };

            Content.RootDirectory = "Content";

            // Limit frame rate to 30 fps.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            //If not running on the phone, show mouse cursor
            if (!Windows.Foundation.Metadata.ApiInformation
                   .IsApiContractPresent("Windows.Phone.PhoneContract", 1))
                IsMouseVisible = true;
            else
            {
                graphics.SupportedOrientations =
                 DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
            }

            // TODO: Add screen scale
            // TODO: Add screen manager
            // TODO: Add gameplay screen
            // TODO: Add additional screens
            // TODO: Add audio manager
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
