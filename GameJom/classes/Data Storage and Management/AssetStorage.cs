using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameJom._3D_Because_Why_Not;
using System.Linq;

namespace GameJom
{
    public static class AssetStorage
    {
        public static Folder ContentAssets;
        public static GraphicsDevice graphicsDevice = Game1.graphicsDevice;
        public static void LoadAllContentAssets()
        {
            ContentAssets = GetFolder("Content");
        }
        public static Folder GetFolder(string root) // gets all files and subfolders in the root directory and creates a Folder instance to store them
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
                string key = subFolder.Split(@"\".ToCharArray().Last()).Last();
                Output.SubFolders.Add(key, GetFolder(subFolder));
            }
            FileInfo[] textureFiles = rootDirectory.GetFiles("*.png");
            foreach (FileInfo file in textureFiles)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                Output.Storage.Add(key, LoadPNGTexture(root + "/" + key + ".png"));
            }
            FileInfo[] stringFiles = rootDirectory.GetFiles("*.txt");
            foreach (FileInfo file in stringFiles)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                Output.Storage.Add(key, File.ReadAllText(root + "/" + key + ".txt"));
            }
            //Returns a Folder output that contains all string and png texture files within the directory given in root and all sub directories
            return Output;
        }
        public static Texture2D LoadPNGTexture(string filePath) // loads the texture directly from PNG file format
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Texture2D loadedPNG = Texture2D.FromStream(graphicsDevice, fileStream);
            fileStream.Dispose();
            return loadedPNG;
        }
        public static void SaveFolder(Folder folder, string root)// saves all directories and string files, does not serialize string files
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
        //public static void LoadText
    }
    public class Folder
    {
        public Dictionary<string, object> Storage = new Dictionary<string, object>(); // storage of all files, currently only designed to support string and texture files but can support any files using object as value. needs specification of object type before pulling from the dictionary
        public Dictionary<string,Folder> SubFolders = new Dictionary<string,Folder>(); // storage of subfolders
        public void AddFolderStorage(Dictionary<string, object> Addition) // adds file contents into the current folder
        {
            foreach (string key in Addition.Keys)
            {
                Storage.Add(key, Addition[key]);
            }
        }
        public Folder Sum() // returns a new folder with the sum of all sub folders
        {
            Folder newFolder = new Folder();
            newFolder.AddFolderStorage(Storage);
            foreach (Folder folder in SubFolders.Values)
            {
                newFolder.AddFolderStorage(folder.Storage);
                newFolder.AddFolderStorage(folder.Sum().Storage);
            }    
            return newFolder;
        }
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
