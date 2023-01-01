using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Map.Element
{
    public class UIInfoDisplay : MonoBehaviour
    {
        [SerializeField] protected PanelActivator _panelActivator;
        [Space]
        [SerializeField] private Image _elementImage;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;


        public void Display(MapElementSO elementDataSO, int currentLevel)
        {
            _elementImage.sprite = elementDataSO.Sprite;
            _titleText.text = elementDataSO.Name + $" (Level {(currentLevel + 1)})";
            _descriptionText.text = elementDataSO.Description;

            _panelActivator.Show();
        }

    }

}