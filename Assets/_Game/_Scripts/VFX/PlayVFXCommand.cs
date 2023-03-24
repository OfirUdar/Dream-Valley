using UnityEngine;

namespace Game
{
    public class PlayVFXCommand : IEventCommand
    {
        private readonly IVFXPool _vfxPool;
        private readonly GameObject _vfxPfb;

        public PlayVFXCommand(IVFXPool vfxPool, GameObject vfxPfb)
        {
            _vfxPool = vfxPool;
            _vfxPfb = vfxPfb;
        }
        public void Execute(object position)
        {
            _vfxPool.Spawn(_vfxPfb, (Vector3)position);
        }
    }
}