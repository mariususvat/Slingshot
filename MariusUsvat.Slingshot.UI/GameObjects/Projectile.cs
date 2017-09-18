using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MariusUsvat.Slingshot.UI.GameObjects
{
    public class Projectile
    {
        public static float MAX_SPEED = 28f;

        public bool IsFired { get { return isFired; } }
        public Vector2 Position { get { return position; } }
        public Vector2 Center { get { return new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2); } }

        private Texture2D texture;
        private Vector2 position;
        private Vector2 direction;
        private float speed;

        private float gravitationalForces;

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
            this.direction = direction;
            this.speed = speed;

            this.isFired = true;
        }

        public void Update(Rectangle groundBoundries)
        {
            this.isOnTheGround = position.Y >= 720 - groundBoundries.Height;

            if (!this.isOnTheGround)
            {
                this.gravitationalForces += SlingshotGame.GRAVITATIONAL_ACCELERATION;

                this.speed = MathHelper.Clamp(this.speed - SlingshotGame.FRICTION_DECELERATION, 0, float.MaxValue);
                this.position = new Vector2(
                    this.position.X + (this.direction.X * this.speed),
                    MathHelper.Clamp(this.position.Y + (this.direction.Y * this.speed + this.gravitationalForces), float.MinValue, 720 - groundBoundries.Height)
                );
            }
        }

        public void UpdateUnfired(Vector2 mousePosition, Vector2 launchPoint, float distanceDivider)
        {
            float maxLaunchDistance = Projectile.MAX_SPEED * distanceDivider;
            float actualDistance = (float) Math.Sqrt(Math.Pow(mousePosition.X - launchPoint.X, 2) + Math.Pow(mousePosition.Y - launchPoint.Y, 2));

            if (actualDistance > maxLaunchDistance)
            {
                Vector2 launchDirection = new Vector2(mousePosition.X - launchPoint.X, mousePosition.Y - launchPoint.Y);
                launchDirection.Normalize();
                
                this.position = new Vector2(launchPoint.X + launchDirection.X * maxLaunchDistance, launchPoint.Y + launchDirection.Y * maxLaunchDistance);
                Console.WriteLine("{0} Out of bounds", actualDistance);
            }
            else
            {
                this.position = new Vector2(mousePosition.X, mousePosition.Y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
        }
    }
}
