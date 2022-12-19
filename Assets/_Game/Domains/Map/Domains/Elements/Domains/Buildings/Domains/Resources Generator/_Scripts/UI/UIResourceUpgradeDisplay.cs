using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Map.Element.Building.Resources
{
    public class UIResourceUpgradeDisplay : MonoBehaviour
    {
        [SerializeField] private UIInfoDisplay _displayer;
        [SerializeField] private UIDataRow _rowPfb;
        [SerializeField] private GameObject _separatorPfb;
        [Space]
        [SerializeField] private Transform _container;
        [Space]
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Image _upgradeButtonImage;
        [SerializeField] private TextMeshProUGUI _upgradeButtonText;

        private Action _upgradeCallback;


        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(StartUpgrade_Button);
        }
        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(StartUpgrade_Button);
        }

        public void Display(MapElementSO mapElementSO,
            ResourceDataSO resource,
            ResourceGeneratorData currentResourceLevel,
            ResourceGeneratorData nextResourceLevel,
            Level elementNextLevel,
            Action upgradeCallback)
        {
            _upgradeButtonImage.sprite = elementNextLevel.UpgradePrice.Resource.Sprite;
            _upgradeButtonText.text = elementNextLevel.UpgradePrice.Amount.ToString();

            _upgradeCallback = upgradeCallback;

            var productionRate = Instantiate(_rowPfb, _container, false);
            productionRate.SetText($"Production Rate:  {currentResourceLevel.AmountPerTime}+{nextResourceLevel.AmountPerTime - currentResourceLevel.AmountPerTime} per {currentResourceLevel.TimeInMinute} min");
            productionRate.SetSprite(resource.Sprite);

            Instantiate(_separatorPfb, _container, false);

            var storageCapcity = Instantiate(_rowPfb, _container, false);
            storageCapcity.SetText($"Storage Capcity:  {currentResourceLevel.Capacity}+{nextResourceLevel.Capacity - currentResourceLevel.Capacity}");
            storageCapcity.SetSprite(resource.Sprite);

            _displayer.Display(mapElementSO);
        }

        private void StartUpgrade_Button()
        {

            _upgradeCallback?.Invoke();
        }
    }

}
