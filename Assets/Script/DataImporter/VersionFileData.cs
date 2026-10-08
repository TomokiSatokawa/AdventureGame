using Csv.Annotations;

namespace DataImporter
{
    [CsvObject]
    public partial class VersionFileData
    {
        [Column(0)]
        private string _sheetName;
        [Column(1)]
        private string _version;

        [IgnoreMember]
        public string SheetName => _sheetName;
        [IgnoreMember]
        public string Version => _version;
    }
}