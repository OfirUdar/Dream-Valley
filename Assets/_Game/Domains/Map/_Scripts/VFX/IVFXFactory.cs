﻿using UnityEngine;

namespace Game.Map
{
    public interface IVFXFactory
    {
        public void CreateEffect(VFXType type, Vector3 position);
        public void CreateEffect(VFXData data, Vector3 position);
    }
}