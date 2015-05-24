using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Game1

{

    class Herring

    {
        int MAXAMOUNT = 10;
        Texture2D herringPic;

        public void Initialize(Texture2D texture)

        {
            herringPic = texture;
        }



        public void Update()

        {

        }



        public void Draw(SpriteBatch spriteBatch)

        {
            Vector2 v = new Vector2(0, 0);
            spriteBatch.Draw(herringPic, v , null, Color.White, 0f, v, 1f, SpriteEffects.None, 0f);
        }

    }

}
