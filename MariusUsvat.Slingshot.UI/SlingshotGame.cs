using MariusUsvat.Slingshot.UI.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MariusUsvat.Slingshot.UI
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SlingshotGame : Game
    {
        public static float GRAVITATIONAL_ACCELERATION = 0f;
        public static float FRICTION_DECELERATION = 0f;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game objects
        Texture2D background;
        Ground ground;
        GameObjects.Slingshot slingshot;
        Target target;

        List<Projectile> projectiles;
        Texture2D projectileTexture;
        Texture2D strapTexture;
        Vector2 slingshotFarBranchPosition;
        Vector2 slingshotNearBranchPosition;

        Texture2D cursorArrow;
        Texture2D cursorHand;
        Texture2D cursorCurrent;

        MouseState currentMouseState;
        MouseState previousMouseState;

        public SlingshotGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

            Window.Title = "Slingshot";

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferMultiSampling = true;
            GraphicsDevice.PresentationParameters.MultiSampleCount = 8;
            graphics.ApplyChanges();

            ground = new Ground();
            slingshot = new GameObjects.Slingshot();
            target = new Target();
            projectiles = new List<Projectile>();

            slingshotNearBranchPosition = new Vector2(157, 500);
            slingshotFarBranchPosition = new Vector2(181, 508);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("background");
            ground.Initialize(Content.Load<Texture2D>("ground"));
            slingshot.Initialize(Content.Load<Texture2D>("slingshot"));
            target.Initialize(Content.Load<Texture2D>("target"));

            projectileTexture = Content.Load<Texture2D>("projectile");
            strapTexture = Content.Load<Texture2D>("strap");

            cursorArrow = Content.Load<Texture2D>("cursor_arrow");
            cursorHand = Content.Load<Texture2D>("cursor_hand");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (currentMouseState != null)
                previousMouseState = currentMouseState;

            currentMouseState = Mouse.GetState();
            Vector2 currentMousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            cursorCurrent = cursorArrow;

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                projectiles.Add(new Projectile(projectileTexture, currentMousePosition));
            }
            if (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                foreach(Projectile p in projectiles)
                    if (!p.IsFired)
                        p.Fire(slingshot.CalculateLaunchDirection(p), 40f);
            }

            foreach (Projectile p in projectiles)
                p.Update(currentMousePosition);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 720), Color.White);
            ground.Draw(spriteBatch);
            target.Draw(spriteBatch);

            foreach(Projectile p in projectiles)
            {
                if (!p.IsFired)
                {
                    DrawLineBetween(spriteBatch, strapTexture, p.Center, slingshotFarBranchPosition, Color.White);
                    p.Draw(spriteBatch);
                    DrawLineBetween(spriteBatch, strapTexture, p.Center, slingshotNearBranchPosition, Color.White);
                }
                else
                    p.Draw(spriteBatch);
            }

            slingshot.Draw(spriteBatch);

            spriteBatch.Draw(cursorCurrent, new Vector2(currentMouseState.X, currentMouseState.Y), Color.White);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void DrawLineBetween(SpriteBatch spriteBatch, Texture2D texture, Vector2 origin, Vector2 destination, Color color)
        {
            Vector2 direction = destination - origin;
            var angle = (float) Math.Atan2(direction.Y, direction.X);
            float distance = Vector2.Distance(origin, destination);

            spriteBatch.Draw(texture, origin, new Rectangle((int) origin.X + 3, (int) origin.Y + 3, (int) distance, 6), Color.White, angle, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }
    }
}
