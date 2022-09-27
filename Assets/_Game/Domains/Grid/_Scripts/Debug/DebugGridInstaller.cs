using UnityEngine;
using Zenject;

namespace Game
{
    public class DebugGridInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GridDebug>().ToSelf().AsSingle().NonLazy();
        }
    }

}
