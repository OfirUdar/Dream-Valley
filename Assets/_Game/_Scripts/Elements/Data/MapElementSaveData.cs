using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class MapElementSaveData
    {
        public string InstanceGUID;
        public Vector3 Position;
    }

    [Flags]
    public enum ElementOption
    {
        Nothing = 0,
        Info = 1,
        Upgrade = 2,
        Remove = 4,
        Collect = 8,
    }




}