using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameJom._3D_Because_Why_Not;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Remoting.Messaging;
using System.Runtime.InteropServices;
using System.Text.Json;

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
            Folder Output = new Folder(); // every new output is a new folder seperate from the original from the previous layer of recursion, this wipes the data generated from newsubfolder
            DirectoryInfo rootDirectory = new DirectoryInfo(root); // sets rootDirectory to correct target
            if (!rootDirectory.Exists)
            {
                rootDirectory.Create();
            }
            
            Output.HostFolderPath = rootDirectory.Name; // sets the host location to current folder name for all subfolders 
            Output.Name = rootDirectory.Name;
            string[] directories = Directory.GetDirectories(root);
            foreach (string subFolder in directories)
            {
                string key = subFolder.Split(@"\".ToCharArray().Last()).Last();
                Output.NewSubFolder(key, GetFolder(subFolder));
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
                if (!File.Exists(@root + "/" + file))
                    File.Create(@root + "/" + file).Dispose();
                string jsonString = JsonSerializer.Serialize(folder.Storage[file]);
                File.WriteAllText(@root + "/" + file, jsonString);
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
        public string HostFolderPath = ""; // should be a path all the way to where the folder tree was first loaded in in AssetStorage
        public string Name = ""; // the name of this folder as it would be called in parent folder
        public Dictionary<string, object> Storage = new Dictionary<string, object>(); // storage of all files, currently only designed to support string and texture files but can support any files using object as value. needs specification of object type before pulling from the dictionary
        private Dictionary<string,Folder> subFolders = new Dictionary<string,Folder>(); // storage of subfolders
        public Dictionary<string, Folder> SubFolders // prevents modification outside of class. modification should be done through correct members to keep folder path integrety 
        {
            get { return subFolders;}
        }
        public Folder GetPotentialFolder(string name)
        {
            if (!SubFolders.ContainsKey(name))
            {
                NewSubFolder(name, new Folder());
            }
            return SubFolders[name];
        }
        #region GetData
        public Folder SearchForFolder(string search) // finds a folder using it's name, not reliable if multiple folders exis with the same name
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
        public Folder PathToFolder(string path)
        {
            path = path.Substring(this.Name.Length + 1);
            string[] pathFolders = path.Split('/');
            Folder Output = this;
            foreach (string folder in pathFolders)
            {
                GetPotentialFolder(folder);
                Output = Output.SubFolders[folder];
            }
            return Output;
        }
        #endregion
        #region SetData
        #region Folder Modification // currently the only way to edit subfolders
        public void NewSubFolder(string name, Folder Data) // creates a subfolder with record of parent folder, only useful for saving data. created when subfolder is created so accessing it does not require use of the method
        {
            Folder addedFolder = Data;
            addedFolder.HostFolderPath = HostFolderPath + "/" + name;
            addedFolder.Name = name;
            SubFolders.Add(name, addedFolder);
        } 
        public void UpdateSubFolderPaths() // updates the subfolder to be accurate to the current folderPath
        {
            foreach (string folderName in SubFolders.Keys)
            {
                subFolders[folderName].HostFolderPath = HostFolderPath + folderName;
                SubFolders[folderName].UpdateSubFolderPaths();
            }
        }
        public void SetFolderValue(Folder Target) // sets the values of the new folder without changing the host path
        {
            this.Storage = Target.Storage;
            this.subFolders = Target.SubFolders;
            UpdateSubFolderPaths();
        }        
        public void ModifySubFolder(string path, Folder modification) // sets values to a subfolder. WILL NOT WORK WITH PATH LENGTH LESS THAN 2
        {
            string[] pathFolders = path.Split('/');
            if (pathFolders.Length < 2)
                throw new Exception("dim headed loser tries to use subfolder to change main folder");
            GetPotentialFolder(pathFolders[1]);
            this.SubFolders[pathFolders[1]].SetFolderValue(PathSetSubfolder(path.Substring((pathFolders[0] + "/").Length), modification, this.SubFolders[pathFolders[1]]));
        }
        private Folder PathSetSubfolder(string path, Folder changeTo, Folder changeFrom) // saves to a specific folder from path
        {
            string[] pathFolders = path.Split('/');
            string folder = pathFolders[0];
            if (folder == pathFolders.Last()) // returns the changed folder at end of path to be set as new folder by previous recursion 
            {
                return changeTo;
            }
            string nextFolder = pathFolders[1];
            changeFrom.GetPotentialFolder(nextFolder);
            changeFrom.SubFolders[nextFolder].SetFolderValue(PathSetSubfolder(path.Substring((folder + "/").Length), changeTo, changeFrom.SubFolders[nextFolder])); // sets selected folder
            return changeFrom;
        }
        public void Save(Folder saveData) // saves the data using it's hostFolderPath
        {
            saveData.UpdateSubFolderPaths();
            ModifySubFolder(saveData.HostFolderPath, saveData);
        }
        #endregion
        #region Storage Modification // modifies the storage dictionary
        public void MergeFolderStorage(Dictionary<string, object> Addition) // adds file contents into the current folder. does not 
        {
            foreach (string key in Addition.Keys)
            {
                Storage.Add(key, Addition[key]);
            }
        }
        public Folder Sum() // returns a new folder with the sum of all sub folder storage
        {
            Folder newFolder = new Folder();
            newFolder.MergeFolderStorage(Storage);
            foreach (Folder folder in SubFolders.Values)
            {
                newFolder.MergeFolderStorage(folder.Storage);
                newFolder.MergeFolderStorage(folder.Sum().Storage);
            }    
            return newFolder;
        }
        #endregion
        #endregion


    }
}
