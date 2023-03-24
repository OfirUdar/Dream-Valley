using Zenject;

namespace Game.Map.Element.Building
{
    public class UpgradeStartedCommand : CompositeEventCommand
    {
        public UpgradeStartedCommand(params IEventCommand[] commands) : base(commands)
        {
        }

        public class Pool : MemoryPool<IEventCommand>
        {

        }
    }
}