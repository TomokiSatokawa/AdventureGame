using Csv.Annotations;
using UnityEngine;

namespace DataImporter
{
    [CsvObject]
    public partial class ImageIDData
    {
        [Column(0)]
        private string _imageID;

        [Column(1)]
        private string _imageName;

        [Column(2)]
        private string _imageURL;


        [IgnoreMember]
        public string ImageID => _imageID;

        [IgnoreMember]
        public string ImageName => _imageName;

        [IgnoreMember]
        public string ImageURL => _imageURL;

        [IgnoreMember]
        public Sprite Sprite { get; private set; }


        public void SetSprite(Sprite sprite)
        {
            Sprite = sprite;
        }
    }
}