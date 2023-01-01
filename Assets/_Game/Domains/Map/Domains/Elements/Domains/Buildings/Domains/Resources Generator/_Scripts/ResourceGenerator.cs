using System;
using Udar;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.Resources
{
    public class ResourceGenerator : IBuildingState, IResourceGenerator, ISaveable, ILoadable
    {
        [Inject] private readonly ResourceGeneratorLevelsData _generatorData;

        private readonly Profile _profile;
        private readonly IMapElement _mapElement;
        private readonly ISaveManager _saveManager;
        private readonly ILoadManager _loadManager;
        private readonly IDateTimer _timer;
        private readonly ILevelManager _levelManager;

        private int _collectAmount;


        public event Action<bool> CollectableChanged;

        public ResourceGenerator(Profile profile,
            IMapElement mapElement,
            ISaveManager saveManager,
            ILoadManager loadManager,
            IDateTimer timer,
            ILevelManager levelManager)
        {
            _profile = profile;
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
        }


        public void Tick()
        {


        }
        public void Exit()
        {
            _timer.Finished -= OnTimerFinished;
            _timer.Stop();

            _saveManager.TryDelete(this);

            CollectableChanged?.Invoke(false);
        }

        private void StartTimer()
        {
            var generatorTime = _generatorData[_levelManager.CurrentIndexLevel].GetTimeSpan();
            StartTimer(generatorTime);
        }
        private void StartTimer(TimeSpan time)
        {
            _timer.SetTime(time).Start();
            _saveManager.Save(this);
        }


        private void OnTimerFinished()
        {
            AddAmountToCollect();
            var maxAmountCapcity = _generatorData[_levelManager.CurrentIndexLevel].Capacity;
            if (_collectAmount < maxAmountCapcity)
                StartTimer();
        }

        private void AddAmountToCollect()
        {
            if (_collectAmount == 0)
                CollectableChanged?.Invoke(true);

            var addAmount = _generatorData[_levelManager.CurrentIndexLevel].AmountPerTime;
            var maxAmountCapcity = _generatorData[_levelManager.CurrentIndexLevel].Capacity;
            _collectAmount = Mathf.Min(_collectAmount + addAmount, maxAmountCapcity);
        }

        public void Collect()
        {
            if (_collectAmount == 0)
                return;
           
            _profile.ResourcesInventory.AddResource(_generatorData.Resource, _collectAmount);
            _saveManager.TrySave(_profile.ResourcesInventory);

            var maxAmountCapcity = _generatorData[_levelManager.CurrentIndexLevel].Capacity;
            if (_collectAmount == maxAmountCapcity)
                StartTimer();


            _collectAmount = 0;

            CollectableChanged?.Invoke(false);
        }

        public ResourceDataSO GetResource()
        {
            return _generatorData.Resource;
        }





        #region Save&Load
        public string Path => SaveLoadKeys.GetResourceGeneratorPath(_mapElement.Data.GUID, _mapElement.SaveData.InstanceGUID);

        public void SetSerialized(string data)
        {
            var saveData = JsonUtility.FromJson<TimerSaveData>(data);
            DateTime.TryParse(saveData.StartTime, out DateTime startTime);
            var generatorTime = _generatorData[_levelManager.CurrentIndexLevel].GetTimeSpan();
            var differentTime = (DateTime.Now - startTime);


            var cyclesPassed = (int)differentTime.TotalSeconds / (int)generatorTime.TotalSeconds;
            var secondsRemains = (int)differentTime.TotalSeconds % (int)generatorTime.TotalSeconds;

            for (int i = 0; i < cyclesPassed; i++)
            {
                AddAmountToCollect();
            }


            StartTimer(TimeSpan.FromSeconds(secondsRemains));
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