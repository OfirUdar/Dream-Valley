using Zenject;

namespace Game.Map
{
    public class DraggingCommand : DragCommandBase
    {
        public class Pool : MemoryPool<IEventCommand>
        {

        }
    }


}