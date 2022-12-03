using Zenject;

namespace Game.Map.Element.Building
{
    public class BuildingStateMachine : IBuildingStateMachine, ITickable
    {
        private IBuildingState _currentState;

        private readonly IBuildingState _activeState; //Spesific state for building - for example, resource generator state
        private readonly IBuildingState _upgradeState;

        public BuildingStateMachine(IBuildingState activeState, IBuildingState upgradeState)
        {
            _activeState = activeState;
            _upgradeState = upgradeState;
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
        public void ChangeState(IBuildingStateMachine.StateType stateType)
        {
            switch(stateType)
            {
                case IBuildingStateMachine.StateType.Active:
                {
                        ChangeState(_activeState);
                        break;
                }
                case IBuildingStateMachine.StateType.Upgrade:
                    {
                        ChangeState(_upgradeState);
                        break;
                    }
            }
        }
    }
}