using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJom
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static MouseState mouseState;
        public static Button button = new Button();
        public static double ScreenSizeAdjustment = 1;
        public static int GameState = 1;
        public static bool Paused = false;
        Texture2D PlayerTexture;
        Rectangle Player = new Rectangle(0, 0, 96, 96);
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            PlayerTexture = Content.Load<Texture2D>("BasicShape");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
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
            button.ButtonUpdate(new Rectangle(300, 300, 1000, 300), PlayerTexture, GameState == 1);
            if (button.Pressed)
            {
                GameState = 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Player.Y -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Player.Y += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Player.X -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Player.X += 10;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            AutomatedDraw MainCamera = new AutomatedDraw(new Vector(Player.X, Player.Y), new Vector(0,0), Color.White, GameState == 1, 1);
            MainCamera.draw(Player, PlayerTexture);
            MainCamera.draw(new Rectangle(0,0, 1000, 1000), PlayerTexture);
            MainCamera.draw(new Rectangle(-1500, -1500, 100, 100), PlayerTexture);
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
