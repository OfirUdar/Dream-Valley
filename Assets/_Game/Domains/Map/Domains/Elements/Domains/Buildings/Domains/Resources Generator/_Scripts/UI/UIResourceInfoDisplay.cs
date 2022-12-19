using UnityEngine;

namespace Game.Map.Element.Building.Resources
{
    public class UIResourceInfoDisplay : MonoBehaviour
    {
        [SerializeField] private UIInfoDisplay _displayer;
        [SerializeField] private UIDataRow _rowPfb;
        [SerializeField] private GameObject _separatorPfb;
        [Space]
        [SerializeField] private Transform _container;

        public void Display(MapElementSO mapElementSO, ResourceDataSO resource, ResourceGeneratorData data)
        {
            var productionRate = Instantiate(_rowPfb, _container, false);
            productionRate.SetText($"Production Rate:  {data.AmountPerTime} per {data.TimeInMinute} min");
            productionRate.SetSprite(resource.Sprite);

            Instantiate(_separatorPfb, _container, false);

            var storageCapcity = Instantiate(_rowPfb, _container, false);
            storageCapcity.SetText($"Storage Capcity:  {data.Capacity}");
            storageCapcity.SetSprite(resource.Sprite);

            _displayer.Display(mapElementSO);
        }
    }

}
