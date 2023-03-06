using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    [RequireComponent(typeof(Button))]
    public class PlayButtonSound : MonoBehaviour
    {
        [SerializeField] private AudioClipInfoSO _audioInfo;

        private ISoundsManager _sfxManager;

        private Button _button;

        private void Awake()
        {
            _button = transform.GetComponent<Button>();
        }
        private void OnEnable()
        {
            _button.onClick.AddListener(PlayOneShot);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(PlayOneShot);
        }

        [Inject]
        public void Init(ISoundsManager sfxManager)
        {
            _sfxManager = sfxManager;
        }


        public void PlayOneShot()
        {
            _sfxManager.PlayOneShot(_audioInfo);
        }
    }
    public enum GameEvent
    {
        ButtonClicked,

        ElementPlaced,
        ElementPlacedError,
        ElementDragging,

        ElementSelected,
        ElementUnselected,

    }
}
