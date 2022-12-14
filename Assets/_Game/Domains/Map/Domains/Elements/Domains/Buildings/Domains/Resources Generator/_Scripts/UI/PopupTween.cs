using DG.Tweening;
using UnityEngine;

namespace Game.Map.Element.Building.Resources.UI
{
    public class PopupTween : MonoBehaviour
    {
        [SerializeField] private GameObject _popup;
        private Tween _showTween;

        private void Awake()
        {
            _showTween = _popup.transform
                .DOScale(1f, 0.1f)
                .SetEase(Ease.OutCubic)
                .From(0f)
                .OnPlay(Enable)
                .OnRewind(Disable)
                .SetAutoKill(false)
                .SetLink(_popup);
        }


        public void AutoActivate(bool isShow)
        {
            if (isShow)
                Show();
            else
                Hide();
        }
        public void Show()
        {
            _showTween.Restart();
        }
        public void Hide()
        {
            _showTween.SmoothRewind();
        }
        public void ForceHide()
        {
            Disable();
        }

        private void Disable()
        {
            _popup.SetActive(false);
        }
        private void Enable()
        {
            _popup.SetActive(true);
        }
    }
}
