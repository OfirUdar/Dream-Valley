using UnityEngine;

namespace Game.Map
{
    [CreateAssetMenu(fileName ="Grid Settings",menuName ="Game/Map/Grid/Settings",order =0)]
    public class GridSettingsSO : ScriptableObject
    {
        [field: SerializeField] public int Rows { get; private set; } = 30;
        [field: SerializeField] public int Columns { get; private set; } = 30;
        [field: SerializeField] public float CellSize { get; private set; } = 0.75f;
    }
}

