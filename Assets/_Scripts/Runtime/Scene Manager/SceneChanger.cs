using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Udar
{
    public static class SceneChanger
    {


        #region Load

        /// <summary>
        /// Load multiple scenes addtivly. Not awaitable
        /// </summary>
        /// <param name="sceneNames"></param>
        public static async void LoadAddtiveAsync(params string[] sceneNames)
        {
            await AwaitLoadAddtiveAsync(sceneNames);
        }

        /// <summary>
        /// Load one scene addtivly. Not awaitable
        /// </summary>
        /// <param name="sceneNames"></param>
        public static async void LoadAddtiveAsync(string sceneName, Action<float> progressCallback = null)
        {
            await AwaitLoadAddtiveAsync(sceneName, progressCallback);
        }

        /// <summary>
        /// Load multiple scenes addtivly. Awaitable
        /// </summary>
        /// <param name="sceneNames"></param>
        public static async Task AwaitLoadAddtiveAsync(string sceneName, Action<float> progressCallback = null)
        {
            var operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!operation.isDone)
            {
                progressCallback?.Invoke(operation.progress);
                await Task.Yield();
            }
        }

        /// <summary>
        /// Load one scene addtivly. Awaitable
        /// </summary>
        /// <param name="sceneNames"></param>
        public static async Task AwaitLoadAddtiveAsync(Action<float> progressCallback = null, params string[] sceneNames)
        {
            var tasks = new Task[sceneNames.Length];

            for (int i = 0; i < sceneNames.Length; i++)
            {
                Action<float> decoratorProgressCallback = (p) => progressCallback?.Invoke(p / (sceneNames.Length - i + 1));

                tasks[i] = AwaitLoadAddtiveAsync(sceneNames[i], decoratorProgressCallback);
            }
            await Task.WhenAll(tasks);
        }
        public static async Task AwaitLoadAddtiveAsync(params string[] sceneNames)
        {
            var tasks = new Task[sceneNames.Length];

            for (int i = 0; i < sceneNames.Length; i++)
            {
                tasks[i] = AwaitLoadAddtiveAsync(sceneNames[i]);
            }
            await Task.WhenAll(tasks);
        }
        #endregion


        #region Unload

        /// <summary>
        /// Unload multiple scenes. Not awaitable
        /// </summary>
        /// <param name="sceneNames"></param>
        public static async void UnloadAsync(params string[] sceneNames)
        {
            await AwaitUnloadAsync(sceneNames);
        }

        /// <summary>
        /// Unload one scene. Not awaitable
        /// </summary>
        /// <param name="sceneNames"></param>
        public static async void UnloadAsync(string sceneName, Action<float> progressCallback = null)
        {
            await AwaitUnloadAsync(sceneName, progressCallback);
        }


        /// <summary>
        /// Unload multiple scenes addtivly. Awaitable
        /// </summary>
        /// <param name="sceneNames"></param>
        public static async Task AwaitUnloadAsync(string sceneName, Action<float> progressCallback = null)
        {
            var operation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                progressCallback?.Invoke(operation.progress);
                await Task.Yield();
            }
        }

        /// <summary>
        /// Unload one scene addtivly. Awaitable
        /// </summary>
        /// <param name="sceneNames"></param>
        public static async Task AwaitUnloadAsync(params string[] sceneNames)
        {
            var tasks = new Task[sceneNames.Length];

            for (int i = 0; i < sceneNames.Length; i++)
            {
                tasks[i] = AwaitUnloadAsync(sceneNames[i]);
            }
            await Task.WhenAll(tasks);

        }

        #endregion
    }
}