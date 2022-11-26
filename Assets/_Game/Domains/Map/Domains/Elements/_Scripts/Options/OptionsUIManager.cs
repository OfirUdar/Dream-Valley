using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Map.Element
{
    public class OptionsUIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private Button _optionButtonPfb;

        private readonly Dictionary<ElementOption, IOption> _optionsDictionary
            = new Dictionary<ElementOption, IOption>();

        private ISelectionManager _selectionManager;

        [Inject]
        public void Init(ISelectionManager selectionManager)
        {
            _selectionManager = selectionManager;
            SetDisable();
        }
        private void OnEnable()
        {
            _selectionManager.SelectionChanged += OnSelectionChanged;
        }
        private void OnDisable()
        {
            _selectionManager.SelectionChanged -= OnSelectionChanged;
        }
        private void SetEnable()
        {
            _container.gameObject.SetActive(true);
        }
        private void SetDisable()
        {
            _container.gameObject.SetActive(false);
        }

        private void OnSelectionChanged(ISelectable selectable)
        {
            if (selectable == null)
            {
                _container.DOScaleX(0f, 0.1f).SetEase(Ease.OutBack).OnComplete(SetDisable).Play();
            }
            if (selectable is IMapElement mapElement)
            {
                _container.DOScaleX(1f, 0.1f).SetEase(Ease.OutBack).OnPlay(SetEnable).Play();
                //mapElement.
            }
        }

    }

    public interface IOption
    {

    }
    public class OptionButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _optionText;
    }

}
