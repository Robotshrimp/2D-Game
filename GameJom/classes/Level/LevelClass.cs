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
        protected List<Texture2D> GraphicsAssets;
        public LevelClass()
        {}
        public void Load(string Folder, ContentManager Content) // input any level information from folder to program runtime
        {
            //this code creates the level template if no level of such name exists
            location = @"Content/Levels/" + Folder; // expected level folder location
            if (!Directory.Exists(location)) // level folder, texture folder, and room text file creation
            {
                Directory.CreateDirectory(location);
                File.Create(location + @"/Rooms.txt");
                Directory.CreateDirectory(location + @"/GraphicsAssets");
            }

            // accessing texture files by foreach loop
            string[] files = Directory.GetFiles(location + @"/GraphicsAssets");
            foreach (string file in files)
            {
                GraphicsAssets.Add(Content.Load<Texture2D>(file));
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
        }
        public void Runtime()
        {
            foreach (Room room in Rooms) // systematicly updates each room, any room updates MUST be here with exception of special rooms that must be singled out
            {
                room.RuntimeUpdate();
                
            }
            // TODO: updates the rooms
        }
    }
}
