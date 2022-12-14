using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Map.Element.Building.Resources.UI
{
    public class ResourceCollectorUI : MonoBehaviour
    {
        [SerializeField] private PopupTween _popup;
        [SerializeField] private Button _collectButton;
        [SerializeField] private Image _resourceImage;

        [Inject] private readonly IResourceGenerator _resourceGenerator;



        private void OnEnable()
        {
            _resourceGenerator.CollectableChanged += OnCollectableChanged;
            _collectButton.onClick.AddListener(OnCollectClicked);
        }

        private void OnDisable()
        {
            _resourceGenerator.CollectableChanged -= OnCollectableChanged;
            _collectButton.onClick.RemoveListener(OnCollectClicked);
        }

        private void Start()
        {
            _resourceImage.sprite = _resourceGenerator.GetResource().Sprite;
        }

        private void OnCollectableChanged(bool canCollect)
        {
            _popup.AutoActivate(canCollect);
        }

        private void OnCollectClicked()
        {
            _resourceGenerator.Collect();
        }


    }
}
