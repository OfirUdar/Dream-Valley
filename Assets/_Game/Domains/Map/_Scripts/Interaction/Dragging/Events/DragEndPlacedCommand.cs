using Zenject;

namespace Game.Map
{
    public class DragEndPlacedCommand : DragCommandBase
    {
        public DragEndPlacedCommand(params IEventCommand[] commands) : base(commands)
        {
        }

        public class Pool : MemoryPool<IEventCommand>
        {

        }
    }


}