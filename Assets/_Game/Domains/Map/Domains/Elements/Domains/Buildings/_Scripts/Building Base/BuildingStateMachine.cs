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
        [Inject] private readonly Eventor _eventor;

        [Inject(Id = StateType.Active)]
        private readonly IBuildingState _activeState; //Spesific state for building - for example, resource generator state
        [Inject(Id = StateType.Upgrade)]
        private readonly IBuildingState _upgradeState;

        private StateType _currentStateType;
        private IBuildingState _currentState;

        public void Initialize()
        {
            var isSuccess = _loadManager.TryLoad(this);
            _eventor.SpawnedSuccessfully += OnElementSpawnedSuccessfully;
        }

        public void LateDispose()
        {
            _eventor.SpawnedSuccessfully -= OnElementSpawnedSuccessfully;
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

        private void OnElementSpawnedSuccessfully()
        {
            ChangeState(StateType.Upgrade);
        }


        #region Save&Load
        public string Path => SaveLoadKeys.GetBuildingState(_mapElement.Data.GUID, _mapElement.SaveData.InstanceGUID);

        public void SetSerialized(string data)
        {
            var saveData = JsonUtility.FromJson<StateSaveData>(data);
            ChangeState(saveData.StateType);
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