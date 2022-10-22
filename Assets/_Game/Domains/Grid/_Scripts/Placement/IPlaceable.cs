using UnityEngine;

namespace Game
{
    public interface IPlaceable 
    {
        public int Width { get; }
        public int Height { get; }
        public Vector3 Position { get; set; }

    }


}