using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Map.Element.Building.TownHall
{
    public class UITownHallUpgradeDisplay : MonoBehaviour
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
        [Space]
        [SerializeField] private Sprite _workersSprite;
        [SerializeField] private Sprite _storageCapacitySprite;

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

        public UITownHallUpgradeDisplay SetElement(MapElementSO elementSO)
        {
            _elementNameText.text = elementSO.Name;
            _elementImage.sprite = elementSO.Sprite;
            return this;
        }
        public UITownHallUpgradeDisplay SetNextLevelData(Level elementNextLevel, bool canPurchase, Action upgradeCallback)
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
        public UITownHallUpgradeDisplay SetNextLevelIndex(int nextLevel)
        {
            _titleText.text = "Upgrade to level " + (nextLevel + 1) + "?";

            return this;
        }
        public UITownHallUpgradeDisplay SetComparisonLevels(TownHallData currentLevelData, TownHallData nextLevelData)
        {
            var workersAmount = Instantiate(_rowPfb, _container, false);
            var addedWorkersUpdated = $"<color=green>{(nextLevelData.Workers - currentLevelData.Workers)}</color>";
            workersAmount.SetText($"<u>Workers:</u>  {currentLevelData.Workers} + {addedWorkersUpdated}");
            workersAmount.SetSprite(_workersSprite);

            Instantiate(_separatorPfb, _container, false);

            var storageCapcity = Instantiate(_rowPfb, _container, false);
            var addedCapacityUpdated = $"<color=green>{(nextLevelData.StorageCapacity - currentLevelData.StorageCapacity)}</color>";
            storageCapcity.SetText($"<u>Storage Capacity:</u>  {currentLevelData.StorageCapacity} + {addedCapacityUpdated}");
            storageCapcity.SetSprite(_storageCapacitySprite);

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
                // _dialog.Show("Not enough", "Not enough to purchase");
            }

        }

    }
}