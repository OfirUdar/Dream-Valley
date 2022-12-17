using UnityEngine;

namespace Game.Map.Element.Building.Resources
{
    public class UIResourceInfoDisplay : UIInfoDisplay<ResourceGeneratorData>
    {
        [SerializeField] private UIDataRow _rowPfb;
        [Space]
        [SerializeField] private Transform _container;
        public override void Setup(ResourceGeneratorData data)
        {
            var productionRate = Instantiate(_rowPfb,_container,false);
            productionRate.SetText($"Production Rate: {data.AmountPerTime} per {data.TimeInMinute} mintues");

            var storageCapcity = Instantiate(_rowPfb, _container, false);
            storageCapcity.SetText($"Storage Capcity: {data.Capacity}");
        }
    }

}
