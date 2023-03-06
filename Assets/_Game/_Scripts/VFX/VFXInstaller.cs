using UnityEngine;
using Zenject;

namespace Game
{
    public class VFXInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Container.BindFactory
            // <Object, VFX, VFX.Factory>()
            // .FromFactory<PrefabFactory<VFX>>();

            //Container.Bind<AbstractPool>().ToSelf().AsSingle();
        }
    }
}