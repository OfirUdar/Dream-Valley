using System.Threading.Tasks;
using UnityEngine;

namespace Game.Map
{
    public class PlaySFXWithRateDecoratorCommand : IEventCommand
    {
        private readonly IEventCommand _playSFXParent;
        private bool _canPlay = true;
        private readonly int _playRateTime;
        public PlaySFXWithRateDecoratorCommand(IEventCommand playSFXParent, int playRateTimeMiliseconds = 70)
        {
            _playSFXParent = playSFXParent;
            _playRateTime = playRateTimeMiliseconds;
        }
        public void Execute(object value)
        {
            if (_canPlay)
            {
                _playSFXParent.Execute(value);
                _canPlay = false;
                Task.Run(RestFlag);
            }
        }
        private async Task RestFlag()
        {
            await Task.Delay(_playRateTime);
            _canPlay = true;
        }
    }
}