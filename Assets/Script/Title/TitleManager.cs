using Common.Scene;
using Common.UI;
using Cysharp.Threading.Tasks;
using DataImporter;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private FadeController _fadeController;
    [SerializeField] private GameDataLoadManager _loadManager;


    public async void StartGame()
    {
        await _fadeController.FadeOut();
        var scene = SceneLoader.LoadScene("InGame");
        var gameData = _loadManager.Load();
        await UniTask.WhenAll(scene, gameData);
        SceneLoader.MoveScene();
    }
}
