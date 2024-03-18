using GameJom.classes.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    public class EditorSelectorScreen : ScreenFromat, IScreen
    {
        public static string name = "editorSelectorScreen";
        Folder UsedAssets;
        Texture2D FolderTexture;
        MouseState mouseState = new MouseState();
        GraphicsDevice graphicsDevice;


        Rectangle pointerLocation = new Rectangle();
        InputMap Inputs = new InputMap();
        public void Draw()
        {
            Inputs.Update();
            mouseState = Mouse.GetState();
            pointerLocation = new Rectangle((int)((float)mouseState.X / (float)Game1.ScreenSizeAdjustment), mouseState.Y, 1, 1);
            Camera BaseDraw = new Camera();

            graphicsDevice.Clear(Color.Black);
            if (Inputs.Inputed(true, true, inputs.up))
                BaseDraw.Draw(new Rectangle(100,100,1000,1000), (Texture2D)UsedAssets.Storage["BasicShape"]);
            BaseDraw.Draw(new Rectangle(pointerLocation.Location, new Point(30, 40)), (Texture2D)UsedAssets.Storage["Curser"]);
        }

        public void Initialize()
        {
            graphicsDevice = Game1.graphicsDevice;
            UsedAssets = AssetStorage.ContentAssets.PathToFolder("Content/Assets/UI Assets");
            FolderTexture = (Texture2D)UsedAssets.Storage["Folder"];
        }

        public void Update()
        {
        }
    }
}
