using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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
            player.Update();
            herring.Update();
            salmon.Update();
            orca.Update();

            base.Update(gameTime);
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
