using DG.Tweening;
using UnityEngine;

namespace Game.Map.Grid
{
    public class GroundGridVisual : MonoBehaviour, IGroundGridVisual
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private Tween _showTween;

        private void Awake()
        {
            _showTween = _meshRenderer.material
                .DOFade(.5f, 0.1f)
                .From(0f)
                .OnPlay(Enable)
                .OnRewind(Disable)
                .SetAutoKill(false)
                .SetLink(gameObject);
        }
        private void Enable()
        {
            _meshRenderer.enabled = true;
        }
        private void Disable()
        {
            _meshRenderer.enabled = false;
        }
        public void Hide()
        {
            _showTween.SmoothRewind();
        }

        public void Show()
        {
            _showTween.Restart();
        }
    }
}
