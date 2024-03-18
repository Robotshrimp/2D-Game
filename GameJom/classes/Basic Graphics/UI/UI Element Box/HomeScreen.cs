using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms.VisualStyles;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using GameJom._3D_Because_Why_Not;

namespace GameJom
{
    class HomeScreen : ScreenFromat, IScreen 
    {
        //turn this into a screen type with specific screen settings stored in jsons


        // screen data
        public static string name = "HomeScreen";
        Folder GetAssets = new Folder(); // not a place to save to files, does not directly take from contentassets. 
        bool pause = false;
        

        Texture2D BlankTexture;
        Texture2D PointerTexture;
        FontPreset Font;
        float Movement3D = 0;
        float AnimationSpeed3D = 0;
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
            ColorFrameData color1 = new ColorFrameData();
            color1.Frames = 1; color1.FrameColor = Color.DarkRed;
            ColorFrameData color2 = new ColorFrameData();
            color2.Frames = 10; color2.FrameColor = Color.PaleGoldenrod;
            afterImage.ColorKeys.Add(color1);
            afterImage.ColorKeys.Add(color2);

            #endregion
            /*
             for each game object, load in during initialize as json, each draw element would have it's location, color, etc, listed.
            new elements would be added using a seperate dev edit UI overlap listed under a different screen that is hard coded, the job of that screen would be
            to generate the json files that comprise a normal screen. this special screen should be removed on release with option to re-enable by editing a json to set dev mode to true
            the problem now is how do i add in functions using only json files, perhaps coding in functions to be called by name in jsons
            */

            string jsonString = JsonSerializer.Serialize(afterImage);
            JsonSerializer.Deserialize<AfterImage>(jsonString);
            BaseDraw.CustomEffects.Add(JsonSerializer.Deserialize<AfterImage>(jsonString));
            //File.Create(@"test").Dispose();
            //File.WriteAllText(@"test", "");
        }
        float bgGradient = 10;
        float changeRate = 0.1f;
        MouseState mouseState = new MouseState();
        Rectangle startButton, editButton, pointerLocation = new Rectangle();

