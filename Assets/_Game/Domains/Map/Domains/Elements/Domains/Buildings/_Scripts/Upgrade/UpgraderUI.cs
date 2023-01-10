using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.UI
{
    public class UpgraderUI : MonoBehaviour
    {
        [SerializeField] private CanvasActivator _canvasActivator;
        [Space]
        [SerializeField] private UITimer _uiTimer;

        [Inject] private readonly IUpgrader _upgrader;

        private void Awake()
        {
            _upgrader.UpgradeStarted += OnUpgradeStarted;
            _upgrader.UpgradeFinished += OnUpgradeFinished;
        }
        private void OnDestroy()
        {
            _upgrader.UpgradeStarted -= OnUpgradeStarted;
            _upgrader.UpgradeFinished -= OnUpgradeFinished;
        }

        private void OnUpgradeStarted()
        {
            _canvasActivator.Activate();
        }

        private void OnUpgradeFinished()
        {
            _canvasActivator.Deactivate();
        }

    }
}
