using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Resources.UI
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private TextMeshProUGUI _capacityText;

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
        private void OnAmountTweenUpdated(int value)
        {
            SetAmount(value);
        }
    }
}
