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
    //ToDo: convert to 3d array to allow more complex tilemapping
    public class Room
    {
        public enum DataChunks // helps keep track of what datachunk each part is
        {
            room = 0,
            tiles = 1,
            grphicalFlare = 2,
            properties = 3,

        }
        #region TileDenotation
        private enum Tiles
        {
            left = 1,
            right = 2,
            top = 4,
            bottom = 8,
            topLeft = 16,
            topRight = 32,
            bottomLeft = 64,
            bottomRight = 128,
        }
        Tiles cardnelTiles = Tiles.left | Tiles.right | Tiles.top | Tiles.bottom;
        Tiles diagonalTiles = Tiles.topLeft | Tiles.bottomLeft | Tiles.topRight | Tiles.bottomRight;
        Dictionary<Point, Tiles> LocationToTileValue = new Dictionary<Point, Tiles>
        {
            {new Point(-1,-1), Tiles.topLeft},
            {new Point(0,-1), Tiles.top},
            {new Point(1,-1), Tiles.topRight},
            {new Point(-1,0), Tiles.left},
            {new Point(1, 0), Tiles.right},
            {new Point(-1, 1), Tiles.bottomLeft},
            {new Point(0, 1), Tiles.bottom},
            {new Point(1,1), Tiles.bottomRight}
        };
        Dictionary<Tiles, Point> TileValueToLocation = new Dictionary<Tiles, Point>
        {
            {Tiles.topLeft, new Point(-1,-1)},
            {Tiles.top, new Point(0,-1)},
            {Tiles.topRight, new Point(1,-1)},
            {Tiles.left, new Point(-1,0)},
            {Tiles.right, new Point(1, 0)},
            {Tiles.bottomLeft, new Point(-1, 1)},
            {Tiles.bottom, new Point(0, 1)},
            {Tiles.bottomRight, new Point(1,1)}
        };
        public const int empty = -1;
        #endregion

        const int DataLayers = 2;
        const int TileLayer = 0;
        const int TileSetLayer = 1;

        public int SelectedLayer = 0;
        public int TotalLayers = 0;
        public List<int[,,]> TileMaps = new List<int[,,]>();
        public List<int> CollisionLayers = new List<int>(); // list of layers with collision
        public Rectangle RoomSize;
        public Point DesiredCameraLocation;
        public Room(string TileMapData)
        {
            #region TileData Parse
            if (TileMapData != "")
            {
                string[] data = TileMapData.Split('.');
                // room data
                string[] vars = data[(int)DataChunks.room].Split(',');
                RoomSize = new Rectangle(int.Parse(vars[0]), int.Parse(vars[1]), int.Parse(vars[2]), int.Parse(vars[3]));
                // tilemap data
                // use room dimenstions to determine where each tile go, this means tiles are stored as 1D not 2d array
                string[] totalRoomTileData = data[(int)DataChunks.tiles].Split(';');
                int currentMap = 0;
                int currentTile = 0;
                int currentCollum = 0;
                int currentDataLayer = 0;
                foreach (string tilemapData in totalRoomTileData)
                {
                    TotalLayers++;
                    int[,,] blankLayer = new int[RoomSize.Width, RoomSize.Height, DataLayers];
                    Fill(ref blankLayer, RoomSize);
                    TileMaps.Add(blankLayer);
                    string[] tiles = tilemapData.Split(' ');
                    foreach (string tile in tiles)
                    {
                        if (currentTile >= RoomSize.Height)
                        {
                            currentCollum += 1;
                            currentTile = 0;
                        }
                        if (currentCollum >= RoomSize.Width)
                        {
                            currentDataLayer += 1;
                            currentCollum = 0;
                        }
                        if (currentDataLayer >= DataLayers)
                        {
                            currentDataLayer = 0;
                            break;
                        }

                        TileMaps[currentMap][currentCollum, currentTile, currentDataLayer] = int.Parse(tile);
                        currentTile += 1;
                    }
                    currentTile = 0;
                    currentDataLayer = 0;
                    currentCollum = 0;
                    currentMap += 1;
                }
                #endregion
            }
        }
        public Room(Rectangle Size) // overload constructor for new room with set size
        {
            RoomSize = Size;
            AddTilemap();
        }
        public void AddTilemap()
        {
            TotalLayers += 1;
            int[,,] initializationMap = new int[RoomSize.Width, RoomSize.Height, DataLayers];
            Fill(ref initializationMap, new Rectangle(new Point(), RoomSize.Size));
            TileMaps.Add(initializationMap);
        }
        public static void Fill(ref int[,,] tileMap, Rectangle fill, int fillNum = empty, int layer = 0)
        {
            if (fill.X >= 0 &&
               fill.Y >= 0 &&
               fill.Right <= tileMap.GetLength(0) &&
               fill.Bottom <= tileMap.GetLength(1))
            {
                for (int x = 0; x < fill.Width; x++)
                {
                    for (int y = 0; y < fill.Height; y++)
                    {
                        tileMap[fill.X + x, fill.Y + y, layer] = fillNum;
                    }
                }
            }
        }
        // playtime
        public void RuntimeUpdate()
        {
            // add ingame interations for when the room is loaded
        }
        Camera DrawParam;
        GridTexture GridParam;
        public void Draw(Camera drawParam, Rectangle grid)
        {
            DrawParam = drawParam;
            GridParam = new GridTexture(drawParam, grid);
            int currentTilemap = 0;
            foreach (int[,,] tilemap in TileMaps)
            {
                for (int x = 0; x < tilemap.GetLength(0); x++)
                {
                    for (int y = 0; y < tilemap.GetLength(1); y++)
                    {
                        if (tilemap[x, y, TileLayer] != empty)
                        {
                            //DrawTile(currentTilemap, new Point(x, y), TileSetAssets[tilemap[x, y, TileSetLayer]]);
                        }
                    }
                }
                currentTilemap += 1;
            }
        }
        public void DrawTile(int tileMap,Point location, Texture2D tileSet) // draws the correct texture form the correct tileset at the correct location using the correct draw parameters
        {
            Point tileTextureSize = new Point(tileSet.Width / 4, tileSet.Height / 5);
            Rectangle selection;
            int tileData = TileMaps[tileMap][location.X, location.Y, TileLayer];
            int mainTileData = tileData & ((int)Tiles.left + (int)Tiles.right + (int)Tiles.top + (int)Tiles.bottom);
            int y = mainTileData / 4;
            int x = mainTileData - y * 4;
            selection = new Rectangle(new Point(tileTextureSize.X * x, tileTextureSize.Y * y), tileTextureSize);
            Rectangle drawArea = new Rectangle(RoomSize.Location + location - new Point(1, 1), new Point(3, 3));
            DrawParam.Draw(GridParam.GridToScreen(drawArea), tileSet, new Rectangle(tileTextureSize.X * x, tileTextureSize.Y * y, tileTextureSize.X, tileTextureSize.Y), Color.White);
            if(cornerTextureCheck(tileData, Tiles.topLeft))
            {
                DrawParam.Draw(GridParam.GridToScreen(drawArea), tileSet, new Rectangle(tileTextureSize.X * 0, tileTextureSize.Y * 4, tileTextureSize.X, tileTextureSize.Y), Color.White);
            }
            if (cornerTextureCheck(tileData, Tiles.topRight))
            {
                DrawParam.Draw(GridParam.GridToScreen(drawArea), tileSet, new Rectangle(tileTextureSize.X * 1, tileTextureSize.Y * 4, tileTextureSize.X, tileTextureSize.Y), Color.White);
            }
            if (cornerTextureCheck(tileData, Tiles.bottomLeft))
            {
                DrawParam.Draw(GridParam.GridToScreen(drawArea), tileSet, new Rectangle(tileTextureSize.X * 2, tileTextureSize.Y * 4, tileTextureSize.X, tileTextureSize.Y), Color.White);
            }
            if (cornerTextureCheck(tileData,Tiles.bottomRight))
            {
                DrawParam.Draw(GridParam.GridToScreen(drawArea), tileSet, new Rectangle(tileTextureSize.X * 3, tileTextureSize.Y * 4, tileTextureSize.X, tileTextureSize.Y), Color.White);
            }
        }
        private bool cornerTextureCheck(int tileData, Tiles corner)
        {
            if (corner == (corner & cardnelTiles))
                return false;
            Point cornerTileLocation = TileValueToLocation[corner];
            if (!filledCheck(tileData, corner) &&
                filledCheck(tileData, LocationToTileValue[new Point(0, cornerTileLocation.Y)]) &&
                filledCheck(tileData, LocationToTileValue[new Point(cornerTileLocation.X, 0)]))
                return true;
            return false;
        }
        private bool filledCheck(int tileData, Tiles checkedTile)
        {
            if ((tileData & (int)checkedTile) == (int)checkedTile)
                return true;
            return false;
        }
        // editing functions, ignore if coding playtime features
        private bool inBounds(Point index, int[,,] array, int dataType = TileLayer)
        {
            return ((dataType >= 0 && dataType < array.GetLength(2)) && (index.Y >= 0 && index.Y < array.GetLength(1)) && (index.X >= 0 && index.X < array.GetLength(0)));
        }
        #region Tile Editing
        public void Edit(Point Location, int value = 0, int dataType = 0) // Edits the tilemap much like updateTileData but calls the updateTileData for all surounding filled tiles
        {
            while (SelectedLayer >= TileMaps.Count)
            {
                AddTilemap();
            }
            if (!inBounds(Location, TileMaps[SelectedLayer], dataType))
                return;
            TileMaps[SelectedLayer][Location.X, Location.Y, dataType] = value;
            foreach (Point point in LocationToTileValue.Keys)
            {
                updateTileLayerData(SelectedLayer, Location + point);
            }
            if (value != -1)
            {
                updateTileLayerData(SelectedLayer, Location);
            }
        }
        private void updateTileLayerData(int tilemap, Point location)
        {
            if (!inBounds(location, TileMaps[tilemap]) || TileMaps[tilemap][location.X, location.Y, TileLayer] == empty)
            {
                return;
            }
            int tileData = 0;
            foreach (Point point in LocationToTileValue.Keys)
            {
                if (!inBounds(location + point, TileMaps[tilemap]) || TileMaps[tilemap][location.X + point.X, location.Y + point.Y, TileLayer] != empty)
                {
                    tileData |= (int)LocationToTileValue[point];
                }
            }
            TileMaps[tilemap][location.X, location.Y, TileLayer] = tileData;
        }
        public void Reload() // updates all tiles for correct display
        {
            for (int tilemap = 0; tilemap < TileMaps.Count; tilemap++) 
            {
                for (int x = 0; x < TileMaps[tilemap].GetLength(0); x ++) 
                {
                    for (int y = 0; y < TileMaps[tilemap].GetLength(1); y++)
                    {
                        updateTileLayerData(tilemap, new Point(x,y));
                    }
                }
            }
        }
        #endregion
        protected void Resize(Point targetSize, int selectedTileMap) // creates a new array that replaces the old one that retains the information
        {
            int[,,] newArray = new int[targetSize.Y, targetSize.X, DataLayers];
            for(int x = 0; x < Math.Min(TileMaps[selectedTileMap].GetLength(0), newArray.GetLength(0)); x++)
            {
                for (int y = 0; y < Math.Min(TileMaps[selectedTileMap].GetLength(1), newArray.GetLength(1)); y++)
                {
                    for (int z = 0; z < Math.Min(TileMaps[selectedTileMap].GetLength(2), newArray.GetLength(2)); z++)
                    {
                        newArray[x, y, z] = TileMaps[selectedTileMap][x, y, z];
                    }
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

                for (int z = 0; z < TileMaps[h].GetLength(2); z++)
                {
                    for (int x = 0; x < TileMaps[h].GetLength(0); x++)
                    {
                        for (int y = 0; y < TileMaps[h].GetLength(1); y++)
                        {
                            saveData += TileMaps[h][x, y, z] + " ";
                        }
                    }
                }
                saveData = saveData.Remove(saveData.Length-1);
                saveData += ';';
            }

            saveData = saveData.Remove(saveData.Length - 1, 1);
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

    }
}
