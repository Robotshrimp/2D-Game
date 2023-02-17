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
        public static double ScreenSizeAdjustment = 1;
        public static Rectangle ScreenBounds;
        public static Vector correctScreenSize = new Vector();
        public static Button button = new Button();
        public static int GameState = 1;
        public static bool Paused = false;
        public static MouseState mouseState;
        int XMousePos;
        int YMousePos;
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
            double num = (double)graphics.PreferredBackBufferWidth / 3840;
            if (num > (double)graphics.PreferredBackBufferHeight / 2160)
            {
                num = (double)graphics.PreferredBackBufferHeight / 2160;
            }
            correctScreenSize.X = (int)(graphics.PreferredBackBufferWidth / num);
            correctScreenSize.Y = (int)(graphics.PreferredBackBufferHeight / num);
            ScreenBounds = new Rectangle(
                (int)((correctScreenSize.X - 3840) / 2), 
                (int)((correctScreenSize.Y - 2160) / 2), 
                3840, 2160);
            ScreenSizeAdjustment = num;
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
            mouseState = Mouse.GetState();
            XMousePos = (int)(mouseState.X / ScreenSizeAdjustment);
            YMousePos = (int)(mouseState.Y / ScreenSizeAdjustment);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


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
            GraphicsDevice.Clear(Color.Gray);
            AutomatedDraw MainCamera = new AutomatedDraw(new Vector(Player.X + Player.Width / 2, Player.Y + Player.Height / 2),  Color.White, new Vector(0, -ScreenBounds.Y), GameState == 2, 1);
            MainCamera.draw(Player, PlayerTexture);
            MainCamera.draw(new Rectangle(0,0, 1000, 1000), PlayerTexture);
            MainCamera.draw(new Rectangle(-1500, -1500, 100, 100), PlayerTexture);
            // TODO: Add your drawing code here
            button.ButtonUpdate(new Rectangle(300 , 300, 1000, 300), PlayerTexture, GameState == 1);
            Color color = Color.White;
            if (button.Pressed)
            {
                GameState = 2;
                color = Color.Red;
            }
            AutomatedDraw Base = new AutomatedDraw(color, new Vector(0,0));

            AutomatedDraw unprocessed = new AutomatedDraw(Color.Black, new Vector(- ScreenBounds.X, - ScreenBounds.Y));

            unprocessed.draw(new Rectangle(XMousePos, YMousePos, 30, 40), PlayerTexture, Color.White);
            unprocessed.draw(new Rectangle(0, 0, correctScreenSize.X, ScreenBounds.Top), PlayerTexture);
            unprocessed.draw(new Rectangle(0, ScreenBounds.Bottom, correctScreenSize.X, ScreenBounds.Top), PlayerTexture);

            
            //Base.draw(new Rectangle(300, 300, 1000, 300), PlayerTexture);
            base.Draw(gameTime);
        }
    }
}
