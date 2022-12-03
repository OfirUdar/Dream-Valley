using Udar;
using Zenject;

namespace Game.Map.Element.Building.Resources
{
    public class ResourcesGenerator : IBuildingState
    {
        [Inject] private readonly ResourceGeneratorLevelsData _generatorData;

        private readonly Profile _profile;
        private readonly ISaveManager _saveManager;
        private readonly IDateTimer _timer;

        public ResourcesGenerator(Profile profile, ISaveManager saveManager, IDateTimer timer)
        {
            _profile = profile;
            _saveManager = saveManager;
            _timer = timer;
        }

        public void Enter()
        {
            _timer.Finished += OnTimerFinished;

            var generatorTime = _generatorData[0].GetTimeSpan();
            _timer.SetTime(generatorTime).Start();
        }
        public void Tick()
        {


        }
        public void Exit()
        {
            _timer.Finished -= OnTimerFinished;

            _timer.Stop();
        }

        private void OnTimerFinished()
        {
            _profile.ResourcesInventory
                .AddResource(_generatorData.Resource, _generatorData[0].AmountPerTime);

            _saveManager.TrySave(_profile.ResourcesInventory);
        }


    }
}