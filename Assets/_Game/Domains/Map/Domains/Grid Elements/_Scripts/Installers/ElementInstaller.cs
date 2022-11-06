using UnityEngine;
using Zenject;

namespace Game.Map.Grid.Element
{
    public class ElementInstaller : MonoInstaller<ElementInstaller>
    {

        public override void InstallBindings()
        {
            Container.BindFactory
               <Object, MapElementBehaviour, MapElementBehaviour.Factory>()
               .FromFactory<PrefabFactory<MapElementBehaviour>>();
        }
    }
}