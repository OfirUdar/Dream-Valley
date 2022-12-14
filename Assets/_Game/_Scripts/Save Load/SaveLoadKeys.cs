using System.IO;

namespace Game
{
    public static class SaveLoadKeys
    {
        public const string Resources = "Player/Inventory/Resources";
        public const string Map = "Map";
        public const string ElementLevel = "ElementLevel";


        public static string GetElementPath(string dataGUID, string instanceGUID)
        {
            return Path.Combine(Map, dataGUID, instanceGUID);
        }

        public static string GetElementLevelPath(string dataGUID, string instanceGUID)
        {
            return Path.Combine(Map, dataGUID, instanceGUID, ElementLevel);
        }

    }
}