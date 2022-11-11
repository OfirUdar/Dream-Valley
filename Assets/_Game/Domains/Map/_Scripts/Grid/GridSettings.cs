using System;
using UnityEngine;

namespace Game.Map
{
    [Serializable]
    public class GridSettings
    {
        [field: SerializeField] public int Rows { get; private set; }
        [field: SerializeField] public int Columns { get; private set; }
        [field: SerializeField] public float CellSize{ get; private set; }
    }
}

