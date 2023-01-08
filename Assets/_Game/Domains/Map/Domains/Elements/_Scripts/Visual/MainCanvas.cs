using UnityEngine;

namespace Game.Map.Element
{
    public class MainCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _main;
        private void OnEnable()
        {
            MainUIEventAggregator.ShowRequested += OnShowRequested;
            MainUIEventAggregator.HideRequested += OnHideRequested;
            MainUIEventAggregator.ForceHideRequested += OnHideRequested;
        }

        private void OnDisable()
        {
            MainUIEventAggregator.ShowRequested -= OnShowRequested;
            MainUIEventAggregator.HideRequested -= OnHideRequested;
            MainUIEventAggregator.ForceHideRequested -= OnHideRequested;
        }
        private void OnHideRequested()
        {
            if (_main.activeSelf)
                _main.SetActive(false);
        }
        private void OnShowRequested()
        {
            if (!_main.activeSelf)
                _main.SetActive(true);
        }
    }

}
