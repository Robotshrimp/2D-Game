using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms.VisualStyles;
using System.Collections.Generic;
using System.IO;

namespace GameJom
{
    class HomeScreen : ScreenFromat, IScreen 
    {

        //test values, remove later








        public static string name = "HomeScreen";
        Folder GetAssets = new Folder();
        bool pause = false;
        

        Texture2D BlankTexture;
        Texture2D PointerTexture;
        FontPreset Font;
        float roati = 0;
        float test = 0;
        GraphicsDevice graphicsDevice;


        AfterImage afterImage = Global.afterImage;

        public void Initialize()
        {
            GetAssets.SubFolders.Add("Fonts", AssetStorage.ContentAssets.SearchForFolder("Fonts"));
            GetAssets.MergeFolderStorage(AssetStorage.ContentAssets.Storage);
            GetAssets.MergeFolderStorage(AssetStorage.ContentAssets.PathToFolder("Content/Assets/UI Assets").Storage);
            BlankTexture = (Texture2D)GetAssets.Storage["BasicShape"];
            PointerTexture = (Texture2D)GetAssets.Storage["Curser"];
            Font = new FontPreset(GetAssets.SubFolders["Fonts"].SubFolders["TestFont"]);
            graphicsDevice = Game1.graphicsDevice;
            #region Afterimage trail color
            afterImage.ColorKeys.Add(new ColorFrameData(Color.Red, 1));
            afterImage.ColorKeys.Add(new ColorFrameData(Color.Orange, 4));
            afterImage.ColorKeys.Add(new ColorFrameData(Color.Yellow, 4));
            afterImage.ColorKeys.Add(new ColorFrameData(Color.Green, 4));
            afterImage.ColorKeys.Add(new ColorFrameData(Color.Blue, 4));
            afterImage.ColorKeys.Add(new ColorFrameData(Color.Purple, 4));
            afterImage.ColorKeys.Add(new ColorFrameData(Color.White, 100));


            #endregion

            File.Create(@"test").Dispose();
            File.WriteAllText(@"test", "");
        }
        float bgGradient = 10;
        float changeRate = 0.1f;
        MouseState mouseState = new MouseState();
        Rectangle startButton, editButton, pointerLocation = new Rectangle();

        Camera BaseDraw = new Camera();
        public void Draw()
        {

            afterImage.DrawTrail();
            mouseState = Mouse.GetState();
            pointerLocation = new Rectangle((int)((float)mouseState.X / (float)Game1.ScreenSizeAdjustment), mouseState.Y, 1, 1);
            if (!BaseDraw.CustomEffects.Contains(afterImage))
                BaseDraw.CustomEffects.Add(afterImage);
            Font.AdvancedPresets(BaseDraw, 96, Color.White, 8);
            startButton = Font.Print("Start", new Point(100, 100));
            editButton = Font.Print("Edit", new Point(100, 250));


            _3D_Because_Why_Not.Renderer3D rend = new _3D_Because_Why_Not.Renderer3D(BaseDraw);
            #region 3D grapnics

            rend.UpdateLocation(new Vector3(0, 0 - 16 * (float)Math.Sin(roati), 16 - 16 * (float)Math.Cos(roati)));
            rend.UpdateDirection(new Vector3(0, 0 - 16 * (float)Math.Sin(roati), 16 - 16 * (float)Math.Cos(roati)));
            _3D_Because_Why_Not.Cuboid cube = new _3D_Because_Why_Not.Cuboid(rend);
            float suroundingsize = 1.5f;
            int segments = 1;
            cube.DrawCuboid(new Vector3(2, -2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, -test - (float)Math.PI / 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, -2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, test, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, 2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, -test, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, 2, 14), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, test + (float)Math.PI / 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, -2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, test - (float)Math.PI / 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, -2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, -test, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, 2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, test, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, 2, 18), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, -test + (float)Math.PI / 3, (float)Math.PI / 4));

            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(3, 3, 3), segments, 3, new Vector3((float)Math.PI / 5, test, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(2.5f, 2.5f, 2.5f), segments, 3, new Vector3((float)Math.PI / 5, test * 2, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(2, 2, 2), segments, 3, new Vector3((float)Math.PI / 5, test * 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 16), new Vector3(1.5f, 1.5f, 1.5f), segments, 3, new Vector3((float)Math.PI / 5, test * 4, (float)Math.PI / 4));

            //cube.DrawCuboid(new Vector3(0 - 16 * (float)Math.Sin(broati)*y, 0 - 16 * (float)Math.Sin(roati), 16 - 16 * (z*y)), new Vector3(1.5f, 1.5f, 1.5f), 5, 3, new Vector3(-broati, 0, roati));

            double n = 0.4;
            cube.DrawCuboid(new Vector3(-3 * (float)Math.Cos(test + 4 * n), 3 * (float)Math.Cos(test + 4 * n), 16 + 3 * (float)Math.Sin(test + 4 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -test * 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-4 * (float)Math.Cos(test + 3 * n), 3 * (float)Math.Cos(test + 3 * n), 16 + 4 * (float)Math.Sin(test + 3 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -test * 4, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-5 * (float)Math.Cos(test + 2 * n), 3 * (float)Math.Cos(test + 2 * n), 16 + 5 * (float)Math.Sin(test + 2 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -test * 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-6 * (float)Math.Cos(test + 1 * n), 3 * (float)Math.Cos(test + 1 * n), 16 + 6 * (float)Math.Sin(test + 1 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -test * 6, (float)Math.PI / 4));

            cube.DrawCuboid(new Vector3(+3 * (float)Math.Cos(test + 4 * n), +3 * (float)Math.Cos(test + 4 * n), 16 - 3 * (float)Math.Sin(test + 4 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -test * 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+4 * (float)Math.Cos(test + 3 * n), +3 * (float)Math.Cos(test + 3 * n), 16 - 4 * (float)Math.Sin(test + 3 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -test * 4, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+5 * (float)Math.Cos(test + 2 * n), +3 * (float)Math.Cos(test + 2 * n), 16 - 5 * (float)Math.Sin(test + 2 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -test * 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+6 * (float)Math.Cos(test + 1 * n), +3 * (float)Math.Cos(test + 1 * n), 16 - 6 * (float)Math.Sin(test + 1 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -test * 6, (float)Math.PI / 4));
            #endregion

            graphicsDevice.Clear(new Color((int)bgGradient, (int)bgGradient, (int)bgGradient));
            BaseDraw.Draw(new Rectangle(pointerLocation.Location, new Point(30, 40)), PointerTexture, color: Color.White);
        }


        public void Update()
        {
            if (!pause)
            {
                BaseDraw.Update();
                roati += (float).002;
                test += (float).01;
                if (OverlapCheck.Overlapped(startButton, pointerLocation))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        pause = true;
                    }
                }
                if (OverlapCheck.Overlapped(editButton, pointerLocation))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        addScreen.Add(EditorSelectorScreen.name);
                        removeScreen.Add(name);
                    }
                }
                if (bgGradient >= 30)
                    changeRate = -0.1f;
                if (bgGradient <= 1)
                    changeRate = 0.1f;
                bgGradient += changeRate;
            }
        }
    }
}
