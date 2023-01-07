using System;
using Udar;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.Resources
{
    public class ResourceGenerator : IBuildingState, IResourceGenerator, ISaveable, ILoadable
    {
        [Inject] private readonly ResourceGeneratorLevelsData _generatorData;

        private readonly IResourceCollector _resourceCollector;
        private readonly IMapElement _mapElement;
        private readonly ISaveManager _saveManager;
        private readonly ILoadManager _loadManager;
        private readonly IDateTimer _timer;
        private readonly ILevelManager _levelManager;



        public ResourceGenerator(IResourceCollector resourceCollector,
            IMapElement mapElement,
            ISaveManager saveManager,
            ILoadManager loadManager,
            IDateTimer timer,
            ILevelManager levelManager)
        {
            _resourceCollector = resourceCollector;
            _mapElement = mapElement;
            _saveManager = saveManager;
            _loadManager = loadManager;
            _timer = timer;
            _levelManager = levelManager;
        }

        public void Enter()
        {
            _timer.Finished += OnTimerFinished;
            var isSuccessed = _loadManager.TryLoad(this);
            if (!isSuccessed)
                StartTimer();

            _resourceCollector.CollectableChanged += OnCollectableChanged;
        }
       
        public void Tick()
        {


        }
        public void Exit()
        {

            _timer.Finished -= OnTimerFinished;
            _timer.Stop();

            _resourceCollector.CollectableChanged -= OnCollectableChanged;

            _resourceCollector.Exit();

            _saveManager.TryDelete(this);
        }

        private void StartTimer()
        {
            var generatorTime = _generatorData[_levelManager.CurrentIndexLevel].GetTimeSpan();
            StartTimer(generatorTime);
            _saveManager.Save(this);
        }
        private void StartTimer(TimeSpan time)
        {
            _timer.SetTime(time).Start();
        }

        private void OnTimerFinished()
        {
            _resourceCollector.AddAmountToStorage();

            if (!_resourceCollector.IsStorageFull())
                StartTimer();
        }

        public ResourceDataSO GetResource()
        {
            return _generatorData.Resource;
        }

        private void OnCollectableChanged(bool canCollect)
        {
            if (!_timer.IsTicking() && !canCollect)
                StartTimer();
        }




        #region Save&Load
        public string Path => SaveLoadKeys.GetResourceGeneratorPath(_mapElement.Data.GUID, _mapElement.SaveData.InstanceGUID);

        public void SetSerialized(string data)
        {
            _loadManager.TryLoad(_resourceCollector);

            var saveData = JsonUtility.FromJson<TimerSaveData>(data);
            DateTime.TryParse(saveData.StartTime, out DateTime startTime);
            var generatorTime = _generatorData[_levelManager.CurrentIndexLevel].GetTimeSpan();
            var differentTime = (DateTime.Now - startTime);


            var cyclesPassed = (int)differentTime.TotalSeconds / (int)generatorTime.TotalSeconds;
            var secondsRemains = (int)differentTime.TotalSeconds % (int)generatorTime.TotalSeconds;

            var generatorAmount = _generatorData[_levelManager.CurrentIndexLevel].AmountPerTime;
            var capacityAmount = _generatorData[_levelManager.CurrentIndexLevel].Capacity;
            cyclesPassed = Mathf.Min(cyclesPassed, capacityAmount / generatorAmount);

            for (int i = 0; i < cyclesPassed; i++)
            {
                _resourceCollector.AddAmountToStorage();
            }

            StartTimer(TimeSpan.FromSeconds(secondsRemains));

            if (cyclesPassed > 0)
                _saveManager.Save(this);
        }

        public string GetSerialized()
        {
            var saveData = new TimerSaveData()
            {
                StartTime = DateTime.Now.ToString(),
            };
            return JsonUtility.ToJson(saveData);
        }
        #endregion
    }

}