using Csv.Annotations;

[CsvObject]
public partial class BranchData
{
    [Column(0)]
    private string _branchId;
    [Column(1)]
    private string _branchName;
    [Column(2)]
    private string _optionName1;
    [Column(3)]
    private string _optionDestination1;
    [Column(4)]
    private string _optionName2;
    [Column(5)]
    private string _optionDestination2;
    [Column(6)]
    private string _optionName3;
    [Column(7)]
    private string _optionDestination3;

    [IgnoreMember]
    public string BranchId => _branchId;
    [IgnoreMember]
    public string BranchName => _branchName;
    [IgnoreMember]
    public string OptionName1 => _optionName1;
    [IgnoreMember]
    public string OptionDestination1 => _optionDestination1;
    [IgnoreMember]
    public string OptionName2 => _optionName2;
    [IgnoreMember]
    public string OptionDestination2 => _optionDestination2;
    [IgnoreMember]
    public string OptionName3 => _optionName3;
    [IgnoreMember]
    public string OptionDestination3 => _optionDestination3;
}
