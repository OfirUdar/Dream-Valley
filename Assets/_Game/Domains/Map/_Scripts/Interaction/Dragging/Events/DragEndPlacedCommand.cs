using Zenject;

namespace Game.Map
{
    public class DragEndPlacedCommand : CompositeEventCommand
    {
        public DragEndPlacedCommand(params IEventCommand[] commands) : base(commands)
        {
        }

        public class Pool : MemoryPool<IEventCommand>
        {

        }
    }


}