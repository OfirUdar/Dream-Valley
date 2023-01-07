using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Map.Element.Building.Resources.UI
{
    public class UIResourceCollector : MonoBehaviour
    {
        [SerializeField] private PopupTween _popup;
        [SerializeField] private Button _collectButton;
        [SerializeField] private Image _resourceImage;

        [Inject] private readonly IResourceCollector _resourceCollector;



        private void OnEnable()
        {
            _resourceCollector.CollectableChanged += OnCollectableChanged;
            _collectButton.onClick.AddListener(OnCollectClicked);
        }

        private void OnDisable()
        {
            _resourceCollector.CollectableChanged -= OnCollectableChanged;
            _collectButton.onClick.RemoveListener(OnCollectClicked);
        }

        private void Start()
        {
            _resourceImage.sprite = _resourceCollector.GetResource().Sprite;
        }

        private void OnCollectableChanged(bool canCollect)
        {
            _popup.AutoActivate(canCollect);
        }

        private void OnCollectClicked()
        {
            _resourceCollector.Collect();
        }


    }
}
