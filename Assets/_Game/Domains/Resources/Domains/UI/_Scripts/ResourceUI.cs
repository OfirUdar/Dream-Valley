using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Resources.UI
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private TextMeshProUGUI _capacityText;

        [Inject] private readonly Camera _camera;
        [Inject] private readonly ResourceUITween.Pool _resourceUITweenPool;

        private int _currentAmount;

        public ResourceUI Setup(Sprite sprite, int amount, int capacity)
        {
            _image.sprite = sprite;
            SetAmount(amount);
            SetCapacity(capacity);
            return this;
        }
        public void SetAmount(int amount)
        {
            _amountText.text = amount.ToString();
            _currentAmount = amount;

        }
        public ResourceUI SetCapacity(int capacity)
        {
            _capacityText.text = capacity.ToString();

            return this;
        }
        public ResourceUI SetTweenAmount(int amount)
        {
            DOVirtual.Int(_currentAmount, amount, 2f, OnAmountTweenUpdated)
               .SetEase(Ease.OutSine).Play();

            return this;
        }
        public async void SetTweenAmountAsync(int amount, Vector3 worldPosition)
        {
            //Move resource to the UI
            var resourceTween = _resourceUITweenPool.Spawn();
            await resourceTween.MoveAsync(_image.sprite, _camera.WorldToScreenPoint(worldPosition), _image.rectTransform.position);
            _resourceUITweenPool.Despawn(resourceTween);

            //Add some punch tweening
            _image.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f).Play();

            //Add the resources
            DOVirtual.Int(_currentAmount, amount, 2f, OnAmountTweenUpdated)
               .SetEase(Ease.OutSine).Play();

        }
        private void OnAmountTweenUpdated(int value)
        {
            SetAmount(value);
        }

        public class Factory : PlaceholderFactory<ResourceUI>
        {

        }
    }
}
