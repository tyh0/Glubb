using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Game1

{

    class Player

        

    {
        private int playerMoveSpeed;
        private int x;
        private int y;
        private int width;
        private int height;
        private bool left;
        private Vector2 coords;
        private Vector2 origin;
        private Texture2D texture;

        public void Initialize(Texture2D texture)

        {
            playerMoveSpeed = 10;
            x = 0;
            y = 0;
            left = false;
            width = texture.Width;
            height = texture.Height;
            coords = new Vector2();
            origin = new Vector2(0, 0);
            this.texture = texture;
        }



        public void Update()

        {


        }



        public void Draw(SpriteBatch spriteBatch)

        {
            coords.X = x;
            coords.Y = y;
            if (left)
            {
                spriteBatch.Draw(texture, coords, null, Color.White, 0f, origin, .25f, SpriteEffects.FlipHorizontally, 0f);
            }else
            {
                spriteBatch.Draw(texture, coords, null, Color.White, 0f, origin, .25f, SpriteEffects.None, 0f);
            }
        }

        public int getX() { return x; }
        public int getY() { return y; }
        public int getPlayerMoveSpeed() { return playerMoveSpeed; }
        public int getWidth() { return width; }
        public int getHeight() { return height; }
        public void setX(int x) { this.x = x; }
        public void setY(int y) { this.y = y; }
        public void setLeft(bool b) { this.left = b; }


    }

}
