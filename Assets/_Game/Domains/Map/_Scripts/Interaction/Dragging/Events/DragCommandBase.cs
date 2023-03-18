namespace Game.Map
{
    public abstract class DragCommandBase : IEventCommand
    {
        private readonly IEventCommand[] _commands;

        public DragCommandBase(params IEventCommand[] commands)
        {
            _commands = commands;
        }

        public void Execute(object value = null)
        {
            for (int i = 0; i < _commands.Length; i++)
            {
                _commands[i].Execute(value);
            }
        }
    }


}