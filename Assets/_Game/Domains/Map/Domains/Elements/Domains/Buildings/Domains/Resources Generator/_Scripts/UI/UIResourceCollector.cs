using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Map.Element.Building.Resources.UI
{
    public class UIResourceCollector : MonoBehaviour
    {
        [SerializeField] private UIResourceCollectorSettingsSO _settings;
        [Space]
        [SerializeField] private PopupTween _popup;
        [SerializeField] private Button _collectButton;
        [SerializeField] private Image _resourceImage;

        [Inject] private readonly IResourceCollector _resourceCollector;
        [Inject] private readonly IResourcesInventory _resourcesInventory;



        private void OnEnable()
        {
            _resourceCollector.CollectableChanged += OnCollectableChanged;
            _resourcesInventory.StorageFullChanged += OnStorageFullChanged;
            _collectButton.onClick.AddListener(OnCollectClicked);
        }

        private void OnDisable()
        {
            _resourceCollector.CollectableChanged -= OnCollectableChanged;
            _resourcesInventory.StorageFullChanged -= OnStorageFullChanged;
            _collectButton.onClick.RemoveListener(OnCollectClicked);
        }

        private void Start()
        {
            _resourceImage.sprite = _resourceCollector.GetResource().Sprite;
        }

        private void ChangeImageColorByStorageState(bool isStorageFull)
        {
            _collectButton.image.color = isStorageFull ? _settings.FullStorageColor : _settings.DefaultColor;
        }
        private void OnCollectableChanged(bool canCollect)
        {
            _popup.AutoActivate(canCollect);
            if (_resourcesInventory.IsStorageFull(_resourceCollector.GetResource()))
                ChangeImageColorByStorageState(true);
        }

        private void OnStorageFullChanged(ResourceDataSO resource, bool isFull)
        {
            if (resource == _resourceCollector.GetResource())
                ChangeImageColorByStorageState(isFull);
        }
        private void OnCollectClicked()
        {
            _resourceCollector.Collect();
        }


    }
}
