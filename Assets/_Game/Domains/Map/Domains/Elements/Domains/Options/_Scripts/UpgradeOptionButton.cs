using UnityEngine;

namespace Game.Map.Element.Options
{
    public class UpgradeOptionButton : MonoBehaviour
    {

        public void Execute(IMapElement mapElement)
        {
            mapElement.UpgradeDisplayer.Display();
        }
    }
}
