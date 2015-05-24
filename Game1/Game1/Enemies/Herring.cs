using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1

{

    class Herring

    {
        private static int MAXAMOUNT = 10;
        Texture2D herringPic;
        int x;
        int y;
        int moveSpeedX;
        int moveSpeedY;
        int randomCounter;
        int dirX;
        int dirY;

        public void Initialize(Texture2D texture)

        {
            herringPic = texture;
            x = 0;
            y = 0;
            Random rndX = new Random();
            Random rndY = new Random();
            moveSpeedX = rndX.Next(1, 11);
            moveSpeedY = rndY.Next(1, 8);
            randomCounter = 101;
            dirX = 1;
            dirY = 1;

        }

        public void Update()

        {
            
            if (dirX == 1)
            {
                x = x + moveSpeedX;
            }
            else
            {
                x = x - moveSpeedX;
            }
            if (dirY == 1)
            {
                y = y + moveSpeedY;
            }
            else
            {
                y = y - moveSpeedY;
            }
            if (randomCounter % 100 == 0)
            {
                Random rndXDir = new Random();
                Random rndYDir = new Random();
                int xDir = rndXDir.Next(1, 5);
                int yDir = rndXDir.Next(1, 5);
                if (xDir <= 2)
                {
                    dirX = -1;
                }
                else
                {
                    dirX = 1;
                }
                if (yDir <= 2)
                {
                    dirY = -1;
                }
                else
                {
                    dirY = 1;
                }
            }
            randomCounter++;
            
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
