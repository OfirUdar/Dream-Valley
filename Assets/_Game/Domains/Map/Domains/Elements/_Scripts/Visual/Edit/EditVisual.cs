using Udar;
using UnityEngine;

namespace Game.Map.Element
{
    public class EditVisual : MonoBehaviour, IEditVisual
    {
        [SerializeField] private Transform _gfx;
        [SerializeField] private GameObject _idleArea;
        [SerializeField] private MeshRenderer _editAreaRenderer;

        [Space]
        [SerializeField] private Color _availableColor = Color.green;
        [SerializeField] private Color _unavailableColor = Color.red;

        [Space]
        [SerializeField] private UdarCanvasGroup _approveButton;


        public void SetPlaceAvailbility(bool isAvailable)
        {
            _editAreaRenderer.material.color =
                isAvailable ? _availableColor : _unavailableColor;

            _approveButton.Activate(isAvailable);
        }

        public void ChangeToIdleMode()
        {
            _gfx.localScale = Vector3.one;
            _gfx.localPosition = Vector3.zero;

            var editAreaPosition = _editAreaRenderer.transform.localPosition;
            editAreaPosition.y -= 0.025f;
            _editAreaRenderer.transform.localPosition= editAreaPosition;
            _editAreaRenderer.gameObject.SetActive(false);

            _idleArea.SetActive(true);
        }
        public void ChangeToEditMode()
        {
            _gfx.localScale = Vector3.one * 0.95f;

            var gfxPosition = _gfx.localPosition;
            gfxPosition.y = 0.025f;
            _gfx.localPosition = gfxPosition;

            var editAreaPosition = _editAreaRenderer.transform.localPosition;
            editAreaPosition.y += 0.025f;
            _editAreaRenderer.transform.localPosition = editAreaPosition;
            _editAreaRenderer.gameObject.SetActive(true);

            _idleArea.SetActive(false);
        }



    }
}