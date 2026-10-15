using DataImporter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.Log
{
    /// <summary>
    /// ÉçÉOUI
    /// </summary>
    public class LogControl : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        [SerializeField] private Button _selectButton;
        [SerializeField] private TextMeshProUGUI _personName;
        [SerializeField] private TextMeshProUGUI _mainText;

        private ScenarioData _scenarioData;
        private void Start()
        {
            _selectButton.onClick.AddListener(OnClick);
        }

        public void SetData(ScenarioData scenarioData)
        {
            if (!string.IsNullOrWhiteSpace(scenarioData.CharacterID1))
            {
                _characterImage.sprite = GameDataContainer.GetImage(scenarioData.CharacterID1);
            }

            _personName.text = scenarioData.PersonName;
            _mainText.text = scenarioData.Message;
            _scenarioData = scenarioData;
        }

        private void OnClick()
        {
            if (_scenarioData == null) return;

            GameManager.I.Replay(_scenarioData);
        }
    }
}