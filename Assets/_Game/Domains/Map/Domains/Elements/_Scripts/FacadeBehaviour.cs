using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class FacadeBehaviour : MonoBehaviour
    {
        [Inject] public IMapElement MapElement { get; private set; }
        [Inject] public IEventor Eventor { get; private set; }

        public class Factory : PlaceholderFactory<Object, FacadeBehaviour>
        {

        }

    }

}