using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;
using System;
using System.IO;

namespace GameJom
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static GraphicsDevice graphicsDevice;
        public static SpriteBatch spriteBatch;

        public static double ScreenSizeAdjustment = 1;
        public static readonly Point calculationScreenSize = new Point(3840, 2160);
        public static Rectangle ScreenBounds;

        public static int GameState = 1;
        public enum states
        {
            menu = 1,
            playArea = 2,
            editor = 3,
            levelSelect= 4,
        }
        public readonly Rectangle GridSize = new Rectangle(0,0,96,96);
        public static bool Paused = false;
        public static MouseState mouseState;
        public int XMousePos;
        public int YMousePos;
        public static Texture2D BasicTexture;
        public static LevelClass clas = new LevelClass();

        Rectangle Player = new Rectangle(0, 0, 96, 96);
        Point editorcenter= new Point(0,0);
        double editorZoom = 1;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
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
            this.IsMouseVisible = false;
            base.Initialize();
        }
        public static Texture2D Text1;
        Texture2D Text2;
        Texture2D Text3;
        public static Texture2D Griddy;
        public static Dictionary<string, Texture2D> Assets;
       
        protected override void LoadContent()
        {
            editor.Load("bad poggie", Content);
            // Create a new SpriteBatch, which can be used to draw textures.
            string[] files = Directory.GetFiles(@"Content/Assets");
            foreach (string file in files)
            {
                Assets.Add(file, Content.Load<Texture2D>(file));
            }
            BasicTexture = Content.Load<Texture2D>("BasicShape");
            clas.Load("bad poggie", Content); 
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
        AutomatedDraw EditorGraphics;
        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            XMousePos = (int)(mouseState.X / ScreenSizeAdjustment);
            YMousePos = (int)(mouseState.Y / ScreenSizeAdjustment);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                editor.Save();
                Exit();

            }
            if (GameState == (int)states.editor)
            {


                int num = 10;
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    num *= 2;
                int movement = EditorGraphics.LenUnScale(num);
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    editorcenter.Y -= movement;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    editorcenter.Y += movement;
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    editorcenter.X -= movement;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    editorcenter.X += movement;
                if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                    editorZoom += .02;
                if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                    editorZoom -= .02;
            }
            if (GameState == (int)states.playArea)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    Player.Y -= 10;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    Player.Y += 10;
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    Player.X -= 10;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    Player.X += 10;
            }

            base.Update(gameTime);

            
        }

        float test = 0;
        float roati = 0;
        float broati = 0;
        LevelEditor editor = new LevelEditor();

        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            // Background color
            GraphicsDevice.Clear(Color.Black);
            if (GameState == (int)states.playArea)
            {
                GraphicsDevice.Clear(Color.White);
            }
            else if (GameState == (int)states.editor)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            // Draw parameters
            AutomatedDraw MainCamera = new AutomatedDraw(ScreenBounds, new Point(Player.X + (Player.Width / 2), Player.Y + Player.Height / 2), Color.White, GameState == (int)states.playArea, Parallax.ParallaxZoom(10));
            AutomatedDraw paralaxDraw = new AutomatedDraw(ScreenBounds, new Point(Player.X + Player.Width / 2, Player.Y + Player.Height / 2), Color.LightGray, GameState == (int)states.playArea, Parallax.ParallaxZoom(20));
            EditorGraphics = new AutomatedDraw(ScreenBounds, editorcenter, Color.White, GameState == (int)states.editor, editorZoom);
            AutomatedDraw Base = new AutomatedDraw();

            //MainCamera.Draw(new Rectangle(0, 0, 1000, 1000), BasicTexture);
            MainCamera.Draw(MainCamera.RatioRectangle(new Vector2(0, 0.1f), new Vector2(0, 0.1f)), BasicTexture);
            MainCamera.Draw(new Rectangle(-1500, -1500, 100, 100), BasicTexture);
            //MainCamera.Draw(Player, BasicTexture, Color.Gray);


            clas.Runtime(MainCamera, new Rectangle(0, 0, 96, 96));




            EditorGraphics = new AutomatedDraw(ScreenBounds, editorcenter, Color.White, GameState == (int)states.editor, editorZoom);







            if (GameState == (int)states.editor)
            {
                editor.Edit(new Rectangle(0, 0, 96, 96), EditorGraphics);

            }

            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);


            #region UI


            //Fonts
            PrintManager textFormat = new PrintManager(20, Color.White, new Point(40, 80));
            //textFormat.Print(Base, XMousePos.ToString() + "  "+Base.DisplayRectangle(new Rectangle(XMousePos, YMousePos, 30, 40)).Y.ToString() + " " + roati, new Point());
            Button bobama = new Button(Base, textFormat, new Point(0, 200), "bobama", GameState == (int)states.playArea);
            bobama.TextButtonUpdate(Text3);
            if (bobama.pressedLeft)
            {
                GameState = (int)states.menu;
            }

            AutomatedDraw MainMenu = new AutomatedDraw(GameState == (int)states.menu);
            Menu menu = new Menu(MainMenu, textFormat, new string[] { "Start", "Level Editor", "Settings", "Credits", "Exit", "borgus" }, new Point(300, 300), 10);
            menu.Initialize(Text3, Text2, Text3);
            menu.MenuUpdate();
            if (menu.check("Start"))
            {
                GameState = (int)states.playArea;
            }
            if (menu.check("Level Editor"))
            {
                GameState = (int)states.editor;
            }
            if (menu.check("Settings"))
            {
                textFormat.Print(Base, Text3, "JAck", new Point(1200, 1000));
            }
            if (menu.check("Exit"))
            {
                Exit();
            }
            #region 3d
            _3D_Because_Why_Not.Renderer3D rend = new _3D_Because_Why_Not.Renderer3D(MainMenu);
            rend.UpdateDirection(new Vector3(0, roati, -roati));
            //rend.UpdateDirection(new Vector3(0, broati, - roati));
            float z = (float)Math.Cos(broati);
            float y = (float)Math.Cos(roati);

            rend.UpdateLocation(new Vector3(0, 0, -20));
            //rend.UpdateLocation(new Vector3(0 - 16 * (float)Math.Sin(broati) * y, 0 - 16 * (float)Math.Sin(roati), 16 - 16 * (z * y)));
            rend.UpdateLocation(new Vector3(0, 0 - 16 * (float)Math.Sin(roati), 16 - 16 * (float)Math.Cos(roati)));
            _3D_Because_Why_Not.Cuboid cube = new _3D_Because_Why_Not.Cuboid(rend);
            //cube.DrawCuboid(new Vector3((float)Math.Cos(test), (float)Math.Sin(test), (float)Math.Sin(test) + 15), new Vector3(2, 2, 2), 100, 5, 3, new Vector2(-test, test));
            roati += (float).001;
            test += (float).001;
            /*
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                roati += (float).03;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                roati += -(float).03;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                broati += (float).03;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                broati += -(float).03;
            }
            */
            float suroundingsize = 1.5f;
            cube.DrawCuboid(new Vector3(2, -2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3(-test - (float)Math.PI / 3, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, -2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3(test, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, 2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3(-test, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, 2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3(test + (float)Math.PI / 3, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, -2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3(test - (float)Math.PI / 3, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, -2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3(-test, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, 2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3(test, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, 2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3(-test + (float)Math.PI / 3, (float)Math.PI / 5, (float)Math.PI / 4));

            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(3, 3, 3), 5, 3, new Vector3(test, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(2.5f, 2.5f, 2.5f), 5, 3, new Vector3(test * 2, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(2, 2, 2), 5, 3, new Vector3(test * 3, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(1.5f, 1.5f, 1.5f), 5, 3, new Vector3(test * 4, (float)Math.PI / 5, (float)Math.PI / 4));

            //cube.DrawCuboid(new Vector3(0 - 16 * (float)Math.Sin(broati)*y, 0 - 16 * (float)Math.Sin(roati), 16 - 16 * (z*y)), new Vector3(1.5f, 1.5f, 1.5f), 5, 3, new Vector3(-broati, 0, roati));

            double n = 0.4;
            cube.DrawCuboid(new Vector3(-3 * (float)Math.Cos(test + 4 * n), 3 * (float)Math.Cos(test + 4 * n), 16 + 3 * (float)Math.Sin(test + 4 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3(-test * 3, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-4 * (float)Math.Cos(test + 3 * n), 3 * (float)Math.Cos(test + 3 * n), 16 + 4 * (float)Math.Sin(test + 3 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3(-test * 4, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-5 * (float)Math.Cos(test + 2 * n), 3 * (float)Math.Cos(test + 2 * n), 16 + 5 * (float)Math.Sin(test + 2 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3(-test * 5, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-6 * (float)Math.Cos(test + 1 * n), 3 * (float)Math.Cos(test + 1 * n), 16 + 6 * (float)Math.Sin(test + 1 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3(-test * 6, (float)Math.PI / 5, (float)Math.PI / 4));

            cube.DrawCuboid(new Vector3(+3 * (float)Math.Cos(test + 4 * n), +3 * (float)Math.Cos(test + 4 * n), 16 - 3 * (float)Math.Sin(test + 4 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3(-test * 3, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+4 * (float)Math.Cos(test + 3 * n), +3 * (float)Math.Cos(test + 3 * n), 16 - 4 * (float)Math.Sin(test + 3 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3(-test * 4, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+5 * (float)Math.Cos(test + 2 * n), +3 * (float)Math.Cos(test + 2 * n), 16 - 5 * (float)Math.Sin(test + 2 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3(-test * 5, (float)Math.PI / 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+6 * (float)Math.Cos(test + 1 * n), +3 * (float)Math.Cos(test + 1 * n), 16 - 6 * (float)Math.Sin(test + 1 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3(-test * 6, (float)Math.PI / 5, (float)Math.PI / 4));


            #endregion


            Base.Draw(new Rectangle(XMousePos, YMousePos, 15, 20), BasicTexture);
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
