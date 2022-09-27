using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class PlacementData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: Space, SerializeField] public int Width { get; private set; }
        [field: SerializeField] public int Height { get; private set; }
    }
}