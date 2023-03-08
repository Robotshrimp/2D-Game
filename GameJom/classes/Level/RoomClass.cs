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
            grphicalFlare = 2,

        }
        public List<int[,]> TileMaps;
        public Rectangle RoomSize;
        public Point DesiredCameraLocation;
        public Room(string components, LevelClass preset)
        {
            if (components != "")
            {
                string[] data = components.Split('.');
                // room data
                string[] vars = data[(int)DataChunks.room].Split(',');
                Rectangle room = new Rectangle (int.Parse(vars[0]), int.Parse(vars[1]), int.Parse(vars[2]), int.Parse(vars[3]));
                // tilemap data
                // use room dimenstions to determine where each tile go, this means tiles are stored as 1D not 2d array
                string[] totalRoomTileData = data[(int)DataChunks.tiles].Split(',');
                int currentMap = 0;
                int currentTile = 0;
                int currentRow = 0;
                foreach(string tilemapData in totalRoomTileData)
                {
                    TileMaps[currentMap] = new int[room.Height,room.Width];
                    string[] tiles = tilemapData.Split(' ');
                    foreach (string tile in totalRoomTileData)
                    {
                        if (currentRow > room.Height)
                            break;
                        if (currentTile > room.Width)
                        {
                            currentRow += 1;
                            currentTile = 0;
                        }
                        TileMaps[currentMap][currentRow, currentTile] = int.Parse(tile);
                        currentTile += 1;
                    }
                    currentMap += 1;
                }
            }
        }
        public void fill(ref int[,] tileMap, Rectangle fill, int fillNum)
        {
            for(int n = 0; n < fill.Height; n++)
            {
                for (int m = 0; m < fill.Width; m++)
                {
                    tileMap[fill.Y + n, fill.X + m] = fillNum;
                }
            }
        }
        // playtime
        public void load()
        {
            // add ingame interations for when the room is loaded
        }

        // editing functions, ignore if coding playtime features
        protected void Edit(Point location, int selectedTileMap, int value = 1) // Edits the tilemap
        {
            if(location.Y < TileMaps[selectedTileMap].GetLength(0) && location.Y >= 0 && 
               location.X < TileMaps[selectedTileMap].GetLength(1) && location.X >= 0)
            {
                TileMaps[selectedTileMap][location.Y, location.X] = value;
            }
        }
        protected void Resize(Point targetSize, int selectedTileMap) // creates a new array that replaces the old one that retains the information
        {
            int[,] newArray = new int[targetSize.Y, targetSize.X];
            for(int n = 0; n < Math.Min(TileMaps[selectedTileMap].GetLength(0), newArray.GetLength(0)); n++)
            {
                for (int m = 0; m < Math.Min(TileMaps[selectedTileMap].GetLength(1), newArray.GetLength(1)); m++)
                {
                    newArray[n, m] = TileMaps[selectedTileMap][n, m];
                }
            }
            TileMaps[selectedTileMap] = newArray;
        }
        public string Save() // saves data into file format 
        {
            string saveData = "";
            saveData += RoomSize.X + "," + RoomSize.Y + "," + RoomSize.Width + "," + RoomSize.Height + ".";
            for (int h = 0; h < TileMaps.Count; h++)
            {
                for (int n = 0; n < TileMaps[h].GetLength(0); n++)
                {
                    for (int m = 0; m < TileMaps[h].GetLength(1); m++)
                    {
                        saveData += TileMaps[h][n, m] + " ";
                    }
                }
                saveData.TrimEnd(' ');
                saveData += ',';
            }

            saveData.TrimEnd(',');
            saveData += ".";
            return saveData; // place holder
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
        }*/ //object code, could be useful for non interactable texture

        public void CalculationUpdate()// lists of class instances that need calculation updates, nothing should be drawn here
        {

        }
        public void DrawUpdate()// lists of class instances that need to be drawn, nothing should change in this function
        {
            
        }
    }
}
