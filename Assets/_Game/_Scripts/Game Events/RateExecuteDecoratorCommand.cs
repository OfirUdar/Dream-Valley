using System.Threading.Tasks;

namespace Game
{
    public class RateExecuteDecoratorCommand : IEventCommand
    {
        private readonly IEventCommand _parentCommand;
        private readonly int _executeTimeRate;
        private bool _canExecute = true;

        public RateExecuteDecoratorCommand(IEventCommand parentCommand, int executeTimeRateMiliseconds = 50)
        {
            _parentCommand = parentCommand;
            _executeTimeRate = executeTimeRateMiliseconds;
        }
        public void Execute(object value)
        {
            if (_canExecute)
            {
                _parentCommand.Execute(value);
                _canExecute = false;
                Task.Run(RestFlag);
            }
        }
        private async Task RestFlag()
        {
            await Task.Delay(_executeTimeRate);
            _canExecute = true;
        }
    }
}