using System;
using Udar;
using UnityEngine;

namespace Game.Map.Element.Building
{
    public class UpgradeState : IBuildingState, IUpgrader, ISaveable, ILoadable
    {
        private readonly IMapElement _mapElement;
        private readonly IBuildingStateMachine _machine;
        private readonly IDateTimer _timer;
        private readonly ILevelManager _levelManager;
        private readonly ISaveManager _saveManager;
        private readonly ILoadManager _loadManager;

        public event Action UpgradeStarted;
        public event Action UpgradeFinished;

        public UpgradeState(IMapElement mapElement,
            IBuildingStateMachine machine,
            IDateTimer timer,
            ILevelManager levelManager,
            ISaveManager saveManager,
            ILoadManager loadManager)
        {
            _mapElement = mapElement;
            _machine = machine;
            _timer = timer;
            _levelManager = levelManager;

            _saveManager = saveManager;
            _loadManager = loadManager;
        }


        public void Enter()
        {
            _timer.Finished += OnTimerFinished;

            var isSuccessed = _loadManager.TryLoad(this);
            if (!isSuccessed)
                StartTimer();
        }


        public void Exit()
        {
            _timer.Finished -= OnTimerFinished;
            _timer.Stop();

            _saveManager.TryDelete(this);

            UpgradeFinished?.Invoke();
        }

        public void Tick()
        {

        }


        private void StartTimer()
        {
            var nextLevel = _levelManager.NextLevel;
            var upgradeTime = nextLevel.UpgradeDuration.GetTimeSpan();
            StartTimer(upgradeTime, upgradeTime);

            _saveManager.Save(this);
        }
        private void StartTimer(TimeSpan currentTime, TimeSpan targetTime)
        {
            _timer.SetTime(currentTime, targetTime).Start();

            UpgradeStarted?.Invoke();
        }


        private void OnTimerFinished()
        {
            _levelManager.UpgradgeLevelUp();

            _machine.ChangeState(StateType.Active);
        }


        #region Save&Load

        public string Path => SaveLoadKeys.GetElementUpgraderPath(_mapElement.Data.GUID, _mapElement.SaveData.InstanceGUID);

        public void SetSerialized(string data)
        {
            var saveData = JsonUtility.FromJson<TimerSaveData>(data);
            DateTime.TryParse(saveData.StartTime, out DateTime startTime);

            var nextLevel = _levelManager.NextLevel;
            var upgradeTime = nextLevel.UpgradeDuration.GetTimeSpan();

            var differentTime = (DateTime.Now - startTime);


            var secondsRemains = Mathf.Max(0, (float)(upgradeTime.TotalSeconds - differentTime.TotalSeconds));

            StartTimer(TimeSpan.FromSeconds(secondsRemains), upgradeTime);
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