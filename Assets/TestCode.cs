using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Csv;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestCode : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    public const string url = "https://docs.google.com/spreadsheets/d/1mdPMF3fQJnCtjBkTx9-Wup8QgluZ-POcJxgxLh0X9Gw/gviz/tq?tqx=out:csv&sheet='ƒeƒXƒg'";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        var req = UnityWebRequest.Get(url);
        var operation = req.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        var csv = req.downloadHandler.text;
        var data = CsvSerializer.Deserialize<Scenario>(csv);

        _image.texture = await LoadAsync(data[0].ImageURL);
        _image.SetNativeSize();
    }

    public static async UniTask<Texture2D> LoadAsync(string driveUrl)
    {
        var fileId = ExtractFileId(driveUrl);

        if (string.IsNullOrEmpty(fileId))
        {
            Debug.LogError($"Google Drive URL‚©‚çFile ID‚ðŽæ“¾‚Å‚«‚Ü‚¹‚ñ: {driveUrl}");   
            return null;
        }

        var url = $"https://drive.google.com/uc?export=download&id={fileId}";

        using var request = UnityWebRequestTexture.GetTexture(url);

        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"‰æ‘œ‚ÌŽæ“¾‚ÉŽ¸”s‚µ‚Ü‚µ‚½: {request.error}");
            return null;
        }

        return DownloadHandlerTexture.GetContent(request);
    }

    private static string ExtractFileId(string url)
    {
        var match = Regex.Match(
            url,
            @"drive\.google\.com/file/d/([^/]+)"
        );

        return match.Success ? match.Groups[1].Value : null;
    }
}
