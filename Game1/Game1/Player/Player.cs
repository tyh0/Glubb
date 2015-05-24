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

        public void Initialize(Texture2D texture)

        {
            playerMoveSpeed = 10;
            x = 0;
            y = 0;
            width = texture.Width;
            height = texture.Height;
        }



        public void Update()

        {


        }



        public void Draw(SpriteBatch spriteBatch)

        {

        }

        public int getX() { return x; }
        public int getY() { return y; }
        public int getPlayerMoveSpeed() { return playerMoveSpeed; }
        public int getWidth() { return width; }
        public int getHeight() { return height; }
        public void setX(int x) { this.x = x; }
        public void setY(int y) { this.y = y; }


    }

}
