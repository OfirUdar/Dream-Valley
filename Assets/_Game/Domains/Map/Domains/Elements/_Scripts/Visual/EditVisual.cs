﻿using Udar;
using UnityEngine;
using UnityEngine.UI;

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

        private void OnEnable()
        {
            _approveButton.GetComponent<Button>().onClick.AddListener(OnUIApproved);
        }

        private void OnDisable()
        {
            _approveButton.GetComponent<Button>().onClick.RemoveListener(OnUIApproved);
        }

        public void SetPlaceAvailbility(bool isAvailable)
        {
            _editAreaRenderer.material.color =
                isAvailable ? _availableColor : _unavailableColor;

            _approveButton.Activate(isAvailable);
        }

        public void ChangeToIdleMode()
        {
            var gfxPos = _gfx.position;
            gfxPos.y = 0;
            _gfx.position = gfxPos;
            _editAreaRenderer.gameObject.SetActive(false);
            _idleArea.SetActive(true);
        }
        public void ChangeToEditMode()
        {
            var gfxPos = _gfx.position;
            gfxPos.y = 0.3f;
            _gfx.position = gfxPos;
            _editAreaRenderer.gameObject.SetActive(true);
            _idleArea.SetActive(false);
        }


        private void OnUIApproved()
        {
            
        }
    }


    public interface IEditVisual
    {
        public void SetPlaceAvailbility(bool isAvailable);
        public void ChangeToIdleMode();
        public void ChangeToEditMode();
       
    }
}