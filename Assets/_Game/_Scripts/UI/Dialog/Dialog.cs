using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Dialog : MonoBehaviour, IDialog
    {
        [SerializeField] private PanelActivator _panelActivator;
        [Header("Buttons")]
        [SerializeField] private Button _okButton;
        [SerializeField] private TextMeshProUGUI _okText;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TextMeshProUGUI _cancelText;
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _content;

        private Action _okCallback;
        private Action _cancelCallback;

        private void OnEnable()
        {
            _okButton.onClick.AddListener(OnOkClicked);
            _cancelButton.onClick.AddListener(OnCancelClicked);
        }
        private void OnDisable()
        {
            _okButton.onClick.RemoveListener(OnOkClicked);
            _cancelButton.onClick.RemoveListener(OnCancelClicked);
        }

      
        public void Show(string title, string content, Action okCallback = null)
        {
            _title.text = title;
            _content.text = content;

            _cancelButton.gameObject.SetActive(false);
            _okText.text = "ok";
            _okCallback = okCallback;

            _panelActivator.Show();

        }

        public void ShowComplex(string title, string content, string ok = "ok", string cancel = "cancel", Action okCallback = null, Action cancelCallback = null)
        {
            _title.text = title;
            _content.text = content;

            _cancelButton.gameObject.SetActive(true);
            _cancelText.text = cancel;
            _okText.text = ok;

            _okCallback = okCallback;
            _cancelCallback = cancelCallback;

            _panelActivator.Show();
        }

        private void Hide()
        {
            _panelActivator.Hide();
        }
        public void ForceHide()
        {
            _panelActivator.ForceHide();
        }

        private void OnOkClicked()
        {
            _okCallback?.Invoke();
            Hide();
        }
        private void OnCancelClicked()
        {
            _cancelCallback?.Invoke();
            Hide();
        }
    }

}
