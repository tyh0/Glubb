using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;   

        Texture2D herringImage;

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
            
            player.Initialize(Content.Load<Texture2D>("Graphics\\Herring.jpg"));
            herring.Initialize(Content.Load<Texture2D>("Graphics\\Herring.jpg"));
            orca.Initialize(Content.Load<Texture2D>("Graphics\\Herring.jpg"));
            salmon.Initialize(Content.Load<Texture2D>("Graphics\\Herring.jpg"));
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
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                player.setX(player.getX() + player.getPlayerMoveSpeed());
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up))

            {
                player.setY(player.getY() - player.getPlayerMoveSpeed());
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down))

            {
                player.setY(player.getY() + player.getPlayerMoveSpeed());
            }
            player.setX(MathHelper.Clamp(player.getX(), 0, GraphicsDevice.Viewport.Width - player.getWidth()));
            player.setY(MathHelper.Clamp(player.getY(), 0, GraphicsDevice.Viewport.Height - player.getHeight()));
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
            herring.Draw(spriteBatch);
            salmon.Draw(spriteBatch);
            orca.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
