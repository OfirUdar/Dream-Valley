using UnityEngine;

namespace Game.Map.Element.Options
{
    public class InfoOptionButton : MonoBehaviour
    {
        public void Execute(IMapElement mapElement)
        {
            mapElement.InfoDisplayer.Display();
        }
    }
}
