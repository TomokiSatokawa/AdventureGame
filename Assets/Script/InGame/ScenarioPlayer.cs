using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DataImporter;
using InGame.Branch;
using InGame.Message;
using UnityEngine;

namespace InGame
{
    /// <summary>
    /// シナリオ再生
    /// </summary>
    public class ScenarioPlayer : MonoBehaviour
    {
        [SerializeField] private MessageControl _messageControl;
        [SerializeField] private CharacterController _background;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CharacterController _myCharacter;
        [SerializeField] private ButtonsController _buttonsController;

        private List<ScenarioData> _currentScenario;
        private string _currentScenarioName;
        public List<ScenarioData> CurrentScenario => _currentScenario;
        public string CurrentScenarioName => _currentScenarioName;

        private void Start()
        {
            //再生データが有効か
            if (ScenarioPlaybackData.I && !string.IsNullOrWhiteSpace(ScenarioPlaybackData.I.StartScenario))
            {
                PlayScenario(ScenarioPlaybackData.I.StartScenario,ScenarioPlaybackData.I.StartIndex);
            }
            else
            {
                Debug.LogWarning("シナリオ再生データが見つかりませんでした");
                PlayScenario("Mainテスト",0);
            }
        }

        public async void PlayScenario(string scenarioName,int start)
        {
            _currentScenario = GameDataContainer.GetScenario(scenarioName);
            _currentScenarioName = scenarioName;

            //再生開始位置を指定
            var startIndex = Mathf.Min(start, _currentScenario.Count - 1);
            Debug.Log(startIndex  + " " + start);

            ScenarioData backGround = null;
            ScenarioData myCharacter = null;
            ScenarioData character = null;

            //再生開始位置時の状態を再現
            for (int i = 0; i < startIndex; i++)
            {
                ScenarioData scenario = _currentScenario[i];

                if (!string.IsNullOrWhiteSpace(scenario.BackgroundID))
                    backGround = scenario;

                if (!string.IsNullOrWhiteSpace(scenario.MyCharacterID))
                    myCharacter = scenario;

                if (!string.IsNullOrWhiteSpace(scenario.CharacterID1))
                    character = scenario;
            }

            if (backGround != null)
                SetCharacterImage(_background, backGround.BackgroundID, "0");

            if (myCharacter != null)
                SetCharacterImage(_myCharacter, myCharacter.MyCharacterID, "0");

            if (character != null)
                SetCharacterImage(_characterController, character.CharacterID1, "0");

            while (true)
            {
                for (int i = startIndex; i < _currentScenario.Count; i++)
                {
                    ScenarioData scenario = _currentScenario[i];

                    //キャラ設定
                    SetCharacterImage(_background, scenario.BackgroundID, scenario.BackgroundChangeType);
                    SetCharacterImage(_myCharacter, scenario.MyCharacterID, scenario.MyCharacterChangeType);
                    SetCharacterImage(_characterController, scenario.CharacterID1, scenario.CharacterChangeType1);
                    //会話ウインドウ
                    await _messageControl.ShowMessage(scenario.PersonName, scenario.Message);

                    if (string.IsNullOrEmpty(scenario.EventType))
                        continue;

                    bool isEndScenario = false;
                    switch (scenario.EventType[0])
                    {
                        case '2':
                            var select = await _buttonsController.ShowButton(GameDataContainer.GetBranch(scenario.EventID));
                            _currentScenario = GameDataContainer.GetScenario(select);
                            _currentScenarioName = select;
                            isEndScenario = true;
                            break;

                    }

                    if (isEndScenario)
                        break;
                }
                await UniTask.Yield();
                startIndex = 0;
            }
        }

        private void SetCharacterImage(CharacterController cc, string id, string changeType)
        {
            if (string.IsNullOrEmpty(id)) return;

            var sprite = GameDataContainer.GetImage(id);
            cc.ShowCharacter(sprite, changeType);
        }
    }
}
