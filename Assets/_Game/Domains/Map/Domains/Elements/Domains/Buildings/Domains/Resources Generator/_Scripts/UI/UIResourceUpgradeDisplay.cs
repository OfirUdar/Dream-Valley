using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Map.Element.Building.Resources
{
    public class UIResourceUpgradeDisplay : MonoBehaviour
    {
        [SerializeField] private PanelActivator _panelActivator;
        [SerializeField] private UIDataRow _rowPfb;
        [SerializeField] private GameObject _separatorPfb;
        [Space]
        [SerializeField] private Transform _container;
        [Space]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _elementNameText;
        [SerializeField] private Image _elementImage;
        [Space]
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Image _upgradeButtonImage;
        [SerializeField] private TextMeshProUGUI _upgradePriceButtonText;
        [SerializeField] private TextMeshProUGUI _upgradeDurationText;

        [Inject] private readonly IDialog _dialog;

        private bool _canPurchase;
        private Action _upgradeCallback;

        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(StartUpgrade_Button);
        }
        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(StartUpgrade_Button);
        }

        public UIResourceUpgradeDisplay SetElement(MapElementSO elementSO)
        {
            _elementNameText.text = elementSO.Name;
            _elementImage.sprite = elementSO.Sprite;
            return this;
        }
        public UIResourceUpgradeDisplay SetNextLevelData(Level elementNextLevel, bool canPurchase, Action upgradeCallback)
        {
            _canPurchase = canPurchase;

            _upgradeDurationText.text = elementNextLevel.UpgradeDuration.GetTimeSpan().ToDisplayTime();

            _upgradeButtonImage.sprite = elementNextLevel.UpgradePrice.Resource.Sprite;

            string priceAmount = elementNextLevel.UpgradePrice.Amount.ToString();
            if (canPurchase)
                _upgradePriceButtonText.text = $"<color=white>{priceAmount}</color>";
            else
                _upgradePriceButtonText.text = $"<color=red>{priceAmount}</color>";

            _upgradeCallback = upgradeCallback;
            return this;
        }
        public UIResourceUpgradeDisplay SetNextLevelIndex(int nextLevel)
        {
            _titleText.text = "Upgrade to level " + (nextLevel + 1) + "?";

            return this;
        }

        public UIResourceUpgradeDisplay SetResourcesData(ResourceDataSO resource, ResourceGeneratorData currentResourceLevel, ResourceGeneratorData nextResourceLevel)
        {
            var productionRate = Instantiate(_rowPfb, _container, false);

            var upgradeAddon = $"<color=green>{ nextResourceLevel.AmountPerTime - currentResourceLevel.AmountPerTime }</color>";
            var perTime = $"\n<size=12>(per {currentResourceLevel.TimeInMinute} min)</size>";
            var totalUpgradeTimeText = $"<u>Production Rate:</u>  {currentResourceLevel.AmountPerTime} + {upgradeAddon} {perTime}";

            productionRate.SetText(totalUpgradeTimeText);
            productionRate.SetSprite(resource.Sprite);

            Instantiate(_separatorPfb, _container, false);

            var storageCapcity = Instantiate(_rowPfb, _container, false);

            upgradeAddon = $"<color=green>{nextResourceLevel.Capacity - currentResourceLevel.Capacity}</color>";
            storageCapcity.SetText($"<u>Storage Capcity:</u>  {currentResourceLevel.Capacity} + {upgradeAddon}");
            storageCapcity.SetSprite(resource.Sprite);

            return this;
        }
        public void Display()
        {
            _panelActivator.Show();
        }
        private void StartUpgrade_Button()
        {
            if (_canPurchase)
            {
                _upgradeCallback?.Invoke();
                _panelActivator.Hide();
            }
            else
            {
                 _dialog.Show("Not enough", "Not enough to purchase");
            }

        }
    }

}
