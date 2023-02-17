using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJom
{
    public class LevelClass: Game1

    { 
        List<Room> Rooms= new List<Room>();
        List<Texture2D> GraphicsAssets;
        public LevelClass()
        {}
        public void Load(string Folder) // input any level information from folder to program runtime
        {
            //this code creates the level template if no level of such name exists
            string location = @"Content/Levels/" + Folder; // expected level folder location
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
                Rooms.Add(new Room(Room, this));
            }
        }
        public void AddRoom()
        {
            // TODO: takes a set of cordnet and place a new room at that location(might be good to allow rooms to overlap to have mid room transitions
        }
        public void Runtime()
        {
            foreach (Room room in Rooms)
            {
                if (room.Clicked(Mouse.GetState().Position, Mouse.GetState().MiddleButton == ButtonState.Pressed))
                {
                    // TODO: make pop up system and require confermation before deletion
                }
            }
            // TODO: updates the rooms
        }
        public void Save(string saveLocation) // output any level information from runtime to folder
        {
            // code to convert every room to text format
            string roomfile = Rooms[0].Save();
            for(int n = 1; n < Rooms.Count; n++)
            {
                roomfile += '&' + Rooms[n].Save();
            }
            // there's actrually no reason to save anything other than the room files, the texture files are only needed to load in and has no change done to them in runtime 
        }
    }
}
