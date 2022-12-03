using System;

namespace Game
{
    [Serializable]
    public class MapElementData
    {
        public string InstanceGUID;
        public int Level;
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