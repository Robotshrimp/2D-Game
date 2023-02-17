using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace GameJom
{
    public class Room : Game1
    {
        public enum DataChunks
        {
            room = 0,
            tiles = 1,
            backForeGround = 2,

        }
        public int[,] TileMap;
        public Rectangle RoomSize;
        public Point DesiredCameraLocation;
        public Room(string components, LevelClass preset)
        {
            
            string[] data = components.Split('.');

            // room data
            string[] vars = data[(int)DataChunks.room].Split(',');
            Rectangle room = new Rectangle (int.Parse(vars[0]), int.Parse(vars[1]), int.Parse(vars[2]), int.Parse(vars[3]));
            TileMap = new int[room.Height, room.Width];
            // tilemap data
            // use room dimenstions to determine where each tile go, this means tiles are stored as 1D not 2d array
            string[] tiles = data[(int)DataChunks.tiles].Split(',');
            int currentTile = 0;
            int currentRow = 0;
            foreach(string tile in tiles)
            {

                if (currentRow > TileMap.GetLength(0))
                    break;

                if (currentTile > TileMap.GetLength(1))
                {
                    currentRow += 1;
                    currentTile = 0;
                }


                TileMap[currentRow, currentTile] = int.Parse(tile);
                currentTile += 1;
            }


        }
        public void Edit(Point location, int value = 1) // Edits the tilemap
        {
            if(location.X < TileMap.GetLength(0) && location.X >= 0 && 
               location.Y < TileMap.GetLength(1) && location.Y >= 0)
            {
                TileMap[location.X, location.Y] = value;
            }
        }
        public void Resize(Point targetSize) // creates a new array that replaces the old one that retains the information
        {
            int[,] newArray = new int[targetSize.X, targetSize.Y];
            for(int n = 0; n < Math.Min(TileMap.GetLength(0), newArray.GetLength(0)); n++)
            {
                for (int m = 0; m < Math.Min(TileMap.GetLength(1), newArray.GetLength(1)); m++)
                {
                    newArray[n, m] = TileMap[n, m];
                }
            }
            TileMap = newArray;
        }
        public bool Clicked(Point Mousepos, bool Pressed) // checks to see if this room was clicked/selected
        {
            if (Pressed)
            {
                if (Mousepos.X > RoomSize.X & Mousepos.X < RoomSize.X + RoomSize.Width &
                    Mousepos.Y > RoomSize.Y & Mousepos.Y < RoomSize.Y + RoomSize.Height)
                {
                    return true;
                }
            }
            return false;
        }
        public string Save() // saves data into file format  
        {
            string saveData = "";
            saveData += RoomSize.X + "," + RoomSize.Y + "," + RoomSize.Width + "," + RoomSize.Height + ".";
            for (int n = 0; n < TileMap.GetLength(0); n++)
            {
                for (int m = 0; m < TileMap.GetLength(1); m++)
                {
                    saveData += TileMap[n, m] + ",";
                }
            }
            saveData.TrimEnd(',');
            saveData += ".";
            return ""; // place holder
        }
        /*
        List<List<StaticObject>> Objects;
        public void addObj(StaticObject NewItem)
        {
             if (Objects.Count < NewItem.layer) // adds new layers if needed
             {
                int n = NewItem.layer - Objects.Count;
                for (int m = NewItem.layer; m < n; m++)
                {
                    Objects.Add(new List<StaticObject>());
                }
             }
            Objects[NewItem.layer - 1].Add(NewItem);
        }
        public void removeObj(StaticObject Target)
        {
            Objects[Target.layer - 1].Remove(Target);
        }
        public void draw()
        {
            for (int n = Objects.Count - 1; n >= 0; n--)
            {
                for (int m = 0; 0 < Objects.Count; m++)
                {
                    Objects[n][m].drawOnce();
                }
            }
        }*/

        public void CalculationUpdate()// lists of class instances that need calculation updates, nothing should be drawn here
        {

        }
        public void DrawUpdate()// lists of class instances that need to be drawn, nothing should change in this function
        {
            
        }
    }
}
