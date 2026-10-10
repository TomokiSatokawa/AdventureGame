#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

/// <summary>Menu画面にシーンを移動するボタンを追加</summary>
public static class SceneTabWindow
{
    [MenuItem("Scene/0 Title")]
    public static void Scene00()
    {
        EditorSceneManager.SaveOpenScenes();
        OpenScene(0);
    }

    [MenuItem("Scene/1 InGame")]
    public static void Scene01()
    {
        EditorSceneManager.SaveOpenScenes();
        OpenScene(1);
    }

    private static void OpenScene(int sceneIndex)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
        if (!string.IsNullOrEmpty(scenePath))
        {
            EditorSceneManager.OpenScene(scenePath);
        }
    }
}
#endif