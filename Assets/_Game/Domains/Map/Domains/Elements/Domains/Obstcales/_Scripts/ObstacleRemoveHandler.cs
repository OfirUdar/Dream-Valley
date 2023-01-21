using Zenject;

namespace Game.Map.Element.Obstcales
{
    public class ObstacleRemoveHandler : IRemoveHandler
    {
        [Inject] private readonly IResourcesInventory _resourcesInventory;
        [Inject] private readonly ISelectionManager _selectionManager;
        [Inject] private readonly IMapGrid _grid;
        [Inject] private readonly IMapElement _mapElement;
        [Inject] private readonly ObstacleDataSO _obstacleDataSO;
        [Inject] private readonly IVFXFactory _vfxFactory;

        [Inject] private readonly IDialog _dialog;
        public ResourcePrice Price => _obstacleDataSO.Price;

        public void Remove()
        {
            var canPurchase = _resourcesInventory.CanSubtract(Price.Resource, Price.Amount);

            if(canPurchase)
            {
                _resourcesInventory.SubtractResource(Price.Resource, Price.Amount);
                _grid.Remove(_mapElement);
                _vfxFactory.CreateEffect(VFXType.ElementPlaced, _mapElement.Center);
                _selectionManager.RequestUnselect();
                _mapElement.Destroy();
            }
            else
            {
                _dialog.Show("Not enough!", $"Not enough \"{Price.Resource.Name}'s\"");
            }
        }

    }
}