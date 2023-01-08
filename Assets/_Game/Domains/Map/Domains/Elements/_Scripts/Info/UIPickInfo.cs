using DG.Tweening;
using System.Text;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class UIPickInfo : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _container;
        [Space]
        [SerializeField] private TextMeshProUGUI _elementNameText;
        [SerializeField] private TextMeshProUGUI _levelText;


        [Inject] private readonly IMapElement _mapElement;
        [Inject] private readonly ILevelManager _levelManager;

        private Tween _showTween;
        private StringBuilder _levelNumber;

        private void Awake()
        {
            _levelNumber = new StringBuilder();
        }
        private void Start()
        {
            _showTween = _container.DOFade(1f, 0.2f)
                .From(0f)
                .OnPlay(Enable)
                .OnRewind(Disable)
                .SetAutoKill(false)
                .SetLink(gameObject);
                

            _elementNameText.text = _mapElement.Data.Name;
        }

        private void Enable()
        {
            _container.gameObject.SetActive(true);
        }
        private void Disable()
        {
            _container.gameObject.SetActive(false);
        }

        /// <summary>
        /// Could call from Inspector
        /// </summary>
        /// <param name="isSelected"></param>
        public void OnSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                var currentLevel = _levelManager.CurrentIndexLevel + 1;
                _levelNumber.Clear();
                _levelNumber.Append("Level ").Append(currentLevel);

                if (!_levelManager.HasNext())
                    _levelNumber.Append(" (Max)");

                _levelText.text = _levelNumber.ToString();
                _showTween.Restart();
            }
            else
            {
                _showTween.Rewind();
            }
        }
    }

}
