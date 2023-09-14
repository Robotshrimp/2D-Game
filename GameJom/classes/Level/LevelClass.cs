using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GameJom
{
    public class LevelClass
    { 
        protected List<Room> Rooms= new List<Room>();
        int CurrentRoom;
        protected string location;
        public static List<Texture2D> DecorationAssets = new List<Texture2D>();
        public static List<Texture2D> TileSetAssets = new List<Texture2D>();
        public static List<Texture2D> BackgroundAssets = new List<Texture2D>();
        
        public LevelClass()
        {}
        public void Load(string Folder, ContentManager Content) // input any level information from folder to program runtime
        {
            //this code creates the level template if no level of such name exists
            location = @"Content/Levels/" + Folder; // expected level folder location
            if (!Directory.Exists(location)) // level folder, texture folder, and room text file creation
                Directory.CreateDirectory(location);
            if (!File.Exists(location + @"/Rooms.txt"))
                File.Create(location + @"/Rooms.txt").Dispose();

            // accessing texture files by foreach loop
            DirectoryInfo d = new DirectoryInfo(@"Content/Assets/TileSets");
            if (!d.Exists) 
            {
                d.Create();
            }
            FileInfo[] files = d.GetFiles("*.xnb");
            foreach (FileInfo file in files) 
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);//gets name of file
                TileSetAssets.Add(Content.Load<Texture2D>(d.FullName + "/" + key));
            }
            // accessing room data through file split and foreach loop
            string data = File.ReadAllText(location + @"/Rooms.txt");
            string[] roomData = data.Split('&');
            foreach (string Room in roomData)
            {
                if(Room != "")
                {
                    Rooms.Add(new Room(Room, this));
                }
            }
            foreach (Room room in Rooms) 
            {
                room.Reload();
            }
        }
        public void Runtime(AutomatedDraw drawParam, Rectangle grid)
        {
            foreach (Room room in Rooms) // systematicly updates each room, any room updates MUST be here with exception of special rooms that must be singled out
            {
                room.RuntimeUpdate();
                room.Draw(drawParam,  grid);
            }
            // TODO: updates the rooms
        }
    }
}
