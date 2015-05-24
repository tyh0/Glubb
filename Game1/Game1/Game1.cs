using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;   

        // Texture2D herringImage;

        Player player = new Player();
        Herring herring = new Herring();
        Orca orca = new Orca();
        Salmon salmon = new Salmon();

        // Herring[] herrings = new Herring[Herring.getMaxAmount()];
        List<Herring> herrings = new List<Herring>(Herring.getMaxAmount());
        // int sizeOfHerring = 0;
        int herringCounter = 101;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            
            player.Initialize(Content.Load<Texture2D>("Graphics\\salmon.png"));
            herring.Initialize(Content.Load<Texture2D>("Graphics\\herring.png"));
            orca.Initialize(Content.Load<Texture2D>("Graphics\\herring.png"));
            salmon.Initialize(Content.Load<Texture2D>("Graphics\\salmon.png"));
            herrings.Add(herring);
            // sizeOfHerring++;
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
            if ((herrings.Count < 10) && (herringCounter % 100 == 0))
            {
                Herring newHerring = new Herring();
                newHerring.Initialize(Content.Load<Texture2D>("Graphics\\herring.png"));
                herrings.Add(newHerring);
            }
            for (int i = 0; i < herrings.Count; i++)
            {
                herrings[i].Update();
                if ((herrings[i].getX() > GraphicsDevice.Viewport.Width) ||
                    (herrings[i].getY() > GraphicsDevice.Viewport.Height))
                {
                    herrings.Remove(herrings[i]);
                }
            }
            herringCounter++;
            salmon.Update();
            orca.Update();

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called to update the player's position.
        /// </summary>
        protected void UpdatePlayer()
        {
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                player.setX(player.getX() - player.getPlayerMoveSpeed());
                player.setLeft(true);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                player.setX(player.getX() + player.getPlayerMoveSpeed());
                player.setLeft(false);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up))

            {
                player.setY(player.getY() - player.getPlayerMoveSpeed());
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down))

            {
                player.setY(player.getY() + player.getPlayerMoveSpeed());
            }
            player.setX(MathHelper.Clamp(player.getX(), 0, GraphicsDevice.Viewport.Width - player.getWidth()/4));
            player.setY(MathHelper.Clamp(player.getY(), 0, GraphicsDevice.Viewport.Height - player.getHeight()/4));
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            player.Draw(spriteBatch);
            for (int i = 0; i < herrings.Count; i++)
            {
                herrings[i].Draw(spriteBatch);
            }
            salmon.Draw(spriteBatch);
            orca.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
