using TMPro;
using Udar;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class AppInitializer : MonoBehaviour
    {
        [SerializeField] private SceneCompositeSO _scenes;
        [SerializeField] private Image _fillBar;
        [SerializeField] private TextMeshProUGUI _progressText;

        private float _targetProgress;

        private void Start()
        {
            LoadMainScene();
        }

        private void Update()
        {
            _fillBar.fillAmount = Mathf.Lerp(_fillBar.fillAmount, _targetProgress, 5 * Time.unscaledDeltaTime);
            _progressText.text = (_fillBar.fillAmount * 100f) + "%";
        }
        private async void LoadMainScene()
        {
            await SceneChanger.AwaitLoadAddtiveAsync(OnLoadingProgress, _scenes.Names);
            await SceneChanger.AwaitUnloadAsync(SceneManager.GetActiveScene().name);
        }


        private void OnLoadingProgress(float progress)
        {
            _targetProgress = progress + 0.1f;
        }

    }
}
