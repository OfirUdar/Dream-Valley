using System;
using Udar;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building
{
    public class BuildingStateMachine : IBuildingStateMachine, ITickable, IInitializable, ILateDisposable, ISaveable, ILoadable
    {
        [Inject] private readonly IMapElement _mapElement;
        [Inject] private readonly ISaveManager _saveManager;
        [Inject] private readonly ILoadManager _loadManager;
        [Inject] private readonly UpgradeCompletedCommand.Pool _upgradeCompletedCommandPool;
        [Inject] private readonly UpgradeStartedCommand.Pool _upgradeStartedCommandPool;
        private readonly IEventor _eventor;

        [Inject(Id = StateType.Active)]
        private readonly IBuildingState _activeState; //Spesific state for building - for example, resource generator state
        [Inject(Id = StateType.Upgrade)]
        private readonly IBuildingState _upgradeState;

        private StateType _currentStateType;
        private IBuildingState _currentState;

        public BuildingStateMachine(IEventor eventor)
        {
            _eventor = eventor;
            _eventor.SpawnedSuccessfully += OnElementSpawnedSuccessfully;
            _eventor.UpgradeRequested += OnUpgradeRequested;
        }

        public void Initialize()
        {
            _loadManager.TryLoad(this);
        }

        public void LateDispose()
        {
            _eventor.SpawnedSuccessfully -= OnElementSpawnedSuccessfully;
            _eventor.UpgradeRequested -= OnUpgradeRequested;
        }

        public void Tick()
        {
            _currentState?.Tick();
        }

        public void ChangeState(IBuildingState nextState)
        {
            _currentState?.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }

        public void ChangeState(StateType stateType)
        {
            FireStateChangedCommand(stateType);
            ChangeStateWithoutNotify(stateType);
        }

        public void ChangeStateWithoutNotify(StateType stateType)
        {
            _currentStateType = stateType;
            _saveManager.Save(this);
            switch (stateType)
            {
                case StateType.Active:
                    {
                        ChangeState(_activeState);
                        break;
                    }
                case StateType.Upgrade:
                    {
                        ChangeState(_upgradeState);
                        break;
                    }
            }

        }

        private void FireStateChangedCommand(StateType stateType)
        {
            if (_currentStateType == StateType.Upgrade && stateType == StateType.Active)
            {
                var command = _upgradeCompletedCommandPool.Spawn();
                command.Execute(_mapElement.Center);
                _upgradeCompletedCommandPool.Despawn(command);
            }

            if (_currentStateType == StateType.Active && stateType == StateType.Upgrade)
            {
                var command = _upgradeStartedCommandPool.Spawn();
                command.Execute(_mapElement.Center);
                _upgradeStartedCommandPool.Despawn(command);
            }
        }

        public StateType GetCurrentState()
        {
            return _currentStateType;
        }


        private void OnElementSpawnedSuccessfully()
        {
            ChangeState(StateType.Upgrade);
        }

        private void OnUpgradeRequested()
        {
            ChangeState(StateType.Upgrade);
        }


        #region Save&Load
        public string Path => SaveLoadKeys.GetBuildingState(_mapElement.Data.GUID, _mapElement.SaveData.InstanceGUID);

        public void SetSerialized(string data)
        {
            var saveData = JsonUtility.FromJson<StateSaveData>(data);
            ChangeStateWithoutNotify(saveData.StateType);
        }

        public string GetSerialized()
        {
            var saveData = new StateSaveData()
            {
                StateType = _currentStateType,
            };
            return JsonUtility.ToJson(saveData);
        }


        [Serializable]
        public struct StateSaveData
        {
            public StateType StateType;
        }
        #endregion
    }
}