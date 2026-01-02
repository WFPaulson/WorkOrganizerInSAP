using System.Collections.Immutable;
using System.Drawing;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ContractWork.MVVM.PMandInventory;
public partial class PMSchedulingViewModel : ObservableObject {

    #region Observable properties
    [ObservableProperty]
    private DataTable _pMScheduleList;

    [ObservableProperty]
    private ObservableCollection<CustomerAccountModel> _customerEquipmentList;

    [ObservableProperty]
    private DataRowView _selectedCustomer;

    [ObservableProperty]
    private CustomerAccountModel _customerAccount;

    [ObservableProperty]
    private DataTable _pMToDoList = new();

    [ObservableProperty]
    private DataRowView _pMToDoCustomer;

    //[ObservableProperty]
    //private string? _planStatus;

    [ObservableProperty]
    private string _togglePositionText = "Full";
    //partial void OnTogglePositionTextChanged(string value) {
    //    if (value != "Full") { this.TogglePositionText = "PM List"; }

    //}

    [ObservableProperty]
    private bool _fullorPMList;
    //partial void OnFullorPMListChanged(bool value) {
    //    MessageBox.Show(value.ToString());
    //}
    
    #endregion

    #region Property statements
    public bool chkJan { get; set; } = false;
    public bool chkFeb { get; set; } = false;
    public bool chkMar { get; set; } = false;
    public bool chkApr { get; set; } = false;
    public bool chkMay { get; set; } = false;
    public bool chkJun { get; set; } = false;
    public bool chkJul { get; set; } = false;
    public bool chkAug { get; set; } = false;
    public bool chkSep { get; set; } = false;
    public bool chkOct { get; set; } = false;
    public bool chkNov { get; set; } = false;
    public bool chkDec { get; set; } = false;

    #endregion

    # region Declaration statements
    //public GC PMMonthColumnWidths { get; set; }

    public AccessService accessDB { get; set; }

    #endregion

    public NavigationService _navigationService { get; set; }
    public PMSchedulingViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
        accessDB = new();
        //PMMonthColumnWidths = new();

