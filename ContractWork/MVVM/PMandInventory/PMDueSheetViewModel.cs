using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;

namespace ContractWork.MVVM.PMandInventory; 
public partial class PMDueSheetViewModel : ObservableObject {

    #region Const statements
    public const string CurrentYear = "#9BC2E6";
    public const string LastYear = "#FFFF99";
    public const string NextYear = "#CC99FF";
    #endregion

    #region Declaration statements
    public AccessService accessDB { get; set; }
   


    #endregion

    #region Static Statements
    public static int JanTotalCount { get; set; } = 0;
    public static int? JanTotalDone { get; set; } = 0;
    public static int FebTotalCount { get; set; } = 0;
    public static int? FebTotalDone { get; set; } = 0;
    public static int MarTotalCount { get; set; } = 0;
    public static int? MarTotalDone{ get; set; } = 0;
    public static int AprTotalCount { get; set; } = 0;
    public static int? AprTotalDone  { get; set; } = 0;
    public static int MayTotalCount { get; set; } = 0;
    public static int? MayTotalDone { get; set; } = 0;
    public static int JunTotalCount { get; set; } = 0;
    public static int? JunTotalDone { get; set; } = 0;
    public static int JulTotalCount { get; set; } = 0;
    public static int? JulTotalDone { get; set; } = 0;
    public static int AugTotalCount { get; set; } = 0;
    public static int? AugTotalDone { get; set; } = 0;
    public static int SepTotalCount { get; set; } = 0;
    public static int? SepTotalDone { get; set; } = 0;
    public static int OctTotalCount { get; set; } = 0;
    public static int? OctTotalDone { get; set; } = 0;
    public static int NovTotalCount { get; set; } = 0;
    public static int? NovTotalDone { get; set; } = 0;
    public static int DecTotalCount { get; set; } = 0;
    public static int? DecTotalDone { get; set; } = 0;


    #endregion

    #region Observable Collection statements

    [ObservableProperty]
    private ObservableCollection<PMDueList> _listOfPMsDue;

    [ObservableProperty]
    private List<string> _monthList = new List<string> {
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec"
    };

    [ObservableProperty]
    private ObservableCollection<string> _selectYearList;
    

    //[ObservableProperty]
    //private CustomerAccountModel _selectedCustomer;
    [ObservableProperty]
    private PMDueList _selectedCustomer;

    [ObservableProperty]
    private DataTable _pMScheduleDueDataTable;

    [ObservableProperty]
    private DataRowView _pMCustomer;

    [ObservableProperty]
    private DataRowView _selectedCustomerDataRow;


    [ObservableProperty]
    private string _endMonth = "Dec";

    [ObservableProperty]
    private int _startMonth = 0;

    #endregion

    #region Observable Property statements

    [ObservableProperty]
    private string _selectedYear;

    [ObservableProperty]
    private string _selectYearBackground = CurrentYear;

    #endregion

    public NavigationService _navigationService { get; set; }
    public PMDueSheetViewModel(NavigationService navigationService) {
        _navigationService = navigationService;
        //SelectedCustomer = new CustomerAccountModel();
        SelectedCustomer = new();
        ListOfPMsDue = new ObservableCollection<PMDueList>();

        SelectYearList = new ObservableCollection<string> {
                "Current Year",
                "Last Year",
                "Next Year"
            };

        ZeroOutCounts();

        GetDataForSpreadsheet();

    }

    private void ZeroOutCounts() {
        JanTotalCount = 0;
        JanTotalDone = 0;
        FebTotalCount = 0;
        FebTotalDone = 0;
        MarTotalCount = 0;
        MarTotalDone = 0;
        AprTotalCount = 0;
        AprTotalDone = 0;
        MayTotalCount = 0;
        MayTotalDone = 0;
        JunTotalCount = 0;
        JunTotalDone = 0;
        JulTotalCount = 0;
        JulTotalDone = 0;
        AugTotalCount = 0;
        AugTotalDone = 0;
        SepTotalCount = 0;
        SepTotalDone = 0;
        OctTotalCount = 0;
        OctTotalDone = 0;
        NovTotalCount = 0;
        NovTotalDone = 0;
        DecTotalCount = 0;
        DecTotalDone = 0;
    }

