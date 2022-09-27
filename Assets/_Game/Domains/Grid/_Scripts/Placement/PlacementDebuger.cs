using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PlacementDebuger : MonoBehaviour
    {
        [SerializeField] private BuildingSO _placementSO;
        [Inject]
        private GridPlacerMachine _gridMachine;
        [Inject]
        private PlacementBehaviour.Factory _placementFactory;


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var ob = _placementFactory.Create(_placementSO.Pfb);
                _gridMachine.ChangeToEdit(ob);
            }
        }
    }
}

