using TMPro;
using Udar;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class InitalizeLoading : MonoBehaviour
    {
        [SerializeField] private SceneField _initalizeScene;
        [SerializeField] private Image _fillBar;
        [SerializeField] private TextMeshProUGUI _progressText;

        private void Start()
        {
            LoadMainScene();
        }

        private async void LoadMainScene()
        {
            await SceneChanger.AwaitLoadAddtiveAsync(_initalizeScene.Name, OnLoadingProgress);
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
