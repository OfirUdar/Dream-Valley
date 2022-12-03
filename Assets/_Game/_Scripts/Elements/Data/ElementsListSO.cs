using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Elements List", menuName = "Game/Map/Elements/Elements List", order = 0)]
    public class ElementsListSO : ScriptableObject
    {
        [field: SerializeField]
        public MapElementSO[] ElementsList { get; private set; }


        private readonly Dictionary<string, MapElementSO> _elementsDictionary 
            = new Dictionary<string, MapElementSO>();

        private void OnEnable()
        {
            foreach (var element in ElementsList)
            {
                var hasAdded = _elementsDictionary.TryAdd(element.GUID, element);
                if (!hasAdded)
                {
                    Debug.Log($"<color=red>{element.Name}</color> guid is already exists- please generate for it", element);
                }
            }
        }

        public MapElementSO GetByGUID(string guid)
        {
            return _elementsDictionary[guid];
        }


    }
}

