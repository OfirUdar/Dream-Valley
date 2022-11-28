using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Map.Element.Options
{
    public abstract class OptionButtonBase : MonoBehaviour
    {
        [SerializeField] protected Button _button;
        [SerializeField] protected Image _image;
        [SerializeField] protected TextMeshProUGUI _optionText;

        protected ISelectable _selectable;
        public abstract void Execute();

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }
        
        public void Setup(ISelectable selectable)
        {
            _selectable = selectable;
        }
        private void OnClicked()
        {
            Execute();
        }

    }
}
