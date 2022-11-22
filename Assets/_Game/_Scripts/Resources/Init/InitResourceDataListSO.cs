using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Init Resources List", menuName = "Game/Resources/New Init Resources List", order = 0)]
    public class InitResourceDataListSO : ScriptableObject
    {
        [field: SerializeField] public InitResourceData[] Resources { get; private set; }


        [Serializable]
        public class InitResourceData
        {
            public ResourceDataSO ResourceDataSO;
            public int Amount;
        }
    }
}