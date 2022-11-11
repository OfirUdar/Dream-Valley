using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class MapElementBehaviour : MonoBehaviour, IPlaceable
    {

        private MapElementSO _placementSO;

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

        public ISelectable Selectable { get; private set; }
        public IDraggable Draggable { get; private set; }

        public IPlaceApprover PlaceApprover { get; private set; }

        [Inject]
        public void Init(MapElementSO placementSO, ISelectable selectable,
            IDraggable draggable,
            IPlaceApprover placeApprover)
        {
            _placementSO = placementSO;
            Selectable = selectable;
            Draggable = draggable;
            PlaceApprover = placeApprover;
        }



        public class Factory : PlaceholderFactory<Object, MapElementBehaviour>
        {

        }

    }

}