    [RelayCommand]
    private void MonthChanged() {
        switch (StartMonth) {
        case 0:
            EndMonth = "Dec";
            break;
        default:
            EndMonth = MonthList[StartMonth-1];
            break;
        }
    }

    [RelayCommand]
    private void YearSelected() {
        switch (SelectedYear) {
            case "Current Year":
                SelectYearBackground = CurrentYear;
                break;
            case "Last Year":
                SelectYearBackground = LastYear;
                break;
            case "Next Year":
                SelectYearBackground = NextYear;
                break;
            default:
                break;
        }
    }

    [RelayCommand]
    private void GetLeftButtonInfo() {

        int unavailables = SelectedCustomer.UADevice;

        MessageBox.Show($"Customer had {unavailables} unavailable devices during last set of PMs");
    }

    private void GetDataForSpreadsheet() { 
        accessDB = new ();

        string sqlServicePlanData =
        "SELECT tblServicePlan.ServicePlanNumber, tblServicePlan.AccountName_cbo, tblServicePlan.ServicePlanStartDate, " +
        "tblServicePlan.ServicePlanExpireDate, tblServicePlan.ServicePlanStatus, tblServicePlan.Archive, " +
        "tblEquipment.CustomerAccountID_FK, tblEquipment.ContractDescription, tblEquipment.PMMonth, tblEquipment.PMCompletedCheck, " +
        "tblEquipment.DeviceUnavailable, " +
        "ABS(Sum(tblEquipment.PMCompletedCheck)) AS SumOfPMCompletedCheck, ABS(Sum(tblEquipment.DeviceUnavailable)) AS SumOfUADevice, " +
        "Count(tblServicePlan.AccountName_cbo) AS DeviceCount " +
        "FROM tblServicePlan INNER JOIN tblEquipment ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +
        "GROUP BY tblServicePlan.ServicePlanNumber, tblServicePlan.AccountName_cbo, tblServicePlan.ServicePlanStartDate, " +
        "tblServicePlan.ServicePlanExpireDate, tblServicePlan.ServicePlanStatus, tblServicePlan.Archive, " +
        "tblEquipment.CustomerAccountID_FK, tblEquipment.ContractDescription, tblEquipment.PMMonth, tblEquipment.PMCompletedCheck, " +
        "tblEquipment.DeviceUnavailable " +
        "HAVING(((tblServicePlan.Archive) <> True) AND ((tblServicePlan.ServicePlanNumber) <> 'xxxxxxx')) " +
        "ORDER BY tblServicePlan.AccountName_cbo, tblServicePlan.ServicePlanStatus, tblServicePlan.ServicePlanExpireDate, " +
        "tblServicePlan.ServicePlanNumber";

        PMScheduleDueDataTable = accessDB.FetchDBRecordRequest(fullstatement: sqlServicePlanData);

        foreach (DataRow row in PMScheduleDueDataTable.Rows) {
            ListOfPMsDue.Add(new PMDueList(row));

        }
    }

    private string DateToString(DateTime dateformat) {
        if (dateformat != DateTime.MinValue) {
            return dateformat.ToShortDateString();
        }
        else return "";
    }

    [RelayCommand]
    private void ShowCustomerData() {
        CustomerAccountModel CustomerSelected = new();
        CustomerSelected.CustomerAccountID = SelectedCustomer.CustomerID;

        CustomerAccountDetailsWindow CustomerAccountDetails = new CustomerAccountDetailsWindow() {
            DataContext = _navigationService.CurrentViewModel =
                    new CustomerAccountDetailsViewModel(_navigationService, CustomerSelected)
        };
        CustomerAccountDetails.Show();
    }
}

public partial class PMDueList : ObservableObject {

    #region const and year declaration
    public const string noColor = "#FFFFFF";
    public const string expired = "#FF0000";
    public const string active = "#00FF00";
    public const string annualRenewed = "#FFFF00";

