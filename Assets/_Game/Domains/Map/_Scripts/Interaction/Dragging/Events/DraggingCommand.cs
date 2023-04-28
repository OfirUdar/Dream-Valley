using Zenject;

namespace Game.Map
{
    public class DraggingCommand : CompositeEventCommand
    {
        public DraggingCommand(params IEventCommand[] commands) : base(commands)
        {
        }

        public class Pool : MemoryPool<IEventCommand>
        {

        }
    }


}