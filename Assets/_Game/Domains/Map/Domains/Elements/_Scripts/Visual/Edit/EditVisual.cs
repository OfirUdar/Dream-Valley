using UnityEngine;
using Zenject;

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

        [Inject] private readonly IPlaceApprover _placeApprover;
        [Inject] private readonly IGroundGridVisual _groundVisual;

        private void OnDestroy()
        {
            _groundVisual.Hide();
        }

        public void SetPlaceAvailbility(bool isAvailable)
        {
            _editAreaRenderer.material.color =
                isAvailable ? _availableColor : _unavailableColor;

            _placeApprover.SetPlaceAvailbility(isAvailable);
        }

        public void ChangeToIdleMode()
        {
            _gfx.localScale = Vector3.one;
            _gfx.localPosition = Vector3.zero;

            var editAreaPosition = _editAreaRenderer.transform.localPosition;
            editAreaPosition.y -= 0.025f;
            _editAreaRenderer.transform.localPosition = editAreaPosition;
            _editAreaRenderer.gameObject.SetActive(false);

            _groundVisual.Hide();
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

            _groundVisual.Show();
             _idleArea.SetActive(false);
        }



    }
}