using UnityEngine;

namespace DataImporter
{
    [CreateAssetMenu(fileName = "SpreadsheetURL", menuName = "ScriptableObject/SpreadsheetURL")]
    public class SpreadsheetURL : ScriptableObject
    {
        [SerializeField] private string _spreadsheetID;
        [SerializeField] private string _sheetName;

        public string SpreadSheetID => _spreadsheetID;
        public string SheetName => _sheetName;

        public string GetURL()
        {
            return CreateURL(_spreadsheetID, _sheetName);  
        }

        public static string  CreateURL(string id,string sheet)
        {
            return $"https://docs.google.com/spreadsheets/d/{id}/gviz/tq?tqx=out:csv&sheet={sheet}";
        }
    }
}