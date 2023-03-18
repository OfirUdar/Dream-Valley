using UnityEngine;

namespace Game
{
    public class PlayVFXCommand : IEventCommand
    {
        private readonly IVFXPool _vfxPool;
        private readonly VFXData _vfxData;

        public PlayVFXCommand(IVFXPool vfxPool, VFXData vfxData)
        {
            _vfxPool = vfxPool;
            _vfxData = vfxData;
        }
        public void Execute(object position)
        {
            _vfxPool.Spawn(_vfxData.EffectPfb, (Vector3)position);
        }
    }
}