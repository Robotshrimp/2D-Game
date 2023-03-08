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
        Texture2D Griddy;
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            BasicTexture = Content.Load<Texture2D>("BasicShape");
            clas.Load("bad poggie"); 
            Text1 = Content.Load<Texture2D>("font(hold)");
            Text2 = Content.Load<Texture2D>("font(hold)");
            Text3 = Content.Load<Texture2D>("font(pressed)");
            Griddy = Content.Load<Texture2D>("transparent");
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

        float test = 0;
        float roati = 0;
        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            // Background color
            GraphicsDevice.Clear(Color.Black);
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



            spriteBatch.End();

            #region 3d

            _3D_Because_Why_Not.Cuboid cube = new _3D_Because_Why_Not.Cuboid();
            //cube.DrawCuboid(new Vector3((float)Math.Cos(test), (float)Math.Sin(test), (float)Math.Sin(test) + 15), new Vector3(2, 2, 2), 100, 5, 3, new Vector2(-test, test));
            if (Keyboard.GetState().IsKeyDown(Keys.H))
            {
                roati += (float).03;
            }
            cube.DrawCuboid(new Vector3(1, -3, 8), new Vector3(2, 2, 2), 100, 5, 3, new Vector3(-test - (float)Math.PI / 3, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-3, -3, 8), new Vector3(2, 2, 2), 100, 5, 3, new Vector3(test , (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(1, 1, 8), new Vector3(2, 2, 2), 100, 5, 3, new Vector3(-test, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-3, 1, 8), new Vector3(2, 2, 2), 100, 5, 3, new Vector3(test + (float)Math.PI / 3, (float)Math.PI / 5, (float)Math.PI / 4));

            cube.DrawCuboid(new Vector3(-1, -1, 7), new Vector3(2, 2, 2), 100, 5, 3, new Vector3(test, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-1, -1, 8), new Vector3(2, 2, 2), 100, 5, 3, new Vector3(test * 2, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-1, -1, 9), new Vector3(2, 2, 2), 100, 5, 3, new Vector3(test * 3, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-1, -1, 10), new Vector3(2, 2, 2), 100, 5, 3, new Vector3(test * 4, (float)Math.PI / 5, (float)Math.PI / 4));

            //cube.DrawCuboid(new Vector3(-(float)0.5 + 3 * (float)Math.Cos(test * 16), -(float)0.5, 10 + 3 * (float)Math.Sin(test * 16)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 3, (float)Math.PI / 5, (float)Math.PI / 4));
            //cube.DrawCuboid(new Vector3(-(float)0.5 + 4 * (float)Math.Cos(test * 12), -(float)0.5, 10 + 4 * (float)Math.Sin(test * 12)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 4, (float)Math.PI / 5, (float)Math.PI / 4));
            //cube.DrawCuboid(new Vector3(-(float)0.5 + 5 * (float)Math.Cos(test * 8), -(float)0.5, 10 + 5 * (float)Math.Sin(test * 8)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 5, (float)Math.PI / 5, (float)Math.PI / 4));
            //cube.DrawCuboid(new Vector3(-(float)0.5 + 6 * (float)Math.Cos(test * 4), -(float)0.5, 10 + 6 * (float)Math.Sin(test * 4)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 6, (float)Math.PI / 5, (float)Math.PI / 4));
            double n = 0.4;
            cube.DrawCuboid(new Vector3(-(float)0.5 - 3 * (float)Math.Cos(test + 4 * n), -(float)0.5 + 3 * (float)Math.Cos(test + 4 * n), 10 + 3 * (float)Math.Sin(test + 4 * n)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 3, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-(float)0.5 - 4 * (float)Math.Cos(test + 3 * n), -(float)0.5 + 3 * (float)Math.Cos(test + 3 * n), 10 + 4 * (float)Math.Sin(test + 3 * n)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 4, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-(float)0.5 - 5 * (float)Math.Cos(test + 2 * n), -(float)0.5 + 3 * (float)Math.Cos(test + 2 * n), 10 + 5 * (float)Math.Sin(test + 2 * n)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 5, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-(float)0.5 - 6 * (float)Math.Cos(test + 1 * n), -(float)0.5 + 3 * (float)Math.Cos(test + 1 * n), 10 + 6 * (float)Math.Sin(test + 1 * n)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 6, (float)Math.PI / 5, (float)Math.PI / 4));

            cube.DrawCuboid(new Vector3(-(float)0.5 + 3 * (float)Math.Cos(test + 4 * n), -(float)0.5 + 3 * (float)Math.Cos(test + 4 * n), 10 - 3 * (float)Math.Sin(test + 4 * n)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 3, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-(float)0.5 + 4 * (float)Math.Cos(test + 3 * n), -(float)0.5 + 3 * (float)Math.Cos(test + 3 * n), 10 - 4 * (float)Math.Sin(test + 3 * n)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 4, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-(float)0.5 + 5 * (float)Math.Cos(test + 2 * n), -(float)0.5 + 3 * (float)Math.Cos(test + 2 * n), 10 - 5 * (float)Math.Sin(test + 2 * n)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 5, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-(float)0.5 + 6 * (float)Math.Cos(test + 1 * n), -(float)0.5 + 3 * (float)Math.Cos(test + 1 * n), 10 - 6 * (float)Math.Sin(test + 1 * n)), new Vector3(1, 1, 1), 100, 5, 3, new Vector3(-test * 6, (float)Math.PI / 5, (float)Math.PI / 4));

            test += (float)0.01;

            #endregion

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);


            #region UI

            //textFormat.Print(Base, XMousePos.ToString() + "  "+Base.DisplayRectangle(new Rectangle(XMousePos, YMousePos, 30, 40)).Y.ToString() + " " + roati, new Point());
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

            InfiniTexture inf = new InfiniTexture(MainCamera);
            inf.griddify(new Point(96, 96), Griddy, new Point());

            Base.mDraw(new Rectangle(XMousePos, YMousePos, 30, 40), BasicTexture);
            spriteBatch.Draw(BasicTexture, new Rectangle(0, 0, calculationScreenSize.X, ScreenBounds.Top), Color.Black);
            spriteBatch.Draw(BasicTexture, new Rectangle(0, ScreenBounds.Bottom, calculationScreenSize.X, ScreenBounds.Top), Color.Black);
            spriteBatch.End();
            #endregion

            
            //linedraw.DrawLine();
            //Base.draw(new Rectangle(300, 300, 1000, 300), PlayerTexture);
            base.Draw(gameTime);
        }
    }
}
