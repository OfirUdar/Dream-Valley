using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class MoveSettings
    {
        [field: SerializeField] public float LerpAmount { get; private set; } = 50f;
        [field: SerializeField] public float InertiaInterpolation { get; private set; } = 0.05f;
        [field: Space,SerializeField] public Vector2 HorizontalLimits { get; private set; } = new Vector2(-12f, 12f);
        [field: SerializeField] public Vector2 VerticalLimits { get; private set; } = new Vector2(-6f, 6f);
    }

}

