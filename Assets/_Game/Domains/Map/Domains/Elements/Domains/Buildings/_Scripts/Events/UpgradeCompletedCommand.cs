using Zenject;

namespace Game.Map.Element.Building
{
    public class UpgradeCompletedCommand : CompositeEventCommand
    {
        public UpgradeCompletedCommand(params IEventCommand[] commands) : base(commands)
        {
        }

        public class Pool : MemoryPool<IEventCommand>
        {

        }
    }
}