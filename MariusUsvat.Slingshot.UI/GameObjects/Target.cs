using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MariusUsvat.Slingshot.UI.GameObjects
{
    public class Target
    {
        private Texture2D texture;
        private Vector2 position;

        public Target()
        {
            this.position = new Vector2(900, 535);
        }

        public void Initialize(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(0, 0), 0.7f, SpriteEffects.None, 0);
        }
    }
}
