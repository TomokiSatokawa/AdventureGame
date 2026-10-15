using Common.Scene;
using Common.UI;
using Cysharp.Threading.Tasks;
using DataImporter;
using UnityEngine;

namespace InGame
{

    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        [SerializeField] private FadeController _fade;
        [SerializeField] private ScenarioPlayer _scenarioPlayer;

        public async void Replay(ScenarioData scenarioData)
        {
            var index = _scenarioPlayer.CurrentScenario.FindIndex(x => x == scenarioData);
            ScenarioPlaybackData.I.SetData(_scenarioPlayer.CurrentScenarioName, index);

            var fade = _fade.FadeOut();
            var scene = SceneLoader.LoadScene("InGame");
            await UniTask.WhenAll(fade, scene);
            SceneLoader.MoveScene();
        }
    }
}
