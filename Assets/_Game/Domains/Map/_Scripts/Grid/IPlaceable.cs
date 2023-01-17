using UnityEngine;

namespace Game.Map
{
    public interface IPlaceable
    {
        public int Width { get; }
        public int Height { get; }
        public Vector3 Position { get; set; }
        public Vector3 Center { get; }


    }


}