    private int thisYear = DateTime.Now.Year;
    private int lastYear = (DateTime.Now.AddYears(-1)).Year;
    private int nextYear = (DateTime.Now.AddYears(1)).Year;
    private int thisMonth = DateTime.Now.Month;

    #endregion

    #region Months Due and Done

    public int? JanDue { get; set; } = null;
    public int? JanDone { get; set; } = null;
    public string JanColor { get; set; } = noColor;

    public int? FebDue { get; set; } = null;
    public int? FebDone { get; set; } = null;
    public string FebColor { get; set; } = noColor;

    public int? MarDue { get; set; } = null;
    public int? MarDone { get; set; } = null;
    public string MarColor { get; set; } = noColor;

    public int? AprDue { get; set; } = null;
    public int? AprDone { get; set; } = null;
    public string AprColor { get; set; } = noColor;

    public int? MayDue { get; set; } = null;
    public int? MayDone { get; set; } = null;
    public string MayColor { get; set; } = noColor;

    public int? JunDue { get; set; } = null;
    public int? JunDone { get; set; } = null;
    public string JunColor { get; set; } = noColor;


    public int? JulDue { get; set; } = null;
    public int? JulDone { get; set; } = null;
    public string JulColor { get; set; } = noColor;


    public int? AugDue { get; set; } = null;
    public int? AugDone { get; set; } = null;
    public string AugColor { get; set; } = noColor;

    public int? SepDue { get; set; } = null;
    public int? SepDone { get; set; } = null;
    public string SepColor { get; set; } = noColor;

    public int? OctDue { get; set; } = null;
    public int? OctDone { get; set; } = null;
    public string OctColor { get; set; } = noColor;

    public int? NovDue { get; set; } = null;
    public int? NovDone { get; set; } = null;
    public string NovColor { get; set; } = noColor;


    public int? DecDue { get; set; } = null;
    public int? DecDone { get; set; } = null;
    public string DecColor { get; set; } = noColor;

    #endregion

    #region Declaration statements
    public AccessService accessDB { get; set; }
    public int? PMsDone { get; set; } = 0;

    private int year;
    private int month;

    #endregion

    #region Observable Property Statements
    [ObservableProperty]
    private int _customerID;

    [ObservableProperty]
    private string _accountName;

    [ObservableProperty]
    private string _servicePlanNumber;

    [ObservableProperty]
    private DateTime _startDate;

    [ObservableProperty]
    private DateTime _expireDate;

    [ObservableProperty]
    private string _planStatus;

    [ObservableProperty]
    private string _contractDescription;

    [ObservableProperty]
    private string _pMMonth;

    [ObservableProperty]
    private int _deviceCount;

    [ObservableProperty]
    private int _uADevice;

    #endregion

    #region CTOR
    public PMDueList() {
        
    }

    public PMDueList(DataRow row) {
        ScrubData(row);
    }

    #endregion

    private void ScrubData(DataRow row) {
        CustomerID = (int)row["CustomerAccountID_FK"];
        AccountName = row["AccountName_cbo"].ToString();
        ServicePlanNumber = row["ServicePlanNumber"].ToString();
        ContractDescription = row["ContractDescription"].ToString();

        StartDate = (DateTime)row["ServicePlanStartDate"];
        ExpireDate = (DateTime)row["ServicePlanExpireDate"];
        PlanStatus = row["ServicePlanStatus"].ToString();

        ContractStatusColor();

        DeviceCount = (int)row["DeviceCount"];
        PMMonth = row["PMMonth"].ToString();
        double dblPMsDone = (double)row["SumOfPMCompletedCheck"];
        PMsDone = Convert.ToInt32(dblPMsDone);
        double dblUADevice = (double)row["SumOfUADevice"];
        UADevice = Convert.ToInt32(dblUADevice);

        GetPMDueDeviceCount();
    }

    private void GetPMDoneCount() {
        
    }

    private void ContractStatusColor() {
        if (ExpireDate.Year == thisYear) {
            month = ExpireDate.Month;
            SetMonthColors(month, expired);
        }

        else if (StartDate.Year == thisYear) {
            month = StartDate.Month;
            SetMonthColors(month, active);
        }

        else if (thisYear > StartDate.Year && thisYear < ExpireDate.Year && PlanStatus == "Active") {
            month = StartDate.Month;
            SetMonthColors(month, annualRenewed);
        }
    }

