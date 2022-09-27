using Zenject;

namespace Game
{
    public class GridPlacerMachine : ITickable
    {
        public IState _currentState;
        private readonly IGridState _editState;

        public GridPlacerMachine(IState initState, IGridState editState)
        {
            _currentState = initState;
            _editState = editState;
        }
        public void Tick()
        {
            _currentState.Update();
        }

        public void ChangeToEdit(IPlaceable placement)
        {
            _editState.SetPlacement(placement);
            ChangeState(_editState);
        }
        public void ChangeState(IState nextState)
        {
            nextState.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }
    }

    public interface ICondition
    {

    }
    public interface IUpdate
    {
        public void Update();
    }
    public interface IState : IUpdate
    {
        public void Enter();
        public void Exit();
    }
    public interface IGridState : IState
    {
        public void SetPlacement(IPlaceable placement);
    }

}