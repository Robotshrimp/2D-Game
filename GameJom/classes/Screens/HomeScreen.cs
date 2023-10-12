using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;

namespace GameJom
{
    class HomeScreen : IScreen
    {

        //test values, remove later
        bool pause = false;








        Folder usedAssets = new Folder();
        
        Texture2D BlankTexture;
        FontPreset Font;
        float roati = 0;
        float test = 0;
        IScreen targetScreen; // denotes the screen to switch to if not null
        bool endSelf = false;

        GraphicsDevice graphicsDevice;

        public void Initialize()
        {
            usedAssets.SubFolders.Add("Fonts", AssetStorage.ContentAssets.SearchForFolder("Fonts"));
            usedAssets.AddFolderStorage(AssetStorage.ContentAssets.Storage);
            BlankTexture = (Texture2D)usedAssets.Storage["BasicShape"];
            Font = new FontPreset(usedAssets.SubFolders["Fonts"].SubFolders["TestFont"]);
            graphicsDevice = Game1.graphicsDevice;
        }
        float bgGradient = 10;
        float changeRate = 0.1f;
        MouseState mouseState = new MouseState();
        Rectangle startButton = new Rectangle();
        Rectangle editButton = new Rectangle();
        Rectangle pointerLocation = new Rectangle();
        public void Draw()
        {
            mouseState = Mouse.GetState();
            pointerLocation = new Rectangle((int)((float)mouseState.X / (float)Game1.ScreenSizeAdjustment), mouseState.Y, 1, 1);
            AutomatedDraw BaseDraw = new AutomatedDraw();
            Font.AdvancedPresets(BaseDraw, 96, Color.White, 8);
            targetScreen = null;
            startButton = Font.Print("Start", new Point(100, 100));
            editButton = Font.Print("Edit", new Point(100, 250));


            _3D_Because_Why_Not.Renderer3D rend = new _3D_Because_Why_Not.Renderer3D(BaseDraw);
            #region 3D grapnics

            rend.UpdateLocation(new Vector3(0, 0 - 16 * (float)Math.Sin(roati), 16 - 16 * (float)Math.Cos(roati)));
            _3D_Because_Why_Not.Cuboid cube = new _3D_Because_Why_Not.Cuboid(rend);
            float suroundingsize = 1.5f;
            cube.DrawCuboid(new Vector3(2, -2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3((float)Math.PI / 5, -test - (float)Math.PI / 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, -2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3((float)Math.PI / 5, test, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, 2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3((float)Math.PI / 5, -test, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, 2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3((float)Math.PI / 5, test + (float)Math.PI / 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, -2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3((float)Math.PI / 5, test - (float)Math.PI / 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, -2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3((float)Math.PI / 5, -test, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, 2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3((float)Math.PI / 5, test, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, 2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), 5, 3, new Vector3((float)Math.PI / 5, -test + (float)Math.PI / 3, (float)Math.PI / 4));

            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(3, 3, 3), 5, 3, new Vector3((float)Math.PI / 5, test, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(2.5f, 2.5f, 2.5f), 5, 3, new Vector3((float)Math.PI / 5, test * 2, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(2, 2, 2), 5, 3, new Vector3((float)Math.PI / 5, test * 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(1.5f, 1.5f, 1.5f), 5, 3, new Vector3((float)Math.PI / 5, test * 4, (float)Math.PI / 4));

            //cube.DrawCuboid(new Vector3(0 - 16 * (float)Math.Sin(broati)*y, 0 - 16 * (float)Math.Sin(roati), 16 - 16 * (z*y)), new Vector3(1.5f, 1.5f, 1.5f), 5, 3, new Vector3(-broati, 0, roati));

            double n = 0.4;
            cube.DrawCuboid(new Vector3(-3 * (float)Math.Cos(test + 4 * n), 3 * (float)Math.Cos(test + 4 * n), 16 + 3 * (float)Math.Sin(test + 4 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3((float)Math.PI / 5, -test * 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-4 * (float)Math.Cos(test + 3 * n), 3 * (float)Math.Cos(test + 3 * n), 16 + 4 * (float)Math.Sin(test + 3 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3((float)Math.PI / 5, -test * 4, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-5 * (float)Math.Cos(test + 2 * n), 3 * (float)Math.Cos(test + 2 * n), 16 + 5 * (float)Math.Sin(test + 2 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3((float)Math.PI / 5, -test * 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-6 * (float)Math.Cos(test + 1 * n), 3 * (float)Math.Cos(test + 1 * n), 16 + 6 * (float)Math.Sin(test + 1 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3((float)Math.PI / 5, -test * 6, (float)Math.PI / 4));

            cube.DrawCuboid(new Vector3(+3 * (float)Math.Cos(test + 4 * n), +3 * (float)Math.Cos(test + 4 * n), 16 - 3 * (float)Math.Sin(test + 4 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3((float)Math.PI / 5, -test * 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+4 * (float)Math.Cos(test + 3 * n), +3 * (float)Math.Cos(test + 3 * n), 16 - 4 * (float)Math.Sin(test + 3 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3((float)Math.PI / 5, -test * 4, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+5 * (float)Math.Cos(test + 2 * n), +3 * (float)Math.Cos(test + 2 * n), 16 - 5 * (float)Math.Sin(test + 2 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3((float)Math.PI / 5, -test * 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+6 * (float)Math.Cos(test + 1 * n), +3 * (float)Math.Cos(test + 1 * n), 16 - 6 * (float)Math.Sin(test + 1 * n)), new Vector3(1, 1, 1), 5, 3, new Vector3((float)Math.PI / 5, -test * 6, (float)Math.PI / 4));

            #endregion

            graphicsDevice.Clear(new Color((int)bgGradient, (int)bgGradient, (int)bgGradient));
            BaseDraw.Draw(new Rectangle(pointerLocation.Location, new Point(30, 40)), BlankTexture);
        }


        public void Update()
        {
            if (!pause)
            {
                roati += (float).01;
                test += (float).01;
                if (OverlapCheck.Overlapped(startButton, pointerLocation))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        pause = true;
                    }
                }
                if (bgGradient >= 30)
                    changeRate = -0.1f;
                if (bgGradient <= 1)
                    changeRate = 0.1f;
                bgGradient += changeRate;
            }
        }
        public IScreen newScreen()
        {
            IScreen placeHolderScreen = targetScreen;
            targetScreen = null;
            return placeHolderScreen;
        }
        public bool removeSelf()
        {
            return endSelf;
        }
    }
}
