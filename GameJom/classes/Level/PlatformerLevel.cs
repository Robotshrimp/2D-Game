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
using System.Web;
using GameJom.classes.Level;

namespace GameJom
{
    public class PlatformerLevel
    { 
        public List<Room> Rooms= new List<Room>();
        int CurrentRoom;
        protected string location;
        public static Dictionary<string, int> TileSetKey = new Dictionary<string, int>();
        Folder LevelData;
        Folder UsedAssets;
        AutomatedDraw DrawParam;
        Rectangle GridSize;
        public PlatformerLevel(Folder levelData) // input any level information from folder to program runtime
        {

            LevelData = levelData;
            if (!LevelData.Storage.ContainsKey("rooms"))
                LevelData.Storage.Add("rooms", "");

            // accessing texture files by foreach loop
            UsedAssets = AssetStorage.ContentAssets.PathToFolder("Content/Assets/TileSets");
            #region Tile Set Organization 
            string tileSetKeyData = (string)UsedAssets.Storage["TileSetKey"]; // data saved of tileset corrilation
            string[] tileSetKey = tileSetKeyData.Split('.');   
            foreach (string key in tileSetKey) // parsing saved data on tileset pairings
            {
                string[] dictionaryPair = key.Split(',');
                TileSetKey.Add(dictionaryPair[0], int.Parse(dictionaryPair[1]));
            }
            Folder AllTileSets = UsedAssets.Sum();
            foreach (string tileSet in AllTileSets.Storage.Keys) // adds new pairing if new tilesets are found
            {
                if (!AllTileSets.Storage.ContainsKey(tileSet))
                {
                    TileSetKey.Add(tileSet, TileSetKey.Count + 1); // adds the number associated to the end
                }
            }
            #endregion

            // parsing data for room 
            string data = (string)LevelData.Storage["rooms"];
            string[] roomData = data.Split('&');
            foreach (string Room in roomData)
            {
                if(Room != "")
                {
                    Rooms.Add(new Room(Room));
                }
            }
            foreach (Room room in Rooms) 
            {
                room.Reload();
            }
        }
        public void Update()
        {
            foreach (Room room in Rooms) 
            {
                room.RuntimeUpdate();
            }
        }
        public void Draw()
        {
            foreach (Room room in Rooms)
            {
                room.Draw(DrawParam, GridSize);
            }
        }
        public void Save()
        {
            foreach(Room room in Rooms)
            {
                room.Save();
            }
        }
    }
}
