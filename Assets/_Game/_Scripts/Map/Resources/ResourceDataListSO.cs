using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Resources List", menuName = "Game/Resources/New Resources List", order = 0)]
    public class ResourceDataListSO : ScriptableObject
    {
        [field: SerializeField] public ResourceDataSO[] Resources { get; private set; }


        private Dictionary<string, ResourceDataSO> _resourcesDictionary = new Dictionary<string, ResourceDataSO>();

        private void OnEnable()
        {
            foreach (var resource in Resources)
            {
                var hasAdded = _resourcesDictionary.TryAdd(resource.GUID, resource);
                if (!hasAdded)
                {
                    Debug.Log($"<color=red>{resource.Name}</color> guid is already exists- please generate for it", resource);
                }
            }
        }

        public ResourceDataSO GetByGUID(string guid)
        {
            return _resourcesDictionary[guid];
        }

    }
}