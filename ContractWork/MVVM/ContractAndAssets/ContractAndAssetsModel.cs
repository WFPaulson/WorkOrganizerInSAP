namespace ContractWork.MVVM.ContractAndAssets; 
public partial class ContractAndAssetsModel : ObservableObject {

    #region Observable properties Excel ColumnNames
    [ObservableProperty]
    private string? _contract;

    [ObservableProperty]
    private string? _equipmentID;

    [ObservableProperty]
    private string? _line;

    [ObservableProperty]
    private string? _billTo;

    [ObservableProperty]
    private string? _billToName;

    [ObservableProperty]
    private string? _shipTo;

    [ObservableProperty]
    private string? _shipToName;

    [ObservableProperty]
    private string? _customerAccount;

    [ObservableProperty]
    private string? _customerName;

    [ObservableProperty]
    private string? _contractMaterial;

    [ObservableProperty]
    private string? _coveredSerial;

    [ObservableProperty]
    private string? _model;

    [ObservableProperty]
    private string? _age;

    [ObservableProperty]
    private DateTime _sold;

    [ObservableProperty]
    private DateTime _cvgStart;

    [ObservableProperty]
    private DateTime _cvgEnd;

    [ObservableProperty]
    private DateTime _eOL;

    [ObservableProperty]
    private string? _status;

    [ObservableProperty]
    private string? _term;

    [ObservableProperty]
    private decimal _netValue;

    [ObservableProperty]
    private string? _type;

    [ObservableProperty]
    private string? _renewed;

    [ObservableProperty]
    private string? _cycle;

    [ObservableProperty]
    private string? _month;

    [ObservableProperty]
    private bool _archived;
    
    [ObservableProperty]
    private string? _tech;

    [ObservableProperty]
    private string? _svcTerr;

    [ObservableProperty]
    private string? _salesRep;

    [ObservableProperty]
    private string? _salesTerr;

    [ObservableProperty]
    private string? _salesRegion;

    #endregion

    #region Observable properties
    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private GVis? _visCol;


    #endregion

    public ContractAndAssetsModel()
    {
            
    }

    public ContractAndAssetsModel(DataRow dtRow) {
        CustomerAccount = dtRow["Account"].ToString();
        CustomerName = dtRow["Customer"].ToString();
        EquipmentID = dtRow["Equip ID"].ToString();
        ContractMaterial = dtRow["Mat Number"].ToString();
        CoveredSerial = dtRow["Serial"].ToString();                      //dtRow["Serial"].ToString();
        Model = dtRow["Product"].ToString();

        if (dtRow["Installed"] != DBNull.Value) {
            Sold = (DateTime)dtRow["Installed"];
        }
        //Sold = (DateTime)dtRow["Installed"];

        if (dtRow["EOL"] != DBNull.Value) {
            EOL = (DateTime)dtRow["EOL"];
        }
        //EOL = (DateTime)dtRow["EOL"];

        if (dtRow["Cvg Start"] != DBNull.Value) {
            CvgStart = (DateTime)dtRow["Cvg Start"];
        }
        //CvgStart = (DateTime)dtRow["Cvg Start"];

        if (dtRow["Cvg End"] != DBNull.Value) {
            CvgEnd = (DateTime)dtRow["Cvg End"];
        }
        //CvgEnd = (DateTime)dtRow["Cvg End"];

        Status = dtRow["Status"].ToString();
        Tech = dtRow["Tech"].ToString();
        SalesRep = dtRow["Sales Rep"].ToString();

    }
}

//#todo fix


