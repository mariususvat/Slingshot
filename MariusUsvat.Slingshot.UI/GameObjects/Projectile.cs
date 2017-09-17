using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MariusUsvat.Slingshot.UI.GameObjects
{
    public class Projectile
    {
        public bool IsFired { get { return isFired; } }
        public Vector2 Position { get { return position; } }
        public Vector2 Center { get { return new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2); } }

        private Texture2D texture;
        private Vector2 position;
        private Vector2 speed;

        private bool isOnTheGround;
        private bool isFired;

        public Projectile(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

            this.isFired = false;
        }

        public void Fire(Vector2 direction, float speed)
        {
            this.speed = new Vector2(direction.X * speed, direction.Y * speed);
            this.isFired = true;
        }

        public void Update(Vector2 mousePosition)
        {
            this.isOnTheGround = position.Y >= 720 - 146;
            
            if (!isFired)
                this.position = new Vector2(mousePosition.X - this.texture.Width / 2, mousePosition.Y - this.texture.Height / 2);

            if (isFired && !this.isOnTheGround)
            {
                this.speed = new Vector2(MathHelper.Clamp(this.speed.X - SlingshotGame.FRICTION_DECELERATION, 0, float.MaxValue), this.speed.Y - SlingshotGame.GRAVITATIONAL_ACCELERATION);
                this.position = new Vector2(this.position.X + speed.X, MathHelper.Clamp(this.position.Y - speed.Y, float.MinValue, 720 - 145));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
        }
    }
}
