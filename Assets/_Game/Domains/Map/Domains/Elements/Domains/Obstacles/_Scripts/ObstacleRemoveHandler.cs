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
        [Inject] private readonly RemoveObstacleCommand.Pool _removeObstacleCommandPool;

        [Inject] private readonly IDialog _dialog;
        public ResourcePrice Price => _obstacleDataSO.Price;

        public void Remove()
        {
            var canPurchase = _resourcesInventory.CanSubtract(Price.Resource, Price.Amount);

            if (canPurchase)
            {
                _resourcesInventory.SubtractResource(Price.Resource, Price.Amount);
                _grid.Remove(_mapElement);
                FireRemoveEventCommand();
                _selectionManager.RequestUnselect();
                _mapElement.Destroy();
            }
            else
            {
                _dialog.Show("Not enough!", $"Not enough \"{Price.Resource.Name}'s\"");
            }
        }

        private void FireRemoveEventCommand()
        {
            var commandInstance = _removeObstacleCommandPool.Spawn();
            commandInstance.Execute(_mapElement.Center);
            _removeObstacleCommandPool.Despawn(commandInstance);
        }
    }

    public class RemoveObstacleCommand : IEventCommand
    {

         private readonly IEventCommand[] _commands;

        public RemoveObstacleCommand(params IEventCommand[] commands)
        {
            _commands = commands;
        }

        public void Execute(object value = null)
        {
            for (int i = 0; i < _commands.Length; i++)
            {
                _commands[i].Execute(value);
            }
        }

        public class Pool : MemoryPool<IEventCommand>
        {

        }
    }
}