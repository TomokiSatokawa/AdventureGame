using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Scene
{
    /// <summary>
    /// シーンの事前読み込みとシーン移動を管理するクラス。
    /// </summary>
    public static class SceneLoader
    {
        private static AsyncOperation _loadOperation;

        /// <summary>
        /// シーンを読み込み、移動せずに待機します。
        /// </summary>
        public static async UniTask LoadScene(string sceneName)
        {
            if (_loadOperation != null)
            {
                Debug.LogError("[SceneLoader] Scene is already being loaded.");
                return;
            }

            _loadOperation = SceneManager.LoadSceneAsync(
                sceneName,
                LoadSceneMode.Single);

            _loadOperation.allowSceneActivation = false;

            await UniTask.WaitUntil(() => _loadOperation.progress >= 0.9f);
        }

        /// <summary>
        /// 読み込み済みのシーンへ移動します。
        /// </summary>
        public static void MoveScene()
        {
            if (_loadOperation == null)
            {
                Debug.LogError("[SceneLoader] No scene has been loaded.");
                return;
            }

            _loadOperation.allowSceneActivation = true;
            _loadOperation = null;
        }
    }
}