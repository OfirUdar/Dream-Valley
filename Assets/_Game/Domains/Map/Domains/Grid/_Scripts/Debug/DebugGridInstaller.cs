using UnityEngine;
using Zenject;

namespace Game.Map.Grid
{
    public class DebugGridInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GridDebug>().ToSelf().AsSingle().NonLazy();
        }
    }

}
