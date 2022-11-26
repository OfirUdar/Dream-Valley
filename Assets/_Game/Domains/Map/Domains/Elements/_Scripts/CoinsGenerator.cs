using System.Linq;
using Udar;
using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class CoinsGenerator : MonoBehaviour
    {
        [SerializeField] private ResourceDataSO _resource;

        private Profile _profile;
        private ISaveManager _saveManager;
        private ILoadManager _loadManager;

        [Inject]
        public void Init(Profile profile, ISaveManager saveManager,ILoadManager loadManager)
        {
            _profile = profile;
            _saveManager = saveManager;
            _loadManager = loadManager;
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount == 1)
        //        _profile.ResourcesInventory.AddResource(_resource, 100);

        //    if (Input.GetKeyDown(KeyCode.S) || Input.touchCount == 2)
        //        _saveManager.Save(_profile.ResourcesInventory);

        //    if (Input.GetKeyDown(KeyCode.L)||Input.touchCount == 3)
        //    {
        //        _loadManager.Load(_profile.ResourcesInventory);
        //    }
        //}
    }
}