using System.Threading.Tasks;
using Udar.SceneField;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class ScenesCollection : MonoBehaviour
    {
        [field: SerializeField] public SceneField MainUI { get; private set; }
        [field: SerializeField] public SceneField IdleEditState { get; private set; }
        [field: SerializeField] public SceneField PurchaseEditState { get; private set; }

        public void Start()
        {
            LoadAddtive(MainUI.SceneName);
            LoadAddtive(IdleEditState.SceneName);
        }

        public void LoadAddtive(string sceneName)
        {
            SceneManager.LoadScene(sceneName,
                LoadSceneMode.Additive);
        }
        public async Task Unload(string sceneName)
        {
            var operation = SceneManager.UnloadSceneAsync(sceneName);

            while(!operation.isDone)
                await Task.Yield();
        }

    }
}