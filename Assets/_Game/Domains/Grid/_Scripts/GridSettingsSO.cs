using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName ="Grid Settings",menuName ="Grid/Settings",order =0)]
    public class GridSettingsSO : ScriptableObject
    {
        [field: SerializeField] public GridSettings Settings { get; private set; }
    }
}

