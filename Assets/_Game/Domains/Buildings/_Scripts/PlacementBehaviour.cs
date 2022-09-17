using UnityEngine;
using Zenject;

namespace Game
{
    public class PlacementBehaviour : MonoBehaviour
    {
        [SerializeField] private BuildingSO _placementSO;
        [SerializeField] private Transform _area;
        [SerializeField] private Transform _gfx;

        private GridSettings _gridSettings;

        [Inject]
        public void Init(GridSettings gridSettings)
        {
            _gridSettings = gridSettings;

            var areaScale = _area.localScale;
            areaScale.x = _gridSettings.CellSize * _placementSO.Data.Width;
            areaScale.z = _gridSettings.CellSize * _placementSO.Data.Height;
            _area.localScale = areaScale;

            var position = _area.localPosition;

            position.x = areaScale.x / 2f;
            position.z = areaScale.z / 2f;

            _area.localPosition = position;
            _gfx.localPosition = position;
        }


        public class Factory : PlaceholderFactory<Object,PlacementBehaviour>
        {

        }

    }
    
}