using UnityEngine;

namespace Udar
{

    [RequireComponent(typeof(CanvasGroup))]
    public class UdarCanvasGroup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [Range(0f, 1f)]
        [SerializeField] private float _activeAlpha = 1f;
        [Range(0f, 1f)]
        [SerializeField] private float _inActiveAlpha = .4f;

        [SerializeField] private bool _isActive;

        public void Activate(bool isActive, bool setInteractive = true)
        {
            _canvasGroup.alpha = isActive ? _activeAlpha : _inActiveAlpha;
            if (setInteractive)
                _canvasGroup.interactable = isActive;
        }
        private void OnValidate()
        {
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();
            Activate(_isActive);
        }
    }
}

