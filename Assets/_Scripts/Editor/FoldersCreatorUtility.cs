using System;
using System.Reflection;
using UnityEditor;

namespace Udar
{
    public static class FoldersCreatorUtility
    {
        private static readonly string[] _domainFolderList = new string[]
        {
            "_Scripts",
            "Domains"
        };

        [MenuItem("Assets/Create/Create Domain Folders", priority = 1)]
        public static void CreateDomainFolders()
        {
            var currentPath = GetCurrentOpenPath();
            foreach (var folderName in _domainFolderList)
            {
                if (!AssetDatabase.IsValidFolder(currentPath + "/" + folderName))
                    AssetDatabase.CreateFolder(currentPath, folderName);
            }

        }

        private static string GetCurrentOpenPath()
        {
            Type projectWindowUtilType = typeof(ProjectWindowUtil);
            MethodInfo getActiveFolderPath = projectWindowUtilType.GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);
            object obj = getActiveFolderPath.Invoke(null, new object[0]);
            string pathToCurrentFolder = obj.ToString();

            return pathToCurrentFolder;
        }
    }
}
