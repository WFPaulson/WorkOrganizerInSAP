namespace ContractWork.MVVM.ContractAndAssets;
public partial class ContractAndAssetsModel : ObservableObject {

    [ObservableProperty]
    private string _contractNumber;

    [ObservableProperty]
    private int _numOfAssets;

    [ObservableProperty]
    private DateTime _startDate;

    [ObservableProperty]
    private DateTime _endDate;

    [ObservableProperty]
    private string _status;

    [ObservableProperty]
    private string _accountNumber;

    [ObservableProperty]
    private string _accountName;

    [ObservableProperty]
    private string _accountType;

    [ObservableProperty]
    private string _renewed;

    [ObservableProperty]
    private string _billingCycle;

    [ObservableProperty]
    private string _billingMonth;

    [ObservableProperty]
    private decimal? _totalContractamount;

    [ObservableProperty]
    private string _serviceTech = "Bill Paulson";

    [ObservableProperty]
    private string _salesRep;

    [ObservableProperty]
    private string _equipID;

    [ObservableProperty]
    private string _equipPartNumber;

    [ObservableProperty]
    private string _model;

    [ObservableProperty]
    private string _serialNumber;

    [ObservableProperty]
    private int _equipAge;

    [ObservableProperty]
    private DateTime _soldDate;

    public ContractAndAssetsModel()
    {
            
    }

    public DataTable CombineSpreadsheets(DataTable SpreadsheetOne, DataTable SpreadSheetTwo) {
        DataTable CombinedSpreadsheet = new();
        foreach (DataRow row in SpreadsheetOne.Rows)
        {
            
        }




        foreach (DataRow row in SpreadSheetTwo.Rows) {


        }
        return CombinedSpreadsheet;
    }

}
