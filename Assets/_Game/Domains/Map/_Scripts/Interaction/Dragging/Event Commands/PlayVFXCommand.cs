using UnityEngine;

namespace Game.Map
{
    public class PlayVFXCommand : IEventCommand
    {
        private readonly IVFXFactory _vfxFactory;
        private readonly VFXData _vfxData;

        public PlayVFXCommand(IVFXFactory vfxFactory, VFXData vfxData)
        {
            _vfxFactory = vfxFactory;
            _vfxData = vfxData;
        }
        public void Execute(object position)
        {
            _vfxFactory.CreateEffect(_vfxData, (Vector3)position);
        }
    }
}