using Cysharp.Threading.Tasks;
using DataImporter;
using InGame.Branch;
using InGame.Message;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    [SerializeField] private MessageControl _messageControl;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterController _characterController2;
    [SerializeField] private ButtonsController _buttonsController;
    [SerializeField] private string _a;
    [SerializeField, TextArea(3, 10)] private string _b;

    private async void OnEnable()
    {
        var currentScenario = GameDataContainer.GetScenario("Mainテスト");
        while (true)
        {
            foreach (var scenario in currentScenario)
            {
                //キャラ設定
                SetCharacterImage(_characterController, scenario.MyCharacterID, scenario.MyCharacterChangeType);
                SetCharacterImage(_characterController2, scenario.CharacterID1, scenario.CharacterChangeType1);
                //会話ウインドウ
                await _messageControl.ShowMessage(scenario.PersonName, scenario.Message);

                if (string.IsNullOrEmpty(scenario.EventType))
                    continue;

                bool isEndScenario = false;
                switch (scenario.EventType[0])
                {
                    case '2':
                        var select =  await _buttonsController.ShowButton(GameDataContainer.GetBranch(scenario.EventID));
                        currentScenario = GameDataContainer.GetScenario(select);
                        isEndScenario = true;
                        break;

                }

                if (isEndScenario)
                    break;
            }
            await UniTask.Yield();
        }
    }

    private void SetCharacterImage(CharacterController cc, string id,string changeType)
    {
        if (string.IsNullOrEmpty(id)) return;

        var sprite = GameDataContainer.GetImage(id);
        cc.ShowCharacter(sprite, changeType);
    }
}
