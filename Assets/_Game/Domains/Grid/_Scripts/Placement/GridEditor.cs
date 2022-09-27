using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GridEditor : IGridState
    {
        private readonly CamPointerUtility _camPointer;
        private readonly IGrid _grid;

        private IPlaceable _ghost;


        public GridEditor(
            CamPointerUtility camPointer,
            IGrid grid)
        {
            _camPointer = camPointer;
            _grid = grid;
        }

        public void SetPlacement(IPlaceable placement)
        {
            _ghost = placement;
        }


        public void Update()
        {
            if (_ghost == null)
                return;

            SnapHandler();
        }

        private void SnapHandler()
        {
            if (_camPointer.RaycastPointer(out Vector3 hitPoint))
            {
                _grid.GetIndexes(hitPoint, out int row, out int column);

                //Substruct some offset in order to center the pointer
                row -= _ghost.Width / 2;
                column -= _ghost.Height / 2;

                //Keep the object on the grid
                row = Mathf.Clamp(row, 0, _grid.GetRows() - _ghost.Width);
                column = Mathf.Clamp(column, 0, _grid.GetColumns() - _ghost.Height);

                var snappedPosition = _grid.GetWorldPosition(row, column);

                _ghost.Position = snappedPosition;
            }
        }

        public void TryPlace()
        {
            TryPlace(_ghost.Position);
        }
        public void TryPlace(Vector3 position)
        {
            if (_grid.CanPlace(position, _ghost.Width, _ghost.Height))
            {
                _ghost.Position = position;
                _ghost = null;
            }
            else
            {
                Debug.Log("Cannot place");
            }
        }

        public void Enter()
        {
           
        }

        public void Exit()
        {
           
        }
    }

    public class IdleGridState : IState
    {
        public void Enter()
        {

        }

        public void Exit()
        {

        }
    
        public void Update()
        {

        }
    }

   
}