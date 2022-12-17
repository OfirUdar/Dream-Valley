using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Map.Element.Options
{
    public class UIInfoElementDialog : MonoBehaviour
    {
        [SerializeField] private PanelActivator _panelActivator;
        [Space]
        [SerializeField] private Image _elementImage;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;



        public void Show(IMapElement mapElement)
        {
            var data = mapElement.Data;
            _titleText.text = data.Name;
            _descriptionText.text = data.Description;
            _elementImage.sprite = data.Sprite;

            _panelActivator.Show();
        }

    }
}
