using UnityEngine;

namespace Game.Map.Element.Options
{
    public class InfoOptionButton : MonoBehaviour
    {
        [SerializeField] private UIInfoElementDialog _pfb;
        public void Execute(IMapElement mapElement)
        {
            Instantiate(_pfb).Show(mapElement);
        }
    }
}
