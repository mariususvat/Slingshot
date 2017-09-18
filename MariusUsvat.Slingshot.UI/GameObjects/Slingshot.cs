using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MariusUsvat.Slingshot.UI.GameObjects
{
    public class Slingshot
    {
        public Vector2 NearBranchPosition
        {
            get { return new Vector2(this.launchPoint.X - 12, this.launchPoint.Y - 4); }
        }

        public Vector2 FarBranchPosition
        {
            get { return new Vector2(this.launchPoint.X + 12, this.launchPoint.Y + 4); }
        }

        public Vector2 LaunchPoint
        {
            get { return this.launchPoint; }
        }

        public float DistanceDivider
        {
            get { return this.distanceDivider; }
        }

        private Texture2D texture;
        private Vector2 position;
        private Vector2 launchPoint;
        private float distanceDivider;

        public Slingshot(Vector2 position, Vector2 launchPoint, float distanceDivider)
        {
            this.position = position;
            this.launchPoint = launchPoint;
            this.distanceDivider = distanceDivider;
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
            Vector2 direction = new Vector2(this.launchPoint.X - p.Center.X, this.launchPoint.Y - p.Center.Y);
            float highestAbsValue = Math.Abs(Math.Abs(direction.X) > Math.Abs(direction.Y) ? direction.X : direction.Y);

            return new Vector2(direction.X / highestAbsValue, direction.Y / highestAbsValue);
        }

        public float CalculateLaunchSpeed(Projectile p)
        {
            Vector2 direction = new Vector2(this.launchPoint.X - p.Center.X, this.launchPoint.Y - p.Center.Y);
            float distance = (float) Math.Sqrt(Math.Pow(p.Center.X - this.launchPoint.X, 2) + Math.Pow(p.Center.Y - this.launchPoint.Y, 2));

            return MathHelper.Clamp(distance / this.distanceDivider, 0, Projectile.MAX_SPEED);
        }
    }
}
