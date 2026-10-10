using System;
using System.Collections.Generic;
using Common.Storage;
using Csv;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.WSA;

namespace DataImporter
{
    /// <summary>
    /// ファイルデータをゲーム内で使えるように読み込む
    /// </summary>
    public class GameDataDeserializer : MonoBehaviour
    {
        [SerializeField] private FolderData _folderData;

        /// <summary>
        /// 読み込み
        /// </summary>
        public void Deserialize()
        {
            //Scenarioを読み込み
            DeserializeCSV<ScenarioData>(_folderData.ScenarioFolder, _folderData.ScenarioVersionsFileName,
                GameDataContainer.SetScenario);

            //画像読み込み
            DeserializeCSV<ImageIDData>(_folderData.AssetFolder, _folderData.AssetVersionsFileName,
             (_, images) => LoadSprite(images));

            //イベント読み込み
            foreach (var kv in GetSheetList(_folderData.EventFolder, _folderData.EventVersionsFileName))
            {
                if (string.IsNullOrEmpty(kv.Key))
                    continue;

                switch(kv.Key[0])
                {
                    //分岐データ
                    case '0':
                        foreach (var branchData in CsvSerializer.Deserialize<BranchData>(kv.Value))
                            GameDataContainer.SetBranch(branchData.BranchId, branchData);
                        break;
                }
            }
        }

       /// <summary>
       /// セクション別にCSVをすべて読み込む
       /// </summary>
        private void DeserializeCSV<T>(string folder, string versionsFile, Action<string, T[]> containerAction)
        {
            foreach (var kv in GetSheetList(folder, versionsFile))
            {
                var datas = CsvSerializer.Deserialize<T>(kv.Value);
                containerAction.Invoke(kv.Key, datas);
            }
        }

        private Dictionary<string, string> GetSheetList(string folder, string versionsFile)
        {
            Dictionary<string, string> result = new();

            var versionData = FileStorage.LoadFile(_folderData.CashFolder, versionsFile);
            var versionCSV = CsvSerializer.Deserialize<VersionFileData>(versionData);

            foreach (var version in versionCSV)
            {
                var directory = FileStorage.GetDirectory(_folderData.CashFolder, folder);
                var csv = FileStorage.LoadFile(directory, version.SheetName + ".csv");

                if (string.IsNullOrEmpty(csv))
                {
                    Debug.LogError(
                        $"[GameDataDeserializer] CSV file could not be loaded. " +
                        $"Directory: {directory}, File: {version.SheetName + ".csv"}");

                    continue;
                }
                result.Add(version.SheetName, csv);
            }

            return result;
        }

        /// <summary>
        /// CSVデータからSpriteを読み込む
        /// </summary>
        private ImageIDData[] LoadSprite(ImageIDData[] data)
        {
            var directory = FileStorage.GetDirectory(_folderData.CashFolder, _folderData.ImageFolder);
            foreach (var image in data)
            {
                var bytes = FileStorage.LoadBytes(directory, image.ImageID + ".png");
                if (bytes == null)
                {
                    Debug.LogWarning($"画像の読み込みに失敗しました。ID:{image.ImageID} Name:{image.ImageName}");
                    continue;
                }
                var sprite = ToSprite(bytes);
                GameDataContainer.SetImage(image.ImageID, sprite);
            }
            return data;
        }

        /// <summary>
        /// byteから画像に変換する
        /// </summary>
        public static Sprite ToSprite(byte[] bytes)
        {
            var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);

            if (!texture.LoadImage(bytes))
            {
                Destroy(texture);
                Debug.LogError("[SpriteUtility] Failed to load image from byte array.");
                return null;
            }

            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}