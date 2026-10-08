using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DataImporter
{

    public class GameDataLoadManager : MonoBehaviour
    {
        [SerializeField] private FileImporter _fileImporter;
        [SerializeField] private GameDataDeserializer _gameDataDeserializer;
        [SerializeField] private Image _test;
        [SerializeField] private string _testId;

        private void Start()
        {
            Load().Forget();
        }

        public async UniTask Load()
        {
            Debug.Log("読み込み開始");
            await _fileImporter.LoadFile();

            Debug.Log($"ロード中");
            _gameDataDeserializer.Deserialize();


            Debug.Log("完了");

            await UniTask.WaitForSeconds(1);

            _test.sprite = GameDataContainer.GetImage(_testId);
        }
    }
}