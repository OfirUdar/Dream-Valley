using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class PlacerBehaviour : MonoBehaviour, IPlaceable
    {

        private MapElementData _mapElementData;
        private Vector3 _size;

        public int Width => _mapElementData.Width;

        public int Height => _mapElementData.Height;

        public Vector3 Position
        {
            get
            {
                return transform.position - _size / 2f;
            }
            set
            {
                transform.position = value + _size / 2f;
            }
        }

        [Inject]
        public void Init(MapElementData mapElementData, GridSettings gridSettings)
        {
            _mapElementData = mapElementData;

            _size = new Vector3(_mapElementData.Width, 0, _mapElementData.Height) * gridSettings.CellSize;
        }
    }
}