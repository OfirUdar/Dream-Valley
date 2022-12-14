using System;
using Udar;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.Resources
{
    public class ResourceGenerator : IBuildingState, IResourceGenerator
    {
        [Inject] private readonly ResourceGeneratorLevelsData _generatorData;

        private readonly Profile _profile;
        private readonly ISaveManager _saveManager;
        private readonly IDateTimer _timer;
        private readonly ILevelManager _levelManager;

        public event Action<bool> CollectableChanged;

        public ResourceGenerator(Profile profile,
            ISaveManager saveManager,
            IDateTimer timer,
            ILevelManager levelManager)
        {
            _profile = profile;
            _saveManager = saveManager;
            _timer = timer;
            _levelManager = levelManager;
        }

        public void Enter()
        {
            _timer.Finished += OnTimerFinished;
            StartTimer();
        }
        
        public void Tick()
        {


        }
        public void Exit()
        {
            _timer.Finished -= OnTimerFinished;

            _timer.Stop();

            CollectableChanged?.Invoke(false);
        }

        private void StartTimer()
        {
            var generatorTime = _generatorData[_levelManager.CurrentLevel].GetTimeSpan();
            _timer.SetTime(generatorTime).Start();
            CollectableChanged?.Invoke(false);
        }

        private void OnTimerFinished()
        {
            CollectableChanged?.Invoke(true);
        }

        public void Collect()
        {
            var amount = _generatorData[_levelManager.CurrentLevel].AmountPerTime;
            _profile.ResourcesInventory.AddResource(_generatorData.Resource, amount);

            _saveManager.TrySave(_profile.ResourcesInventory);

            StartTimer();
        }

        public ResourceDataSO GetResource()
        {
            return _generatorData.Resource;
        }
    }
}