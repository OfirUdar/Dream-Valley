using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Options
{
    public class OptionsUIManager : MonoBehaviour
    {
        [SerializeField] private ElementOptionsSO _elementOptionsSO;
        [Space]
        [SerializeField] private RectTransform _mainContainer;
        [SerializeField] private RectTransform _container;
        [SerializeField] private TextMeshProUGUI _elementNameText;


        private ISelectionManager _selectionManager;

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
        }

        private void OnDisable()
        {
            _selectionManager.SelectionChanged -= OnSelectionChanged;
        }


        [Inject]
        public void Init(ISelectionManager selectionManager)
        {
            _selectionManager = selectionManager;
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
                _hideTween.SmoothRewind();
                _elementNameText.text = mapElement.Data.Name;

                var optionsButtonsPfb = _elementOptionsSO.GetPrefabsByOptions(mapElement.Data.Options);

                for (int i = 0; i < _container.childCount; i++)
                {
                    Destroy(_container.GetChild(i).gameObject);
                }

                foreach (var pfb in optionsButtonsPfb)
                {
                    var optionButton = Instantiate(pfb, _container, false);
                    optionButton.Setup(mapElement);
                }

            }
        }

    }
}