    private void SetMonthColors(int MonthNumber, string MonthColor) {
        switch (MonthNumber) {
            case 1:
                JanColor = MonthColor;
                break;
            case 2:
                FebColor = MonthColor;
                break;
            case 3:
                MarColor = MonthColor;
                break;
            case 4:
                AprColor = MonthColor;
                break;
            case 5:
                MayColor = MonthColor;
                break;
            case 6:
                JunColor = MonthColor;
                break;
            case 7:
                JulColor = MonthColor;
                break;
            case 8:
                AugColor = MonthColor;
                break;
            case 9:
                SepColor = MonthColor;
                break;
            case 10:
                OctColor = MonthColor;
                break;
            case 11:
                NovColor = MonthColor;
                break;
            case 12:
                DecColor = MonthColor;
                break;
        }
    }

    private void GetPMDueDeviceCount() {
        switch (PMMonth) {
            case "Jan":
                JanDue = DeviceCount;
                JanDone = PMsDone + UADevice;
                PMDueSheetViewModel.JanTotalCount += DeviceCount;
                PMDueSheetViewModel.JanTotalDone += JanDone;
                break;
            case "Feb":
                FebDue = DeviceCount;
                FebDone = PMsDone + UADevice;
                PMDueSheetViewModel.FebTotalCount += DeviceCount;
                PMDueSheetViewModel.FebTotalDone += FebDone;          //PMsDone;
                break;
            case "Mar":
                MarDue = DeviceCount;
                MarDone = PMsDone + UADevice;
                PMDueSheetViewModel.MarTotalCount += DeviceCount;
                PMDueSheetViewModel.MarTotalDone += MarDone;
                break;
            case "Apr":
                AprDue = DeviceCount;
                AprDone = PMsDone + UADevice;
                PMDueSheetViewModel.AprTotalCount += DeviceCount;
                PMDueSheetViewModel.AprTotalDone += AprDone;
                break;
            case "May":
                MayDue = DeviceCount;
                MayDone = PMsDone + UADevice;
                PMDueSheetViewModel.MayTotalCount += DeviceCount;
                PMDueSheetViewModel.MayTotalDone += MayDone;
                break;
            case "Jun":
                JunDue = DeviceCount;
                JunDone = PMsDone + UADevice;
                PMDueSheetViewModel.JunTotalCount += DeviceCount;
                PMDueSheetViewModel.JunTotalDone += JunDone;
                break;
            case "Jul":
                JulDue = DeviceCount;
                JulDone = PMsDone + UADevice;
                PMDueSheetViewModel.JulTotalCount += DeviceCount;
                PMDueSheetViewModel.JulTotalDone += JulDone;
                break;
            case "Aug":
                AugDue = DeviceCount;
                AugDone = PMsDone + UADevice;
                PMDueSheetViewModel.AugTotalCount += DeviceCount;
                PMDueSheetViewModel.AugTotalDone += AugDone;
                break;
            case "Sep":
                SepDue = DeviceCount;
                SepDone = PMsDone + UADevice;
                PMDueSheetViewModel.SepTotalCount += DeviceCount;
                PMDueSheetViewModel.SepTotalDone += SepDone;
                break;
            case "Oct":
                OctDue = DeviceCount;
                OctDone = PMsDone + UADevice;
                PMDueSheetViewModel.OctTotalCount += DeviceCount;
                PMDueSheetViewModel.OctTotalDone += OctDone;
                break;
            case "Nov":
                NovDue = DeviceCount;
                NovDone = PMsDone + UADevice;
                PMDueSheetViewModel.NovTotalCount += DeviceCount;
                PMDueSheetViewModel.NovTotalDone += NovDone;
                break;
            case "Dec":
                DecDue = DeviceCount;
                DecDone = PMsDone + UADevice;
                PMDueSheetViewModel.DecTotalCount += DeviceCount;
                PMDueSheetViewModel.DecTotalDone += DecDone;
                break;
        }
    }
}



