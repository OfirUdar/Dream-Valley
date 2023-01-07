using UnityEngine;
using System.IO;

namespace Udar
{

    public class SaveLoadManager : ISaveManager, ILoadManager
    {
        private static readonly string _mainPath = Application.persistentDataPath + "/";

        public void Load(ILoadable loadable)
        {
            var finalPath = _mainPath + loadable.Path;
            var loader = File.ReadAllText(finalPath + "/data");
            loadable.SetSerialized(loader);
        }
        public bool TryLoad(ILoadable loadable)
        {
            try
            {
                Load(loadable);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string[] LoadFoldersPaths(string path)
        {
            var finalPath = _mainPath + path;
            if (Directory.Exists(finalPath))
                return Directory.GetDirectories(_mainPath + path);
            return new string[0];
        }

        public void Save(ISaveable saveable)
        {
            var finalPath = _mainPath + saveable.Path;
            if (!Directory.Exists(finalPath))
                Directory.CreateDirectory(finalPath);

            File.WriteAllText(finalPath + "/data", saveable.GetSerialized());
        }
        public bool TrySave(ISaveable saveable)
        {
            try
            {
                Save(saveable);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDelete(ISaveable saveable)
        {
            var finalPath = _mainPath + saveable.Path;

            if (!Directory.Exists(finalPath))
                return false;

            File.Delete(finalPath + "/data");
            Directory.Delete(finalPath, true);

            return true;
        }
    }
}