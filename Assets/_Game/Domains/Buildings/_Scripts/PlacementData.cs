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
        [field: SerializeField] public ResourcePrice ResourcePrice { get; private set; }
       

    }
    [Serializable]
    public class ResourcePrice
    {
        [field: SerializeField] public ResourceData Resource { get; private set; }
        [field: SerializeField] public int Price { get; private set; }

    }
    [Serializable] // ************ SHOULD TRANSFER IT TO SO (SCRIPTABLE OBJECT)
    public class ResourceData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }

    }
}