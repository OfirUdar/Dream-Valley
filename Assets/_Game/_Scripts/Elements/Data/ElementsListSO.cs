﻿using System.Collections.Generic;
using UnityEngine;

namespace Game.Map.Element
{
    [CreateAssetMenu(fileName = "Elements List", menuName = "Game/Map/Elements/Elements List", order = 0)]
    public class ElementsListSO : ScriptableObject
    {
        [field: SerializeField]
        public List<MapElementSO> ElementsList { get; private set; }
            = new List<MapElementSO>();

    }
}
