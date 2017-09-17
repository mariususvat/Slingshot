using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MariusUsvat.Slingshot.UI.GameObjects
{
    public class Slingshot
    {
        public Vector2 LaunchPoint
        {
            get { return new Vector2(169, 504); }
        }

        private Texture2D texture;
        private Vector2 position;

        public Slingshot()
        {
            this.position = new Vector2(150, 485);
        }

        public void Initialize(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(0,0), 0.4f, SpriteEffects.None, 0);
        }

        public Vector2 CalculateLaunchDirection(Projectile p)
        {
            Vector2 hypothenuseVector = new Vector2(p.Center.X - this.LaunchPoint.X, p.Center.Y - this.LaunchPoint.Y);
            double alphaAngle = Math.Atan(Math.Tan(hypothenuseVector.Y / hypothenuseVector.X));
            Console.WriteLine(alphaAngle * 180 / Math.PI);

            return new Vector2((float) Math.Cos(alphaAngle), (float) Math.Sin(alphaAngle));
        }
    }
}
