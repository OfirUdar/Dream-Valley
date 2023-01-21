using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ScaleTweener : MonoBehaviour
    {
        [SerializeReference] private float _scaleStartAmount = .9f;
        [SerializeReference] private float _scaleEndAmount = 1.2f;
        [SerializeReference] private float _duration = 0.3f;

        private void OnEnable()
        {
            transform
                .DOScale(_scaleEndAmount, _duration)
                .From(_scaleStartAmount)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine).Play();
        }
        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}
