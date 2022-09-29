using UnityEngine;
using Zenject;

namespace Game
{
    public class PlacementBehaviour : MonoBehaviour, IPlaceable
    {
        [SerializeField] private PlacementSO _placementSO;
        [SerializeField] private Transform _area;
        [SerializeField] private MeshRenderer _editAreaRenderer;
        [SerializeField] private Transform _gfx;

        public int Width => _placementSO.Data.Width;
        public int Height => _placementSO.Data.Height;
        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

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

            _editAreaRenderer.material.mainTextureScale =
                new Vector2(_placementSO.Data.Width, _placementSO.Data.Height) / 2f;
        }
        
        public void Approve()
        {

        }
        public void Cancel()
        {
            Destroy(this.gameObject);
        }

        public class Factory : PlaceholderFactory<Object, PlacementBehaviour>
        {

        }

    }

}