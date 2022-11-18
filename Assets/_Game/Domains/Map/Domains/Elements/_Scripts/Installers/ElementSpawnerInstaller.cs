using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class ElementSpawnerInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.BindFactory
              <Object, FacadeBehaviour, FacadeBehaviour.Factory>()
              .FromFactory<PrefabFactory<FacadeBehaviour>>();

            Container.Bind<IElementSpawner>().To<ElementSpawner>().AsSingle();
        }
    }
}