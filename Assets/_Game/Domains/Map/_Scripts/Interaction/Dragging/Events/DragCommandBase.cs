using Zenject;

namespace Game.Map
{
    public abstract class DragCommandBase : IEventCommand
    {
        [Inject] private readonly IEventCommand _command;

        public void Execute(object value = null)
        {
            _command.Execute(value);
        }
    }


}