﻿using System;
using System.IO;

namespace Game
{
    public static class SaveLoadKeys
    {
        public const string Resources = "Player/Inventory/Resources";
        public const string Map = "Map";
        public const string ElementLevel = "ElementLevel";
        public const string ElementUpgrader = "ElementUpgrader";
        public const string ResourceGenerator = "ResourceGenerator";
        public const string BuildingState = "BuildingState";


        public static string GetElementPath(string dataGUID, string instanceGUID)
        {
            return Path.Combine(Map, dataGUID, instanceGUID);
        }

        public static string GetElementLevelPath(string dataGUID, string instanceGUID)
        {
            return Path.Combine(Map, dataGUID, instanceGUID, ElementLevel);
        }
        public static string GetElementUpgraderPath(string dataGUID, string instanceGUID)
        {
            return Path.Combine(Map, dataGUID, instanceGUID, ElementUpgrader);
        }
        public static string GetResourceGeneratorPath(string dataGUID, string instanceGUID)
        {
            return Path.Combine(Map, dataGUID, instanceGUID, ResourceGenerator);
        }

        public static string GetBuildingState(string dataGUID, string instanceGUID)
        {
            return Path.Combine(Map, dataGUID, instanceGUID, BuildingState);
        }
    }
}