namespace Game.Map
{
    public class DragEventCommand : IEventCommand
    {
        private readonly IEventCommand[] _commands;

        public DragEventCommand(params IEventCommand[] commands)
        {
            _commands = commands;
        }
        public void Execute(object value)
        {
            for (int i = 0; i < _commands.Length; i++)
            {
                _commands[i].Execute(value);
            }
        }
    }
}