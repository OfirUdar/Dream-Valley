using System;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.UI
{
    public class UpgraderUI : MonoBehaviour
    {
        [SerializeField] private UITimer _uiTimer;

        [Inject] private readonly IUpgrader _upgrader;

        private void OnEnable()
        {
            _upgrader.UpgradeStarted += OnUpgradeStarted;
            _upgrader.UpgradeFinished += OnUpgradeFinished;
        }
        private void OnDisable()
        {
            _upgrader.UpgradeStarted -= OnUpgradeStarted;
            _upgrader.UpgradeFinished -= OnUpgradeFinished;
        }


        private void OnUpgradeStarted()
        {
            _uiTimer.gameObject.SetActive(true);
        }

        private void OnUpgradeFinished()
        {
            _uiTimer.gameObject.SetActive(false);
        }

    }
}