        Camera BaseDraw = new Camera();
        public void Draw()
        {
            BaseDraw.EffectDraw();
            mouseState = Mouse.GetState();
            pointerLocation = new Rectangle((int)((float)mouseState.X / (float)Game1.ScreenSizeAdjustment), mouseState.Y, 1, 1);
            Font.AdvancedPresets(BaseDraw, 96, Color.White, 8);
            startButton = Font.Print("Start", new Point(100, 100));
            editButton = Font.Print("Edit", new Point(100, 250));

            Renderer3D testrenderer = new Renderer3D(new Camera());
            Camera cam = new Camera();

            _3D_Because_Why_Not.Renderer3D rend = new _3D_Because_Why_Not.Renderer3D(BaseDraw);
            #region 3D grapnics
            // use functions for orbiting with params for orbiting height, speed, etc. should be the most straight forward way to add json compatability
            rend.UpdateLocation(new Vector3(0, 0 - 16 * (float)Math.Sin(Movement3D), 0 - 16 * (float)Math.Cos(Movement3D)));
            rend.UpdateDirection(new Vector3(0, 0 - (float)Math.PI * (float)Math.Sin(Movement3D), 0 - (float)Math.PI * (float)Math.Sin(Movement3D)));
            rend.LookAt(new Vector3());



            rend.InstaniatePlaneRendering(new Point(215, 135));




            _3D_Because_Why_Not.Cuboid cube = new _3D_Because_Why_Not.Cuboid(rend);
            float suroundingsize = 1.5f;
            int segments = 1;
            cube.DrawCuboid(new Vector3(2, -2, -2), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D - (float)Math.PI / 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, -2, -2), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, AnimationSpeed3D, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, 2, -2), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, 2, -2), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, AnimationSpeed3D + (float)Math.PI / 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, -2, 2), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, AnimationSpeed3D - (float)Math.PI / 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, -2, 2), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(2, 2, 2), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, AnimationSpeed3D, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-2, 2, 2), new Vector3(suroundingsize, suroundingsize, suroundingsize), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D + (float)Math.PI / 3, (float)Math.PI / 4));

            cube.DrawCuboid(new Vector3(0, 0, 0), new Vector3(3, 3, 3), segments, 3, new Vector3((float)Math.PI / 5, AnimationSpeed3D, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 0), new Vector3(2.5f, 2.5f, 2.5f), segments, 3, new Vector3((float)Math.PI / 5, AnimationSpeed3D * 2, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 0), new Vector3(2, 2, 2), segments, 3, new Vector3((float)Math.PI / 5, AnimationSpeed3D * 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(0, 0, 0), new Vector3(1.5f, 1.5f, 1.5f), segments, 3, new Vector3((float)Math.PI / 5, AnimationSpeed3D * 4, (float)Math.PI / 4));

            //cube.DrawCuboid(new Vector3(0 - 16 * (float)Math.Sin(broati)*y, 0 - 16 * (float)Math.Sin(roati), 16 - 16 * (z*y)), new Vector3(1.5f, 1.5f, 1.5f), 5, 3, new Vector3(-broati, 0, roati));

            double n = 0.4;
            cube.DrawCuboid(new Vector3(-3 * (float)Math.Cos(AnimationSpeed3D + 4 * n), 3 * (float)Math.Cos(AnimationSpeed3D + 4 * n),  3 * (float)Math.Sin(AnimationSpeed3D + 4 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D * 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-4 * (float)Math.Cos(AnimationSpeed3D + 3 * n), 3 * (float)Math.Cos(AnimationSpeed3D + 3 * n),  4 * (float)Math.Sin(AnimationSpeed3D + 3 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D * 4, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-5 * (float)Math.Cos(AnimationSpeed3D + 2 * n), 3 * (float)Math.Cos(AnimationSpeed3D + 2 * n), 5 * (float)Math.Sin(AnimationSpeed3D + 2 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D * 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(-6 * (float)Math.Cos(AnimationSpeed3D + 1 * n), 3 * (float)Math.Cos(AnimationSpeed3D + 1 * n), 6 * (float)Math.Sin(AnimationSpeed3D + 1 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D * 6, (float)Math.PI / 4));

            cube.DrawCuboid(new Vector3(+3 * (float)Math.Cos(AnimationSpeed3D + 4 * n), +3 * (float)Math.Cos(AnimationSpeed3D + 4 * n),  3 * (float)Math.Sin(AnimationSpeed3D + 4 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D * 3, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+4 * (float)Math.Cos(AnimationSpeed3D + 3 * n), +3 * (float)Math.Cos(AnimationSpeed3D + 3 * n), 4 * (float)Math.Sin(AnimationSpeed3D + 3 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D * 4, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+5 * (float)Math.Cos(AnimationSpeed3D + 2 * n), +3 * (float)Math.Cos(AnimationSpeed3D + 2 * n), 5 * (float)Math.Sin(AnimationSpeed3D + 2 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D * 5, (float)Math.PI / 4));
            cube.DrawCuboid(new Vector3(+6 * (float)Math.Cos(AnimationSpeed3D + 1 * n), +3 * (float)Math.Cos(AnimationSpeed3D + 1 * n), 6 * (float)Math.Sin(AnimationSpeed3D + 1 * n)), new Vector3(1, 1, 1), segments, 3, new Vector3((float)Math.PI / 5, -AnimationSpeed3D * 6, (float)Math.PI / 4));
            rend.RenderAll(new Rectangle(0, 0, 3840, 2160));
            #endregion

            graphicsDevice.Clear(new Color((int)bgGradient, (int)bgGradient, (int)bgGradient)); // store in screen folder as the screen's own json
            BaseDraw.Draw(new Rectangle(pointerLocation.Location, new Point(30, 40)), PointerTexture, color: Color.White);


        }
        public void Update()
        {
            if (!pause)
            {
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


                // temperary, should be stored as json
                BaseDraw.Update();
                Movement3D += (float).005;
                AnimationSpeed3D += (float).01;
                if (bgGradient >= 30)
                    changeRate = -0.1f;
                if (bgGradient <= 1)
                    changeRate = 0.1f;
                bgGradient += changeRate;
            }
        }
    }
}
