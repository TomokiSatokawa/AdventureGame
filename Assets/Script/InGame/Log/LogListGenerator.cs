using DataImporter;
using UnityEngine;

namespace InGame.Log
{

    public class LogListGenerator : MonoBehaviour
    {
        [SerializeField] private LogControl _logControl;
        [SerializeField] private RectTransform _content;
        [SerializeField] private ScenarioPlayer _scenarioPlayer;

        public void ShowLog()
        {
            DeleteAllChild();
            foreach (var scenarioData in _scenarioPlayer.CurrentScenario)
            {
                if (string.IsNullOrWhiteSpace(scenarioData.Message)) break;

                var log = Instantiate(_logControl, _content);
                log.SetData(scenarioData);
            }
        }

        private void DeleteAllChild()
        {
            for (int i = _content.childCount - 1; i >= 0; i--)
            {
                Destroy(_content.GetChild(i).gameObject);
            }
        }
    }
}
