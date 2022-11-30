using Zenject;

namespace Game.Map.Element.Building
{
    public class BuildingStateMachine : IBuildingStateMachine, ITickable
    {
        private IBuildingState _currentState;

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

       
    }
}