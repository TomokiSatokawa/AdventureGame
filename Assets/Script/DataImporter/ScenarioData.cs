using Csv.Annotations;

namespace DataImporter
{
    /// <summary>
    /// シナリオデータを管理する
    /// </summary>
    [CsvObject]
    public partial class ScenarioData
    {
        [Column(0)]
        private string _personName;

        [Column(1)]
        private string _message;

        [Column(2)]
        private string _backgroundID;

        [Column(3)]
        private string _backgroundChangeType;

        [Column(4)]
        private string _myCharacterID;

        [Column(5)]
        private string _myCharacterChangeType;

        [Column(6)]
        private string _characterID1;

        [Column(7)]
        private string _characterPosition1;

        [Column(8)]
        private string _characterChangeType1;

        [Column(9)]
        private string _characterID2;

        [Column(10)]
        private string _characterPosition2;

        [Column(11)]
        private string _characterChangeType2;

        [Column(12)]
        private string _characterID3;

        [Column(13)]
        private string _characterPosition3;

        [Column(14)]
        private string _characterChangeType3;

        [Column(15)]
        private string _eventType;

        [Column(16)]
        private string _eventID;

        [Column(17)]
        private string _eventData;

        [IgnoreMember]
        public string PersonName => _personName;

        [IgnoreMember]
        public string Message => _message;

        [IgnoreMember]
        public string BackgroundID => _backgroundID;

        [IgnoreMember]
        public string BackgroundChangeType => _backgroundChangeType;

        [IgnoreMember]
        public string MyCharacterID => _myCharacterID;

        [IgnoreMember]
        public string MyCharacterChangeType => _myCharacterChangeType;

        [IgnoreMember]
        public string CharacterID1 => _characterID1;

        [IgnoreMember]
        public string CharacterPosition1 => _characterPosition1;

        [IgnoreMember]
        public string CharacterChangeType1 => _characterChangeType1;

        [IgnoreMember]
        public string CharacterID2 => _characterID2;

        [IgnoreMember]
        public string CharacterPosition2 => _characterPosition2;

        [IgnoreMember]
        public string CharacterChangeType2 => _characterChangeType2;

        [IgnoreMember]
        public string CharacterID3 => _characterID3;

        [IgnoreMember]
        public string CharacterPosition3 => _characterPosition3;

        [IgnoreMember]
        public string CharacterChangeType3 => _characterChangeType3;

        [IgnoreMember]
        public string EventType => _eventType;

        [IgnoreMember]
        public string EventID => _eventID;

        [IgnoreMember]
        public string EventData => _eventData;
    }
}