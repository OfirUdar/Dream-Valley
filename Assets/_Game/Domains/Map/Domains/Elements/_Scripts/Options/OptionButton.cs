using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Map.Element
{
    public class OptionButton : MonoBehaviour
    {
        [SerializeField] protected Button _button;
        [SerializeField] protected Image _image;
        [SerializeField] protected TextMeshProUGUI _optionText;

        protected IMapElement _mapElement;

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
        }
        private void OnClicked()
        {
            Clicked?.Invoke(_mapElement);
        }

    }
}
