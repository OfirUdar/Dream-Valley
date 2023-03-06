using UnityEngine;
using Zenject;

namespace Game
{
    public class PrefabFactory<T> : IFactory<Object, T>
    {
        private readonly DiContainer _container;

        public PrefabFactory(DiContainer container)
        {
            _container = container;
        }
        public T Create(Object pfb)
        {
            //return _container.InstantiatePrefabForComponent<T>(pfb);
            var context = _container.InstantiatePrefabForComponent<GameObjectContext>(pfb);
            return context.Container.Resolve<T>();
        }
    }
}
