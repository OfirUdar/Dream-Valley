using System.Collections.Generic;
using UnityEngine;

namespace Game.Map
{
    [CreateAssetMenu(fileName ="VFX Collection",menuName ="Game/Map/VFX/New VFX Collection")]
    public class VFXListSO : ScriptableObject
    {
        [field: SerializeField] public List<VFXData> List { get; private set; }
    }
}