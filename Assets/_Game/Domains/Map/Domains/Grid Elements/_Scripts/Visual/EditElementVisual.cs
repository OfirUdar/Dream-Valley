using UnityEngine;


namespace Game.Map.Grid.Element
{
    public class EditElementVisual : MonoBehaviour
    {
        [SerializeField] private Transform _gfx;
        [SerializeField] private MeshRenderer _editAreaRenderer;

        [Space]
        [SerializeField] private Color _availableColor = Color.green;
        [SerializeField] private Color _unavailableColor = Color.red;


        public void SetPlaceAvailbility(bool isAvailable)
        {
            _editAreaRenderer.material.color =
                isAvailable ? _availableColor : _unavailableColor;

        }

        public void ChangeToIdleMode()
        {
            var gfxPos = _gfx.position;
            gfxPos.y = 0;
            _gfx.position = gfxPos;
            _editAreaRenderer.gameObject.SetActive(false);
        }
        public void ChangeToEditMode()
        {
            var gfxPos = _gfx.position;
            gfxPos.y = 0.3f;
            _gfx.position = gfxPos;
            _editAreaRenderer.gameObject.SetActive(true);
        }
    }
}