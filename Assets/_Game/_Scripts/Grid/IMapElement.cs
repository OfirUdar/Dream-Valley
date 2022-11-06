using UnityEngine;

namespace Game
{
    public interface IMapElement
    {
        public int Width { get; }
        public int Height { get; }
        public Vector3 Position { get; set; }

        public ISelectable Selectable { get; }
        public IDraggable Draggable { get; }

    }


}