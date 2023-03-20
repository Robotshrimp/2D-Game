using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameJom
{
    public class Room
    {
        public enum DataChunks
        {
            room = 0,
            tiles = 1,
            grphicalFlare = 2,

        }
        public List<int[,]> TileMaps = new List<int[,]>();
        public Rectangle RoomSize;
        public Point DesiredCameraLocation;
        public LevelClass Preset;
        public Room(string components, LevelClass preset)
        {
            this.Preset = preset;
            if (components != "")
            {
                string[] data = components.Split('.');
                // room data
                string[] vars = data[(int)DataChunks.room].Split(',');
                RoomSize = new Rectangle (int.Parse(vars[0]), int.Parse(vars[1]), int.Parse(vars[2]), int.Parse(vars[3]));
                // tilemap data
                // use room dimenstions to determine where each tile go, this means tiles are stored as 1D not 2d array
                string[] totalRoomTileData = data[(int)DataChunks.tiles].Split(',');
                int currentMap = 0;
                int currentTile = 0;
                int currentRow = 0;
                foreach(string tilemapData in totalRoomTileData)
                {
                    TileMaps.Add(new int[RoomSize.Height,RoomSize.Width]);
                    string[] tiles = tilemapData.Split(' ');
                    foreach (string tile in tiles)
                    {
                        if (currentRow >= RoomSize.Height)
                            break;
                        if (currentTile >= RoomSize.Width)
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
        public Room(Rectangle Size, LevelClass preset)
        {
            this.Preset = preset;
            int[,] initializationMap = new int[Size.Height, Size.Width];
            Fill(ref initializationMap, new Rectangle(new Point(), Size.Size), 0);
            TileMaps.Add(initializationMap);
            RoomSize = Size;
        }
        public static void Fill(ref int[,] tileMap, Rectangle fill, int fillNum)
        {
            if (fill.X >= 0 &&
               fill.Y >= 0 &&
               fill.Right < tileMap.GetLength(1) &
               fill.Bottom < tileMap.GetLength(0))
            {
                for (int n = 0; n < fill.Height; n++)
                {
                    for (int m = 0; m < fill.Width; m++)
                    {
                        tileMap[fill.Y + n, fill.X + m] = fillNum;
                    }
                }
            }
        }
        // playtime
        public void RuntimeUpdate()
        {
            // add ingame interations for when the room is loaded
        }
        public void Draw(AutomatedDraw drawParam, GridTexture gridParam, Texture2D tileTexture)
        {
            foreach (int[,] tilemap in TileMaps)
            {
                for (int n = 0; n < tilemap.GetLength(0); n++)
                {
                    for (int m = 0; m < tilemap.GetLength(1); m++)
                    {
                        if (tilemap[n,m] == 1)
                        {
                            drawParam.Draw(gridParam.GridToScreen(new Rectangle(RoomSize.Location + new Point(m,n), new Point (1,1))), tileTexture);
                        }
                    }
                }
            }
        }
        // editing functions, ignore if coding playtime features
        public void Edit(Point location, int selectedTileMap = 0, int value = 1) // Edits the tilemap
        {
            //if(RoomSize.Intersects(new Rectangle(location + RoomSize.Location, new Point(0,0))))
            //{
                TileMaps[selectedTileMap][location.Y, location.X] = value;
            //}
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
                saveData = saveData.Remove(saveData.Length-1);
                saveData += ',';
            }

            saveData = saveData.Remove(saveData.Length - 1, 1);
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
