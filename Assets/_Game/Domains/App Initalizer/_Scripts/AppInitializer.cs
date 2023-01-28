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

        private void Start()
        {
            LoadMainScene();
        }

        private async void LoadMainScene()
        {
            await SceneChanger.AwaitLoadAddtiveAsync(OnLoadingProgress, _scenes.Names);
            await SceneChanger.AwaitUnloadAsync(SceneManager.GetActiveScene().name);
        }


        private void OnLoadingProgress(float progress)
        {
            progress += 0.1f;
            _fillBar.fillAmount = progress;
            _progressText.text = (progress * 100f) + "%";
        }

    }
}
