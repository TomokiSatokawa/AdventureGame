using Common.Storage;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace DataImporter
{
    /// <summary>
    /// 画像アセットインポート
    /// </summary>
    public class ImageImporter : MonoBehaviour
    {
        [SerializeField] private FolderData _folderData;

        public async UniTask DownloadImages(IReadOnlyList<ImageIDData> imageDatas)
        {
            foreach (var imageData in imageDatas)
            {
                Debug.Log($"画像　{imageData.ImageName} ダウンロード中");
                var bytes = await DownloadFile(imageData.ImageURL);
                if (bytes == null) continue;

                var directory = FileStorage.GetDirectory(_folderData.CashFolder, _folderData.ImageFolder);
                FileStorage.SaveOrCreateBytes(directory, imageData.ImageID + ".png", bytes);
            }
        }

        /// <summary>
        /// Google Driveの共有URLからファイルIDを取得する。
        /// </summary>
        private static string GetFileID(string shareURL)
        {
            if (string.IsNullOrEmpty(shareURL))
            {
                Debug.LogError("[GoogleDriveUtility] Share URL is null or empty.");
                return null;
            }

            const string filePath = "/file/d/";

            int startIndex = shareURL.IndexOf(filePath, StringComparison.Ordinal);

            if (startIndex < 0)
            {
                Debug.LogError(
                    $"[GoogleDriveUtility] Invalid Google Drive URL: {shareURL}");
                return null;
            }

            startIndex += filePath.Length;

            int endIndex = shareURL.IndexOf(
                '/',
                startIndex);

            if (endIndex < 0)
            {
                endIndex = shareURL.Length;
            }

            return shareURL.Substring(
                startIndex,
                endIndex - startIndex);
        }


        /// <summary>
        /// Google DriveのファイルIDからダウンロードURLを生成する。
        /// </summary>
        private string CreateDownloadURL(string fileID)
        {
            if (string.IsNullOrEmpty(fileID))
            {
                Debug.LogError("File ID is null or empty.");
                return null;
            }

            return $"https://drive.google.com/uc?export=download&id={fileID}";
        }


        /// <summary>
        /// Google Driveの共有URLからファイルをダウンロードする。
        /// </summary>
        private async UniTask<byte[]> DownloadFile(string shareURL)
        {
            string fileID = GetFileID(shareURL);

            if (string.IsNullOrEmpty(fileID))
            {
                return null;
            }

            string downloadURL = CreateDownloadURL(fileID);

            if (string.IsNullOrEmpty(downloadURL))
            {
                return null;
            }

            using var request = UnityWebRequest.Get(downloadURL);

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(
                    $"[GoogleDriveUtility] Failed to download file.\n" +
                    $"URL: {downloadURL}\n" +
                    $"Error: {request.error}");

                return null;
            }

            return request.downloadHandler.data;
        }
    }
}