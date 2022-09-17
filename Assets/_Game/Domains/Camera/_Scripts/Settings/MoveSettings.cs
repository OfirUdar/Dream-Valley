using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class MoveSettings
    {
        [field: SerializeField] public Vector2 HorizontalLimits { get; private set; } = new Vector2(-12f, 12f);
        [field: SerializeField] public Vector2 VerticalLimits { get; private set; } = new Vector2(-6f, 6f);
    }

}

