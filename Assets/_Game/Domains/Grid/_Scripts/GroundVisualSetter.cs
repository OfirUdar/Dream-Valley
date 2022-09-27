using UnityEngine;
using Zenject;

namespace Game
{
    public class GroundVisualSetter : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _gridPlane;

        [Inject]
        public void Init(GridSettings settings)
        {
            var _rows = settings.Rows;
            var _columns = settings.Columns;
            var _cellSize = settings.CellSize;

            var scale = new Vector3(_rows, 0, _columns) * _cellSize;
            scale.y = _gridPlane.transform.localScale.y;
            _gridPlane.transform.localScale = scale;
            _gridPlane.material.mainTextureScale = new Vector2(_rows, _columns) / 2f;
        }
    }
}
