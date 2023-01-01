using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Options
{
    public class OptionsUIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _mainContainer;
        [SerializeField] private RectTransform _container;
        [SerializeField] private TextMeshProUGUI _elementNameText;

        [Inject] private ISelectionManager _selectionManager;
        [Inject] private IOptionsAggragetor _optionsAggragetor;
        private IMapElement _selectedElement;

        private Tween _hideTween;

        private void Awake()
        {
            _hideTween = _mainContainer
                .DOAnchorPosY(-100f, 0.1f)
                .SetEase(Ease.OutBack)
                .OnPlay(SetEnable)
                .OnComplete(SetDisable)
                .SetAutoKill(false)
                .SetLink(gameObject);
            _hideTween.Complete();
            SetDisable();
        }

        private void OnEnable()
        {
            _selectionManager.SelectionChanged += OnSelectionChanged;
            _optionsAggragetor.RefreshRequested += OnRefreshResquested;
        }
        private void OnDisable()
        {
            _selectionManager.SelectionChanged -= OnSelectionChanged;
            _optionsAggragetor.RefreshRequested -= OnRefreshResquested;
        }



        private void SetEnable()
        {
            _mainContainer.gameObject.SetActive(true);
        }
        private void SetDisable()
        {
            _mainContainer.gameObject.SetActive(false);
        }

        private void OnSelectionChanged(ISelectable selectable)
        {
            if (selectable == null && _mainContainer.gameObject.activeSelf)
            {
                _hideTween.Restart();
            }

            if (selectable is IMapElement mapElement)
            {
                _selectedElement = mapElement;
                _hideTween.SmoothRewind();

                Display();
            }
        }

        private void Display()
        {
            _elementNameText.text = _selectedElement.OptionsDisplayer.GetDisplayText();//mapElement.Data.Name;
            CleanContainer();
            _selectedElement.OptionsDisplayer.Show(_container);
        }

        private void CleanContainer()
        {
            for (int i = 0; i < _container.childCount; i++)
            {
                Destroy(_container.GetChild(i).gameObject);
            }
        }

        private void OnRefreshResquested()
        {
            Display();
        }
    }
}
