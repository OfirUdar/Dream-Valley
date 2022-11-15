using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Resource", menuName = "Game/Resources/New Resource", order = 0)]
    [Serializable]
    public class ResourceDataSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }

    }

}