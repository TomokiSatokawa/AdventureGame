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
        [SerializeField] private TextMeshProUGUI _personName;
        [SerializeField] private TextMeshProUGUI _mainText;

        public void SetData(ScenarioData scenarioData)
        {
            if (!string.IsNullOrWhiteSpace(scenarioData.CharacterID1))
            {
                   _characterImage.sprite = GameDataContainer.GetImage(scenarioData.CharacterID1);
            }

            _personName.text = scenarioData.PersonName;
            _mainText.text = scenarioData.Message;
        }
    }
}