using UnityEngine;

namespace Common.Storage
{
    [CreateAssetMenu(fileName = "FolderData", menuName = "Scriptable Objects/FolderData")]
    public class FolderData : ScriptableObject
    {
        [SerializeField] private string _cashFolder = "Cash";
        [SerializeField] private string _scenarioFolder = "Scenario";
        [SerializeField] private string _assetFolder = "AssetData";
        [SerializeField] private string _imageFolder = "Images";
        [SerializeField] private string _eventFolder = "Event";
        [SerializeField] private string _scenarioVersionsFileName = "ScenarioVersions.csv";
        [SerializeField] private string _assetVersionsFileName = "AssetVersions.csv";
        [SerializeField] private string _eventVersionsFileName = "EventVersions.csv";

        public string CashFolder => _cashFolder;
        public string ScenarioFolder => _scenarioFolder;
        public string AssetFolder => _assetFolder;
        public string ImageFolder => _imageFolder;
        public string EventFolder => _eventFolder;
        public string ScenarioVersionsFileName => _scenarioVersionsFileName;
        public string AssetVersionsFileName => _assetVersionsFileName;
        public string EventVersionsFileName => _eventVersionsFileName;
    }
}