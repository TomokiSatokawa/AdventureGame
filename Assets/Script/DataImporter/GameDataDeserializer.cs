using System;
using Common.Storage;
using Csv;
using UnityEngine;

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
        }

       /// <summary>
       /// セクション別にCSVをすべて読み込む
       /// </summary>
        private void DeserializeCSV<T>(string folder, string versionsFile, Action<string, T[]> containerAction)
        {
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
                var datas = CsvSerializer.Deserialize<T>(csv);
                containerAction.Invoke(version.SheetName, datas);
            }
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