using Zenject;

namespace Game.Map
{
    public class DragEndPlacedCommand : DragCommandBase
    {
        public class Pool : MemoryPool<IEventCommand>
        {

        }
    }


}