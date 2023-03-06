using UnityEngine;

namespace Game
{
    public interface IVFXFactory
    {
        public void CreateEffect(VFXData data, Vector3 position);
    }
}