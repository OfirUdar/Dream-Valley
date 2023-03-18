using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class VFXPool : IVFXPool, IInitializable
    {
        private readonly Dictionary<int, List<GameObject>>
            _pool = new Dictionary<int, List<GameObject>>();

        private Transform _container;
        public void Initialize()
        {
            _container = new GameObject("VFX Pool").transform;
        }

        public GameObject Spawn(GameObject prefab, Vector3 position)
        {
            int prefabID = prefab.GetInstanceID();
            if (_pool.ContainsKey(prefabID))
            {
                foreach (var instance in _pool[prefabID])
                {
                    if (!instance.activeSelf)
                    {
                        instance.transform.position = position;
                        instance.SetActive(true);
                        return instance;
                    }
                }

                return CreateNewAndAddToExist(prefab, position, prefabID);
            }

            return CreateNewInstanceAndNewInstanceList(prefab, position, prefabID);
        }

        private GameObject CreateNewInstanceAndNewInstanceList(GameObject prefab, Vector3 position, int prefabID)
        {
            var newInstance = GameObject.Instantiate(prefab, position, Quaternion.identity, _container);
            _pool.Add(prefabID, new List<GameObject>() { newInstance });
            return newInstance;
        }

        private GameObject CreateNewAndAddToExist(GameObject prefab, Vector3 position, int prefabID)
        {
            var newInstance = GameObject.Instantiate(prefab, position, Quaternion.identity, _container);
            _pool[prefabID].Add(newInstance);
            return newInstance;
        }


    }
}