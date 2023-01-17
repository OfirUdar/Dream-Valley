using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class VFXInstaller : MonoInstaller
    {
        [SerializeField] private VFXListSO _vfxListSO;
        public override void InstallBindings()
        {
            Container.Bind<List<VFXData>>().FromInstance(_vfxListSO.List);

            Container.Bind<IVFXFactory>().To<VFXFactory>().AsSingle();
        }
    }
}