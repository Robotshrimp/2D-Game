using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GameJom
{
    public static class AssetStorage
    {
        public static Folder BaseTextureAssets;
        public static GraphicsDevice graphicsDevice;
        public static void AssetLoader(string folder/*sets target folder in content/assets*/)
        {
            DirectoryInfo rootDirectory = new DirectoryInfo(@"Content/Assets/" + folder); // sets accessFolder to correct target
            if (!rootDirectory.Exists)
            {
                rootDirectory.Create();
            }
            else
            {
                FileInfo[] files = rootDirectory.GetFiles("*.xnb");
                foreach (FileInfo file in files)
                {
                    string key = Path.GetFileNameWithoutExtension(file.Name);
                    //TileSetAssets.Add(Content.Load<Texture2D>(rootDirectory.FullName + "/" + key));
                }
            }
        }
        public static Folder GetFolder(string root, ContentManager Content)
        {
            Folder Output = new Folder();
            DirectoryInfo rootDirectory = new DirectoryInfo(root); // sets rootDirectory to correct target
            if (!rootDirectory.Exists)
            {
                rootDirectory.Create();
            }
            string[] directories = Directory.GetDirectories(root);
            foreach (string subFolder in directories)
            {
                Output.SubFolders.Add(subFolder, GetFolder(subFolder, Content));
            }
            FileInfo[] textureFiles = rootDirectory.GetFiles("*.xnb");
            foreach (FileInfo file in textureFiles)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                Output.Storage.Add(key, LoadPNGTexture(key));
            }
            FileInfo[] stringFiles = rootDirectory.GetFiles("*.txt");
            foreach (FileInfo file in stringFiles)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                Output.Storage.Add(key, File.ReadAllText(key));
            }
            //Returns a Folder output that contains all string and png texture files within the directory given in root and all sub directories
            return Output;
        }
        public static Texture2D LoadPNGTexture(string filePath) // loads the texture directly from PNG file format
        {
            FileStream fileStream = new FileStream("Content/" + filePath + ".png", FileMode.Open);
            Texture2D spriteAtlas = Texture2D.FromStream(graphicsDevice, fileStream);
            fileStream.Dispose();
            return spriteAtlas;
        }
        public static void SaveFolder(Folder folder, string root)// saves all directories and string files, string files must be pre serialized
        {
            foreach(string file in folder.Storage.Keys)
            {
                if(folder.Storage[file].GetType() == typeof(string))
                {
                    if (!File.Exists(file))
                        File.Create(file).Dispose();
                    File.WriteAllText(file, folder.Storage[file].ToString());
                }
            }
            foreach(string subFolder in folder.SubFolders.Keys)
            {
                SaveFolder(folder.SubFolders[subFolder], root + "/" + subFolder);
            }
        }
    }
    public class Folder
    {
        public Dictionary<string, object> Storage = new Dictionary<string, object>();
        public Dictionary<string,Folder> SubFolders = new Dictionary<string,Folder>();
        public Folder SearchForFolder(string search)
        {
            if (SubFolders.ContainsKey(search)) // searches for folder within first layer of subfolders
            {
                return SubFolders[search];
            }
            foreach (Folder folder in SubFolders.Values) // calls self for all sub folders, outputing if any returns a non null
            {
                if (folder.SearchForFolder(search) != null)
                    return folder.SearchForFolder(search);
            }
            return null;
        }
    }
}
