using System.Collections.Generic;
using Common.Storage;
using Csv;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace DataImporter
{
    public class FileImporter : MonoBehaviour
    {

        [SerializeField] private FolderData _folderData;
        [SerializeField] private ImageImporter _imageImporter;
        [SerializeField] private SpreadsheetURL _scenarioURL;
        [SerializeField] private SpreadsheetURL _imageURL;

        public async UniTask LoadFile()
        {
            Debug.Log("シナリオ更新");
            //シナリオ更新
            await LoadSpreadSheet(_folderData.ScenarioVersionsFileName, _folderData.ScenarioFolder, _scenarioURL);

            Debug.Log("画像データ更新");
            //画像データ更新
            var loadImageFile = await LoadSpreadSheet(_folderData.AssetVersionsFileName, _folderData.AssetFolder, _imageURL);

            foreach (var sheet in loadImageFile)
            {
                Debug.Log($"{sheet} 画像読み込み");
                var directory = FileStorage.GetDirectory(_folderData.CashFolder, _folderData.AssetFolder);
                var csv = FileStorage.LoadFile(directory, sheet + ".csv");
                var imageDatas = CsvSerializer.Deserialize<ImageIDData>(csv);
                await _imageImporter.DownloadImages(imageDatas);
            }
        }

        private async UniTask<List<string>> LoadSpreadSheet(string versionFile, string sectionDirectory, SpreadsheetURL urlData)
        {
            var importVersionData = await DownloadFile(urlData.GetURL());
            List<string> loadFile = GetSheetsToUpdate(importVersionData, versionFile);

            var directory = FileStorage.GetDirectory(_folderData.CashFolder, sectionDirectory);
            foreach (var sheet in loadFile)
            {
                string url = SpreadsheetURL.CreateURL(urlData.SpreadSheetID, sheet);
                var csv = await DownloadFile(url);
                FileStorage.SaveOrCreateFile(directory, sheet + ".csv", csv);
            }

            return loadFile;
        }

        private List<string> GetSheetsToUpdate(string importVersionCSV, string versionFileName)
        {
            var importVersionData = CsvSerializer.Deserialize<VersionFileData>(importVersionCSV);
            var storageVersionFile = FileStorage.LoadFile(_folderData.CashFolder, versionFileName);
            GameDataContainer.SetVersion(importVersionData);

            List<string> loadFile = new();

            //バージョンデータなし
            if (storageVersionFile == null)
            {
                //すべてのファイルをロードする
                foreach (var versionData in importVersionData)
                {
                    loadFile.Add(versionData.SheetName);
                }
            }
            else
            {
                var storageVersionData = CsvSerializer.Deserialize<VersionFileData>(storageVersionFile);

                //シート名のDictionaryを作成
                var versionDic = new Dictionary<string, VersionFileData>();
                foreach (var versionData in storageVersionData)
                {
                    versionDic.Add(versionData.SheetName, versionData);
                }

                //Version一致しないのをロードリストに入れる
                foreach (var versionData in importVersionData)
                {
                    if (versionDic.TryGetValue(versionData.SheetName, out var data)
                        && data.Version != versionData.Version)
                    {
                        loadFile.Add(versionData.SheetName);
                    }
                }
            }

            FileStorage.SaveOrCreateFile(_folderData.CashFolder, versionFileName, importVersionCSV);
            return loadFile;
        }

        private async UniTask<string> DownloadFile(string url)
        {
            var req = UnityWebRequest.Get(url);
            var operation = req.SendWebRequest();

            while (!operation.isDone)
            {
                await UniTask.Yield();
            }
            return req.downloadHandler.text;
        }
    }
}