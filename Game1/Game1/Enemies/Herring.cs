using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Game1

{

    class Herring

    {
        private static int MAXAMOUNT = 10;
        Texture2D herringPic;
        int x;
        int y;

        public void Initialize(Texture2D texture)

        {
            herringPic = texture;
            x = 0;
            y = 0;
        }

        public void Update()

        {
            x = x + 5;
            y = y + 1;
        }

        public void Draw(SpriteBatch spriteBatch)

        {
            Vector2 v = new Vector2();
            v.X = x;
            v.Y = y;
            spriteBatch.Draw(herringPic, v, Color.White);
            // spriteBatch.Draw(herringPic, v , null, Color.White, 0f, v, 1f, SpriteEffects.None, 0f);
        }

        public int getX() { return x; }
        public int getY() { return y; }
        public static int getMaxAmount() { return MAXAMOUNT; }
    }

}
