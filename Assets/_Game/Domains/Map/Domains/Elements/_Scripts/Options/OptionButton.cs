using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Map.Element
{
    public class OptionButton : MonoBehaviour
    {
        [SerializeField] protected Button _button;

        protected IMapElement _mapElement;

        public UnityEvent<IMapElement> Initalized;
        public UnityEvent<IMapElement> Clicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public void Setup(IMapElement mapElement)
        {
            _mapElement = mapElement;
            Initalized?.Invoke(mapElement);
        }
        private void OnClicked()
        {
            Clicked?.Invoke(_mapElement);
        }

    }
}
