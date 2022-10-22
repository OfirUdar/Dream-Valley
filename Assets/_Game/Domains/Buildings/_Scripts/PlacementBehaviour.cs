using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game
{
    public class PlacementBehaviour : MonoBehaviour, IPlaceable
    {
        private PlacementSO _placementSO;

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

        [Inject]
        public void Init(PlacementSO placementSO)
        {
            _placementSO = placementSO;
        }



        public class Factory : PlaceholderFactory<Object, PlacementBehaviour>
        {

        }

    }

}