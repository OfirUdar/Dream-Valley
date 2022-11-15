using System;
using System.Collections.Generic;

namespace Game.Player
{
    public class Profile
    {
        public List<ResourceData> Resources;

        //Level
        //Current XP
    }

    [Serializable]
    public class ResourceData
    {
        public ResourceDataSO Data;
        public int Amount;
    }

}
