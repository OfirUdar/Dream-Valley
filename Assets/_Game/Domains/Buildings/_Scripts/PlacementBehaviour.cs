using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game
{
    public class PlacementBehaviour : MonoBehaviour, IPlaceable
    {
        public PlacementFacade Facade { get; private set; }
        
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
        public void Init(PlacementSO placementSO,PlacementFacade placementFacade)
        {
            _placementSO = placementSO;
            Facade = placementFacade;
        }



        public class Factory : PlaceholderFactory<Object, PlacementBehaviour>
        {

        }

    }

}