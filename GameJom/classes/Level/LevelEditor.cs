using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    class LevelEditor : LevelClass
    {
        int SelectedRoom;
        int SelectedTileMap;
        Rectangle SelectedTiles;
        int brush;
        public void AddRoom()
        {
            // TODO: takes a set of cordnet and place a new room at that location(might be good to allow rooms to overlap to have mid room transitions
        }
        public void RemoveRoom(int index /*index of room to be deleted*/)
        {
            // TODO: delete specified room
        }
        private void Save(string saveLocation) // output any level information from runtime to folder. IMPORTAINT: must only be useable in level editor
        {
            // code to convert every room to text format
            string roomfile = Rooms[0].Save();
            for (int n = 1; n < Rooms.Count; n++)
            {
                roomfile += '&' + Rooms[n].Save();
            }
            // there's actrually no reason to save anything other than the room files, the texture files are only needed to load in and has no change done to them in runtime 
        }
    }
}
