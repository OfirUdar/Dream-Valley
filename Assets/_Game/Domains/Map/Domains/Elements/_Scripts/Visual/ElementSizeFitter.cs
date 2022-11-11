using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class ElementSizeFitter : MonoBehaviour
    {
        [SerializeField] private Transform _gfx;
        [SerializeField] private BoxCollider _collider;
        [Space]
        [SerializeField] private Transform _area;
        [SerializeField] private MeshRenderer _editAreaRenderer;

        private MapElementSO _placementSO;


        [Inject]
        public void Init(GridSettings gridSettings, MapElementSO placementSO)
        {
            _placementSO = placementSO;

            FitAll(gridSettings);
        }

        private void FitAll(GridSettings gridSettings)
        {
            var areaScale = _area.localScale;
            areaScale.x = gridSettings.CellSize * _placementSO.Data.Width;
            areaScale.z = gridSettings.CellSize * _placementSO.Data.Height;

            var position = _area.localPosition;

            position.x = areaScale.x / 2f;
            position.z = areaScale.z / 2f;

            FitGFX(position);
            FitCollider(areaScale, position);

            FitArea(areaScale, position);
            FitAreaMaterial();
        }

        private void FitGFX(Vector3 position)
        {
            _gfx.localPosition = position;
        }
        private void FitCollider(Vector3 areaScale, Vector3 position)
        {
            //Size:
            var colliderSize = _collider.size;
            colliderSize.x = areaScale.x;
            colliderSize.z = areaScale.z;
            _collider.size = colliderSize;

            //Center / Position:
            var centerPosition = position;
            centerPosition.y = colliderSize.y / 2f;
            _collider.center = centerPosition;

        }

        private void FitArea(Vector3 areaScale, Vector3 position)
        {
            _area.localScale = areaScale;
            _area.localPosition = position;
        }
        private void FitAreaMaterial()
        {
            _editAreaRenderer.material.mainTextureScale =
                new Vector2(_placementSO.Data.Width, _placementSO.Data.Height) / 2f;
        }

    }
}