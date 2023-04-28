using Zenject;

namespace Game.Map
{
    public class DragEndPlacedErrorCommand : CompositeEventCommand
    {
        public DragEndPlacedErrorCommand(params IEventCommand[] commands) : base(commands)
        {
        }

        public class Pool : MemoryPool<IEventCommand>
        {

        }
    }


}