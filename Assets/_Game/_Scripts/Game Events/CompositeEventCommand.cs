using UnityEngine;

namespace Game
{
    public class CompositeEventCommand : IEventCommand
    {
        private readonly IEventCommand[] _commands;

        public CompositeEventCommand(params IEventCommand[] commands)
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