using System.Threading.Tasks;
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
        [Inject] private readonly IMapGrid _grid;



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
        private async void OnCollectClicked()
        {
            //_resourceCollector.Collect(transform.position);

            var uiResourceCollectors = GameObject.FindObjectsOfType<UIResourceCollector>();
            foreach (var resource in uiResourceCollectors)
            {
                if (resource.gameObject.activeSelf && 
                    resource._resourceCollector.GetResource() == _resourceCollector.GetResource())
                {
                    resource._resourceCollector.Collect(resource.transform.position);
                    await Task.Delay(120);
                }
            }
        }


    }
}
