﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;
        

        Player player = new Player();
        Herring herring = new Herring();
        Orca orca = new Orca();
        Salmon salmon = new Salmon();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            
            player.Initialize();
            herring.Initialize();
            orca.Initialize();
            salmon.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
 
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // call UpdatePlayer instead of player.Update() because player needs to take
            // user input, which were defined as fields in this class
            UpdatePlayer();
            herring.Update();
            salmon.Update();
            orca.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called to update the player's position.
        /// </summary>
        protected void UpdatePlayer()
        {
            player.x += currentGamePadState.ThumbSticks.Left.X * player.playerMoveSpeed;
            player.y -= currentGamePadState.ThumbSticks.Left.Y * player.playerMoveSpeed;
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                player.x -= player.playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                player.x += player.playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed)

            {
                player.y -= player.playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)

            {
                player.y += player.playerMoveSpeed;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            player.Draw();
            herring.Draw();
            salmon.Draw();
            orca.Draw();
            base.Draw(gameTime);
        }
    }
}
