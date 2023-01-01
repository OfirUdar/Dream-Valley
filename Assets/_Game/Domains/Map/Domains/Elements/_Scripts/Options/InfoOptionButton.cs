using UnityEngine;

namespace Game.Map.Element
{
    public class InfoOptionButton : MonoBehaviour
    {
        public void Execute(IMapElement mapElement)
        {
            mapElement.InfoDisplayer.Display();
        }
    }
}
