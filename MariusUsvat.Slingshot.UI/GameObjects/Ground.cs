using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MariusUsvat.Slingshot.UI.GameObjects
{
    public class Ground
    {
        public Rectangle Boundries
        {
            get { return this.boundries; }
        }

        private Texture2D texture;
        private Rectangle boundries; // Used for collision
        private Vector2 size;
        private Vector2 position;

        public Ground()
        {
            this.boundries = new Rectangle(0, 0, 1280, 146);
            this.size = new Vector2(1280, 174);
            this.position = new Vector2(0, 720 - this.size.Y);
        }

        public void Initialize(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
