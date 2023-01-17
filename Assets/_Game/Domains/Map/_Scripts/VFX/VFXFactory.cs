using System.Collections.Generic;
using UnityEngine;

namespace Game.Map
{
    public class VFXFactory : IVFXFactory
    {
        private readonly Dictionary<VFXType, GameObject> _vfxDictionary =
            new Dictionary<VFXType, GameObject>();

        public VFXFactory(List<VFXData> vfxList)
        {
            for (int i = 0; i < vfxList.Count; i++)
            {
                var vfxData = vfxList[i];
                _vfxDictionary.Add(vfxData.Type, vfxData.EffectPfb);
            }
        }

        public void CreateEffect(VFXType type, Vector3 position)
        {
            var pfb = _vfxDictionary[type];
            GameObject.Instantiate(pfb, position, Quaternion.identity);
        }
    }
}