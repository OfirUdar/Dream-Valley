using System.Collections;
using UnityEngine;

namespace Game
{
    public class EditPlaceVisual : MonoBehaviour
    {
        [SerializeField] private Transform _gfx;
        [SerializeField] private MeshRenderer _editAreaRenderer;
        [SerializeField] private GameObject _idleArea;
        [Space]
        [SerializeField] private Color _availableColor = Color.green;
        [SerializeField] private Color _unavailableColor = Color.red;

        public void SetEditingVisual(bool isPlacing)
        {
            if (isPlacing)
                ChangeToEditMode();

            else
                ChangeToIdleMode();
        }

        public void SetPlaceAvailbility(bool isAvailable)
        {
            _editAreaRenderer.material.color = 
                isAvailable ? _availableColor : _unavailableColor;
        }


        private void ChangeToIdleMode()
        {
            var gfxPos = _gfx.position;
            gfxPos.y = 0;
            _gfx.position = gfxPos;
            _editAreaRenderer.gameObject.SetActive(false);
            _idleArea.SetActive(true);
        }
        private void ChangeToEditMode()
        {
            var gfxPos = _gfx.position;
            gfxPos.y = 0.25f;
            _gfx.position = gfxPos;
            _editAreaRenderer.gameObject.SetActive(true);
            _idleArea.SetActive(false);
        }
    }
}