using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class FacadeBehaviour : MonoBehaviour
    {
        public IMapElement MapElement { get; private set; }

        [Inject]
        public void Init(IMapElement mapElement)
        {
            MapElement = mapElement;
        }

        public class Factory : PlaceholderFactory<Object, FacadeBehaviour>
        {
            
        }

    }

}