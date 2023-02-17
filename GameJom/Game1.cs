using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;
using System;

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
        public static Point calculationScreenSize = new Point(3840, 2160);
        public static Rectangle ScreenBounds;
        public static int GameState = 1;
        public static bool Paused = false;
        public static MouseState mouseState;
        public int XMousePos;
        public int YMousePos;
        public static Texture2D BasicTexture;
        public static LevelClass clas = new LevelClass();

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
            // loads any squishing you would need to do to the game to not deform with different resolutions
            
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            double num = (double)graphics.PreferredBackBufferWidth / 3840;
            if (num > (double)graphics.PreferredBackBufferHeight / 2160)
            {
                num = (double)graphics.PreferredBackBufferHeight / 2160;
            }
            ScreenBounds = new Rectangle(
                (int)((graphics.PreferredBackBufferWidth - 3840 * num) / 2), 
                (int)((graphics.PreferredBackBufferHeight - 2160 * num) / 2), 
                (int)(3840 * num), (int)(2160 * num));
            ScreenSizeAdjustment = num;

            // graphical parameters

            graphics.ApplyChanges();
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>

        Texture2D Text1;
        Texture2D Text2;
        Texture2D Text3;
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            BasicTexture = Content.Load<Texture2D>("BasicShape");
            clas.Load("bad poggie"); 
            Text1 = Content.Load<Texture2D>("Font");
            Text2 = Content.Load<Texture2D>("font(hold)");
            Text3 = Content.Load<Texture2D>("font(pressed)");
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
            LinearAlgebruh.MatrixTransform(new float[,] { { 0, 1 }, { 1, 0 } }, new float[] { 1, 0 });
            base.Update(gameTime);

            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            // Background color
            GraphicsDevice.Clear(Color.Gray);
            if (GameState == 2)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            // Draw parameters
            AutomatedDraw MainCamera = new AutomatedDraw(ScreenBounds, new Point(Player.X + (Player.Width / 2), Player.Y + Player.Height / 2),  Color.White, GameState == 2, Parallax.ParallaxZoom(10));
            AutomatedDraw paralaxDraw = new AutomatedDraw(ScreenBounds, new Point(Player.X + Player.Width / 2, Player.Y + Player.Height / 2), Color.LightGray, GameState == 2, Parallax.ParallaxZoom(20));
            AutomatedDraw Base = new AutomatedDraw();

            //Fonts
            PrintManager textFormat = new PrintManager(Text2, 20, Color.White, new Point(100, 200));

            paralaxDraw.mDraw(new Rectangle(0, 0, 1000, 1000), BasicTexture);
            MainCamera.mDraw(MainCamera.RatioRectangle(new Vector2(0, 0.1f), new Vector2(0, 0.1f)), BasicTexture);
            MainCamera.mDraw(new Rectangle(-1500, -1500, 100, 100), BasicTexture);
            MainCamera.mDraw(Player, BasicTexture, Color.Gray);

            #region UI

            textFormat.Print(Base, XMousePos.ToString() + "  "+Base.DisplayRectangle(new Rectangle(XMousePos, YMousePos, 30, 40)).Y.ToString(), new Point());
            Button bobama = new Button(Base, textFormat, new Point(0, 200), "bobama", GameState == 2);
            bobama.TextButtonUpdate(Text3);
            if (bobama.pressedLeft)
            {
                GameState = 1;
            }

            Menu menu = new Menu(Base, textFormat, new string[] { "Start", "Level Editor", "Settings", "Credits", "Exit", "borgus" }, new Point(300, 300), 10, Text1, Text2, Text3, GameState == 1);
            menu.MenuUpdate();
            if (menu.buttons[0].PressedCheck())
            {
                GameState = 2;
            }
            if (menu.buttons[1].PressedCheck())
            {
                GameState = 3;
            }
            if (menu.buttons[3].PressedCheck())
            {
                textFormat.Print(Base, Text3, "JAck", new Point(1200, 1000));
            }
            if (menu.buttons[4].PressedCheck())
            {
                Exit();
            }


            Base.mDraw(new Rectangle(XMousePos, YMousePos, 30, 40), BasicTexture);
            spriteBatch.Draw(BasicTexture, new Rectangle(0, 0, calculationScreenSize.X, ScreenBounds.Top), Color.Black);
            spriteBatch.Draw(BasicTexture, new Rectangle(0, ScreenBounds.Bottom, calculationScreenSize.X, ScreenBounds.Top), Color.Black);
            spriteBatch.End();
            #endregion

            /*
            _3D_Because_Why_Not._3D_Renderer _3DEngine = new _3D_Because_Why_Not._3D_Renderer(100);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                _3DEngine.UpdateLocation(new Vector3(-0.05f, 0, 0));
                //_3DEngine.UpdateDirection(new Vector2(0.02f, 0));
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _3DEngine.UpdateLocation(new Vector3(0.05f, 0, 0));
                //_3DEngine.UpdateDirection(new Vector2(-0.02f, 0));
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                _3DEngine.UpdateLocation(new Vector3(0,  0, -0.05f));
                //_3DEngine.UpdateDirection(new Vector2(0, 0.02f));
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                _3DEngine.UpdateLocation(new Vector3(0,0, 0.05f));
                //_3DEngine.UpdateDirection(new Vector2(0, -0.02f));
            int depth = 10;
            int depth2 = 12;
            Vector3 corner1_1 = new Vector3(1, 1, depth);
            Vector3 corner1_2 = new Vector3(-1, 1, depth);
            Vector3 corner1_3 = new Vector3(-1, -1, depth);
            Vector3 corner1_4 = new Vector3(1, -1, depth);
            Vector3 corner2_1 = new Vector3(1, 1, depth2);
            Vector3 corner2_2 = new Vector3(-1, 1, depth2);
            Vector3 corner2_3 = new Vector3(-1, -1, depth2);
            Vector3 corner2_4 = new Vector3(1, -1, depth2);
            _3DEngine.renderLine(corner1_1, corner2_1, 3).DrawLine();
            _3DEngine.renderLine(corner1_2, corner2_2, 3).DrawLine();
            _3DEngine.renderLine(corner1_4, corner2_4, 3).DrawLine();
            _3DEngine.renderLine(corner1_3, corner2_3, 3).DrawLine();

            _3DEngine.renderLine(corner1_1, corner1_2, 3).DrawLine();
            _3DEngine.renderLine(corner1_1, corner1_4, 3).DrawLine();
            _3DEngine.renderLine(corner1_3, corner1_2, 3).DrawLine();
            _3DEngine.renderLine(corner1_3, corner1_4, 3).DrawLine();

            _3DEngine.renderLine(corner2_1, corner2_2, 3).DrawLine();
            _3DEngine.renderLine(corner2_1, corner2_4, 3).DrawLine();
            _3DEngine.renderLine(corner2_3, corner2_2, 3).DrawLine();
            _3DEngine.renderLine(corner2_3, corner2_4, 3).DrawLine();
            AutomatedLine line = new AutomatedLine(new AutomatedDraw());
            line.DrawLine(_3DEngine.renderLine(corner1_3, corner2_3, 3));
            */
            LineClass linedraw = new LineClass(new Point(1, 2), new Point(3, 3), 3);
            linedraw.Function(-10);
            //linedraw.DrawLine();
            //Base.draw(new Rectangle(300, 300, 1000, 300), PlayerTexture);
            base.Draw(gameTime);
        }
    }
}
