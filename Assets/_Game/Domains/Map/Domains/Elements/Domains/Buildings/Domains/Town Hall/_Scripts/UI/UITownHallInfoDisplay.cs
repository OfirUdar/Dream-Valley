using UnityEngine;

namespace Game.Map.Element.Building.TownHall
{
    public class UITownHallInfoDisplay : MonoBehaviour
    {
        [SerializeField] private UIInfoDisplay _displayer;
        [SerializeField] private UIDataRow _rowPfb;
        [SerializeField] private GameObject _separatorPfb;
        [Space]
        [SerializeField] private Transform _container;
        [Space]
        [SerializeField] private Sprite _workersSprite;
        [SerializeField] private Sprite _storageCapacitySprite;

        public void Display(MapElementSO mapElementSO,
            TownHallData data,
            int currentLevel)
        {
            var workersAmount = Instantiate(_rowPfb, _container, false);
            workersAmount.SetText($"<u>Workers:</u>  {data.Workers}");
            workersAmount.SetSprite(_workersSprite);

            Instantiate(_separatorPfb, _container, false);

            var storageCapcity = Instantiate(_rowPfb, _container, false);
            storageCapcity.SetText($"<u>Storage Capacity:</u>  {data.StorageCapacity}");
            storageCapcity.SetSprite(_storageCapacitySprite);

            _displayer.Display(mapElementSO, currentLevel);
        }
    }
}