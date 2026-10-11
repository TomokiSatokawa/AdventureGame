using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DataImporter;
using InGame.Branch;
using InGame.Message;
using UnityEngine;

namespace InGame
{

    public class ScenarioPlayer : MonoBehaviour
    {
        [SerializeField] private MessageControl _messageControl;
        [SerializeField] private CharacterController _background;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CharacterController _characterController2;
        [SerializeField] private ButtonsController _buttonsController;

        private IReadOnlyList<ScenarioData> _currentScenario;
        public IReadOnlyList<ScenarioData> CurrentScenario => _currentScenario;
        private async void Start()
        {
            _currentScenario = GameDataContainer.GetScenario("Mainテスト");

            while (true)
            {
                foreach (var scenario in _currentScenario)
                {
                    //キャラ設定
                    SetCharacterImage(_background, scenario.BackgroundID, scenario.BackgroundChangeType);
                    SetCharacterImage(_characterController2, scenario.MyCharacterID, scenario.MyCharacterChangeType);
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
                            isEndScenario = true;
                            break;

                    }

                    if (isEndScenario)
                        break;
                }
                await UniTask.Yield();
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
