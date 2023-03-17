using Zenject;

namespace Game.Map
{
    public class DragEndPlacedErrorCommand : DragCommandBase
    {
        public class Pool : MemoryPool<IEventCommand>
        {

        }
    }


}