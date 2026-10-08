using Csv.Annotations;

[CsvObject]
public partial class Scenario
{
    [Column(0)]
    private string _text;
    [Column(1)]
    private string _imageURL;

    [IgnoreMember]
    public string Text => _text;
    [IgnoreMember]
    public string ImageURL => _imageURL;
}
