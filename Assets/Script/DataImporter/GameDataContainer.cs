using System.Collections.Generic;
using UnityEngine;
using static DataImporter.GameDataContainer;

namespace DataImporter
{
    /// <summary>
    /// ゲームのデータ保管
    /// </summary>
    public static class GameDataContainer
    {
        private static readonly CSVData<VersionFileData> _version = new();
        private static readonly Dictionary<string,CSVData<ScenarioData>> _scenarios = new();
        private static readonly Dictionary<string,Sprite> _images = new();

        public static IReadonlyCSVData<VersionFileData> Version => _version;


        public static void SetVersion(VersionFileData[] data) => _version.SetData(data);

        public static void SetScenario(string name, ScenarioData[] data)
        {
            if (!_scenarios.TryGetValue(name, out var scenario))
            {
                scenario = new CSVData<ScenarioData>();
                _scenarios.Add(name, scenario);
            }

            scenario.SetData(data);
        }


        public static void SetImage(string id, Sprite data)
        {
            _images[id] = data;
        }

        public static IReadOnlyList<ScenarioData> GetScenario(string name)
        {
            if(!_scenarios.TryGetValue(name, out var csvData))
            {
                Debug.LogError("Scenario見つからない");
                return null;
            }
            return csvData.Data;
        }

        public static Sprite GetImage(string id)
        {
            if (!_images.TryGetValue(id, out var image))
            {
                Debug.LogError("image見つからない");
                return null;
            }
            return image;
        }


        public class CSVData<T> : IReadonlyCSVData<T>
        {
            private T[] datas;

            public IReadOnlyList<T> Data => datas;

            public void SetData(T[] data)
            {
                datas = data;
            }
        }

        public interface IReadonlyCSVData<T>
        {
            public IReadOnlyList<T> Data { get; }
        }
    }
}
