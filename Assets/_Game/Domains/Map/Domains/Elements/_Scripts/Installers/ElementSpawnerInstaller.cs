using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class ElementSpawnerInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.BindFactory
              <Object, IMapElement, MapElementFactory>()
              .FromFactory<PrefabFactory<IMapElement>>();

            Container.BindInterfacesTo<ElementSpawner>().AsSingle();

            Container.Bind<ElementSpawnerAggragator>().ToSelf().AsSingle();
        }
    }
}