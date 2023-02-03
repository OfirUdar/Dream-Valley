using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Resources.UI
{
    public class ResourceUITween : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _resourceImage;

        private Tween _scaleOutTween;

        private TweenerCore<Vector3, Vector3, VectorOptions> _moveTween;

        private void Awake()
        {
            _scaleOutTween = _rectTransform.DOScale(1f, .15f)
                .From(0f)
                .SetEase(Ease.OutBack)
                .SetAutoKill(false)
                .SetLink(gameObject);

            _moveTween = _rectTransform.DOMove(Vector3.one, .5f)
               .From(Vector3.zero)
               .SetEase(Ease.InOutQuad)
               .SetAutoKill(false)
               .SetLink(gameObject);

        }

        public async Task MoveAsync(Sprite sprite, Vector3 startScreenPoint, Vector3 endScreenPoint)
        {
            _resourceImage.sprite = sprite;

            _moveTween.ChangeValues(startScreenPoint, endScreenPoint);

            _scaleOutTween.Restart();
            await _scaleOutTween.AsyncWaitForCompletion();
            await Task.Delay(30);
            _moveTween.Restart();

            await _moveTween.AsyncWaitForCompletion();
        }

        public class Pool : MonoMemoryPool<ResourceUITween>
        {

        }

    }
}