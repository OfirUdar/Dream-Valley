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

        private int _currentAmount;

       

        public void Setup(Sprite sprite, int amount)
        {
            _image.sprite = sprite;
            SetAmount(amount);
        }
        public void SetAmount(int amount)
        {
            _amountText.text = amount.ToString() + "/100";
            _currentAmount = amount;
        }
        public void SetTweenAmount(int amount)
        {
             DOVirtual.Int(_currentAmount, amount, 2f, OnAmountTweenUpdated)
                .SetEase(Ease.OutSine).Play();
        }
        public void OnAmountTweenUpdated(int value)
        {
            SetAmount(value);
        }
    }
}
