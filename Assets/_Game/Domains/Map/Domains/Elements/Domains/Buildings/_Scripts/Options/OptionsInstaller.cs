using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building
{
    public class OptionsInstaller : MonoInstaller
    {
        [SerializeField] private ElementOptionsSO _elementOptionsSO;
        public override void InstallBindings()
        {
            Container.Bind<ElementOptionsSO>().FromInstance(_elementOptionsSO);

            Container.Bind<IOptionsDisplayer>().To<OptionsDisplayer>().AsSingle();
        }
    }

}