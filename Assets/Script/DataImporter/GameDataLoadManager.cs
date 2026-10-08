using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DataImporter
{

    public class GameDataLoadManager : MonoBehaviour
    {
        [SerializeField] private FileImporter _fileImporter;
        [SerializeField] private GameDataDeserializer _gameDataDeserializer;

        public async UniTask Load()
        {
            Debug.Log("読み込み開始");
            await _fileImporter.LoadFile();

            Debug.Log($"ロード中");
            _gameDataDeserializer.Deserialize();

            Debug.Log("完了");
        }
    }
}