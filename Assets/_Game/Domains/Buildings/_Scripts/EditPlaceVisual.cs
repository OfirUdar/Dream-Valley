using DG.Tweening;
using Udar;
using UnityEngine;
namespace Game
{
    public class EditPlaceVisual : MonoBehaviour
    {
        [SerializeField] private Transform _gfx;
        [SerializeField] private MeshRenderer _editAreaRenderer;
        [SerializeField] private GameObject _idleArea;
        [Space]
        [SerializeField] private GameObject _purchaseButtons;
        [SerializeField] private UdarCanvasGroup _approveActivator;

        [Space]
        [SerializeField] private Color _availableColor = Color.green;
        [SerializeField] private Color _unavailableColor = Color.red;
        [Space]
        [SerializeField] private float _selectedOpacity = 0.5f;
        [SerializeField] private float _duration = 1f;

        private Renderer[] _renderers;

        private void Awake()
        {
            _renderers = _gfx.GetComponentsInChildren<Renderer>(true);
        }


        
        public void SetPlaceAvailbility(bool isAvailable)
        {
            _editAreaRenderer.material.color =
                isAvailable ? _availableColor : _unavailableColor;

            _approveActivator.Activate(isAvailable);
        }

        public void OnSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                foreach (var renderer in _renderers)
                    renderer.material.
                        DOFade(_selectedOpacity, _duration)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.OutSine);
            }
            else
            {
                foreach (var renderer in _renderers)
                {
                    renderer.material.DOKill();
                    renderer.material.DOFade(1f, 0f);
                }
            }


        }


        public void ChangeToIdleMode()
        {
            var gfxPos = _gfx.position;
            gfxPos.y = 0;
            _gfx.position = gfxPos;
            _editAreaRenderer.gameObject.SetActive(false);
            _idleArea.SetActive(true);

            _purchaseButtons.SetActive(false);

        }
        public void ChangeToEditMode(bool isAlreadyPlaced)
        {
            var gfxPos = _gfx.position;
            gfxPos.y = 0.3f;
            _gfx.position = gfxPos;
            _editAreaRenderer.gameObject.SetActive(true);
             _idleArea.SetActive(false);

            if (!isAlreadyPlaced)
                _purchaseButtons.SetActive(true);

        }
    }
}