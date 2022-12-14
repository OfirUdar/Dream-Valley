using Zenject;

namespace Game.Map.Element.Building
{
    public class BuildingStateMachine : IBuildingStateMachine, ITickable, IInitializable
    {
        private IBuildingState _currentState;

        [Inject(Id = StateType.Active)]
        private readonly IBuildingState _activeState; //Spesific state for building - for example, resource generator state
        [Inject(Id = StateType.Upgrade)]
        private readonly IBuildingState _upgradeState;


        public void Initialize()
        {
            ChangeState(StateType.Active);
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

    }
}