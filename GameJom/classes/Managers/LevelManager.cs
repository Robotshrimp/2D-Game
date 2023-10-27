using GameJom.classes.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    public static class LevelManager // format for a level?
    {
        static Folder UsedAssets;
        static Folder LevelData;
        static Dictionary<string, PlatformerLevel> UsedLevels = new Dictionary<string, PlatformerLevel>();
        static Dictionary <string, int> TileSetKey = new Dictionary<string, int>();
        static void Load()
        {
            #region Level loading
            LevelData = AssetStorage.ContentAssets.PathToFolder("Content/Levels");
            foreach(string levelName in LevelData.SubFolders.Keys)
            {
                UsedLevels.Add(levelName, new PlatformerLevel(LevelData.SubFolders[levelName]));
            }
            #endregion
        }
        static void Update()
        {

        }
        public static void Save()
        {
            foreach (PlatformerLevel level in UsedLevels.Values)
            {
                level.Save();
            }
        }
    }
}