         RunPMListSetup();
    }

    [RelayCommand]
    private void OpenExcelPMDueList() {
        PMDueSheetWindow PMDueSheet = new PMDueSheetWindow() {
            DataContext = _navigationService.CurrentViewModel =
                new PMDueSheetViewModel(_navigationService)
        };
        PMDueSheet.Show();
    }

    [RelayCommand]
    private void Full_PMList() {

        if (FullorPMList) {
            TogglePositionText = "PM";
            OpenPMToDoList(PMandInventorySQLStatements.ShowPMList()); }
        else {
            TogglePositionText = "Full";
            PMMonthSelection();
        }
    }

    [RelayCommand]
    private void AddToPMList() {
        accessDB = new();

        int CustomerID = (int)SelectedCustomer.Row.ItemArray[2];
        bool value = (bool)SelectedCustomer.Row.ItemArray[1];
        string sqlInsert =
            $"UPDATE [tblCustomerAccounts] " +
            $"SET [PMList] = {value} " +
            $"WHERE [CustomerAccountID] = {CustomerID}";

        accessDB.AddToAccount(SQLInsert: sqlInsert);

        //PMMonthSelection();
        Full_PMList();
    }

    [RelayCommand]
    private void OpenPMToDoList(string tmpSQL) {
        PMScheduleList = new DataTable();
        List<DateTime?> firstAndLast;

        PMScheduleList = accessDB.FetchDBRecordRequest(tmpSQL);
        //TODO: need PM Completed, Oldest Completed, and UA
        AddPMCompletedColumns();

        foreach (DataRow item in PMScheduleList.Rows) {
            firstAndLast = GetMostRecentAndOldestPMsCompleted((int)item["ID"], (int)item["Service Plan"], item["Model"].ToString());

            // Assign null if firstAndLast[0] is null, otherwise assign the value
            item["PM Completed"] = firstAndLast[0] == null ? null : firstAndLast[0];

            // If you want to assign "Oldest Completed" as well, you can do similarly:
            item["Oldest Completed"] = firstAndLast[1] == null ? null : firstAndLast[1];
        }
    }


    #region Methods
    private void RunPMListSetup() {
        PMScheduleList = new DataTable();
        List<DateTime?> firstAndLast;
        //List<string?> status;
        //bool UAPM;

        //TODO: need to add ua as being completed, but a hover states how many were UA, like on the excel spreadsheet

        PMScheduleList = accessDB.FetchDBRecordRequest(PMandInventorySQLStatements.GetFullPMList());            //GetFullPMList  GetAPMList

        int columnCount = PMScheduleList.Columns.Count;
        int index = 6;

        List<(string Name, int Number)> list = new List<(string, int)>
        {
            ("PM Completed", index),
            ("Oldest Completed", index + 1),
            ("UA", columnCount + 2)
        };

        AddPMCompletedColumns(list);

       int counter = 0;

        //TODO: need to add check if contract has epired also
        /// Gets rid of devices that were UA, need to add check for expired contract in this loop
        
        foreach (DataRow item in PMScheduleList.Rows) {
            (firstAndLast) = GetMostRecentAndOldestPMsCompleted((int)item["ID"], (int)item["Service Plan"], item["Model"].ToString());

            if (firstAndLast[0] != null) { item["PM Completed"] = firstAndLast[0]; }

            if (firstAndLast[1] != null) { item["Oldest Completed"] = firstAndLast[1]; }
        }
    }

    private void AddPMCompletedColumns(List<(string Name, int index)> items) {
        string columnName;
        int columnNumber;

        foreach (var item in items) {
           
            columnName = item.Name;
            columnNumber = item.index;

            DataColumn dcName = new DataColumn(columnName, typeof(DateTime));
            PMScheduleList.Columns.Add(dcName);
            dcName.SetOrdinal(columnNumber);

        }
    }

    private void AddPMCompletedColumns() {
        DataColumn dcPMcompleted = new DataColumn("PM Completed", typeof(string));
        DataColumn dcOldestCompleted = new DataColumn("Oldest Completed", typeof(string));

        int insertIndex = 6;

        PMScheduleList.Columns.Add(dcPMcompleted);
        PMScheduleList.Columns.Add(dcOldestCompleted);

        dcPMcompleted.SetOrdinal(insertIndex);
        dcOldestCompleted.SetOrdinal(insertIndex + 1);
    }

    private List<DateTime?> GetMostRecentAndOldestPMsCompleted(int customerID, int servicePlanID, string modelName) {     //DateTime? firstPM, DateTime? lastPM)
        //TODO: need to add check if contract has epired also

        int mdlID = modelName.ModelToID();
        //string x = string.Empty;
        //string test;
        //string strfirst, strlast;

        //DateTime? FirstPM, LastPM;
        List<DateTime?> firstAndLast = new();
        //List<string> status = new();

        string sqlMostRecentAndOldPMs =
            "SELECT [PMCompleted], [DeviceUnavailable] " +
            "FROM [tblEquipment] " +
            $"WHERE [CustomerAccountID_FK] = {customerID} " +
            $"AND [ModelID] = {mdlID} " +
            $"AND [ServicePlanID_FK] = {servicePlanID} " +
            "AND [tblEquipment.Archive] <> True " +
            "ORDER BY [PMCompleted] DESC";

        DataTable dte = accessDB.FetchDBRecordRequest(sqlMostRecentAndOldPMs);

        DateTime? fst = (dte.Rows.Count > 0 && !Convert.IsDBNull(dte.Rows[0]["PMCompleted"]))
           ? (DateTime?)dte.Rows[0]["PMCompleted"]
           : null;
        DateTime? lst = (dte.Rows.Count > 0 && !Convert.IsDBNull(dte.Rows[dte.Rows.Count - 1]["PMCompleted"])) 
            ? (DateTime?)dte.Rows[dte.Rows.Count - 1]["PMCompleted"] 
            : null;

        firstAndLast.Add(fst);
        firstAndLast.Add(lst);
        // Fix for CS0019 and CS0201: Remove invalid use of ?? with void-returning Add()
        // Instead, add fst and lst directly, handling nulls as needed.
        //if (!fst.HasValue || !lst.HasValue)
        //{
        // If fst is null, add null; otherwise, add fst
        //      firstAndLast.Add(fst.HasValue ? fst : null);

        // If lst is null, add null; otherwise, add lst
        //      firstAndLast.Add(lst.HasValue ? lst : null);

        // Additional logic for status can be added here if needed
        // status.Add(x); // Uncomment and implement if required
        //}
        //else
        //{
        //    firstAndLast.Add(fst);
        //    firstAndLast.Add(lst);
        //    status.Add(null);

        //    DateTime dateTimeObject = DateTime.Parse(fst);
        //    fst = dateTimeObject.ToString("MM/dd/yyyy");

        //    dateTimeObject = DateTime.Parse(lst);
        //    lst = dateTimeObject.ToString("MM/dd/yyyy");


        //}
        //}

        return (firstAndLast);
       
    }
    
    [RelayCommand]
    private void PMMonthSelection() {
        List<string> mnthList = new List<string>();

        if (chkJan) mnthList.Add("'Jan'");
        if (chkFeb) mnthList.Add("'Feb'");
        if (chkMar) mnthList.Add("'Mar'");
        if (chkApr) mnthList.Add("'Apr'");
        if (chkMay) mnthList.Add("'May'");
        if (chkJun) mnthList.Add("'Jun'");
        if (chkJul) mnthList.Add("'Jul'");
        if (chkAug) mnthList.Add("'Aug'");
        if (chkSep) mnthList.Add("'Sep'");
        if (chkOct) mnthList.Add("'Oct'");
        if (chkNov) mnthList.Add("'Nov'");
        if (chkDec) mnthList.Add("'Dec'");

        FullorPMList = false;
        TogglePositionText = "Full";

        RefreshPMMonth(mnthList);

    }

    private void RefreshPMMonth(List<string> pmMnthSQL) {
        PMScheduleList = new DataTable();

        string tmpSQL =
            "SELECT tblEquipment.CustomerAccountID_FK AS [ID], tblCustomerAccounts.AccountName AS [Account Name], " +
            "tblEquipmentModel_LU.ModelType AS [Model], Count(tblEquipment.EquipmentSerial) AS [Device Qty], " +
            "tblEquipment.PMMonth AS [PM Month], tblPMMonth_LU.MonthNumber AS [Month Number], " +
            "First(tblEquipment.PMCompleted) AS [PM Completed], Last(tblEquipment.PMCompleted) AS [Oldest Completed], " +
            "tblCustomerAccounts.PMList AS [Add To List] " +
            "FROM tblPMMonth_LU INNER JOIN(tblCustomerAccounts INNER JOIN (tblEquipmentModel_LU INNER JOIN tblEquipment " +
            "ON tblEquipmentModel_LU.ModelID = tblEquipment.ModelID) " +
            "ON tblCustomerAccounts.CustomerAccountID = tblEquipment.CustomerAccountID_FK) " +
            "ON tblPMMonth_LU.PMMonth = tblEquipment.PMMonth ";

        if (pmMnthSQL.Count > 0) {
            string strHolder = string.Empty;
            foreach (string item in pmMnthSQL) {
                strHolder += item + ", ";
            }
            strHolder = strHolder.Substring(0, strHolder.Length - 2);
            tmpSQL += $"WHERE tblEquipment.PMMonth In ({strHolder}) ";
        }

        tmpSQL +=
            "GROUP BY tblEquipment.CustomerAccountID_FK, tblCustomerAccounts.AccountName, tblEquipmentModel_LU.ModelType, " +
            "tblEquipment.PMMonth, tblPMMonth_LU.MonthNumber, tblCustomerAccounts.Archive, tblEquipment.Archive, " +
            "tblCustomerAccounts.PMList, tblEquipment.NoPMContract " +
            "HAVING(((tblCustomerAccounts.Archive) <> True) " +
            "AND((tblEquipment.Archive) <> True) " +
            "AND ((tblEquipment.NoPMContract) <> True)) " +
            "ORDER BY [tblPMMonth_LU.MonthNumber] DESC, [tblCustomerAccounts.AccountName] ASC ";

        PMScheduleList = accessDB.FetchDBRecordRequest(tmpSQL);
    }

    [RelayCommand]
    private void GetCustomerData() {
        //MessageBox.Show("test");
        try {
            GetCustomerAccountInfo();
            GoToCustomerAccount();
        }
        catch (Exception) {

        }
        //GetCustomerAccountInfo();
        //GoToCustomerAccount();
    }

    private void GetCustomerAccountInfo() { 
        CustomerAccount = new();
        ///CustomerEquipmentList = new();

        int CustomerID = (int)SelectedCustomer.Row.ItemArray[2];
        //string sqlJoinStatement = CustomerDetailsSQLStatements.CustomerAccountEquipmentList(CustomerID);
        //DataTable equipmentList = accessDB.FetchDBRecordRequest(sqlJoinStatement);

        //List<string> cboContractList = new();

        string sqlJoinStatement = PMandInventorySQLStatements.GetCustomerAccountData(CustomerID);
        DataTable dbTable = accessDB.FetchDBRecordRequest(sqlJoinStatement);
        CustomerAccount.tblCustomerAccountData(dbTable.Rows[0]);

    }

    private void GoToCustomerAccount() {
        CustomerAccountDetailsWindow PMCustomer = new () {
            DataContext = _navigationService.CurrentViewModel =
                new CustomerAccountDetailsViewModel(_navigationService, CustomerAccount)
        };
        PMCustomer.Show();

    }

    #endregion
}
