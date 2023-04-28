using System.Threading.Tasks;

namespace Game
{
    public class RateExecuteDecoratorCommand : IEventCommand
    {
        private readonly IEventCommand _parentCommand;
        private readonly int _executeRateTime;
        private bool _canExecute = true;

        public RateExecuteDecoratorCommand(IEventCommand parentCommand, int executeRateTimeMiliseconds = 50)
        {
            _parentCommand = parentCommand;
            _executeRateTime = executeRateTimeMiliseconds;
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
            await Task.Delay(_executeRateTime);
            _canExecute = true;
        }
    }
}