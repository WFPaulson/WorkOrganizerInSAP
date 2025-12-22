using System.Collections.Immutable;
using System.Drawing;
using System.Text;
using Microsoft.IdentityModel.Tokens;

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
    public GC PMMonthColumnWidths { get; set; }

    public AccessService accessDB { get; set; }

    #endregion

    public NavigationService _navigationService { get; set; }
    public PMSchedulingViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
        accessDB = new();
        PMMonthColumnWidths = new();

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
        string FirstPM, LastPM;
        PMScheduleList = new DataTable();

        PMScheduleList = accessDB.FetchDBRecordRequest(tmpSQL);
        //TODO: need PM Completed, Oldest Completed, and UA
        AddPMCompletedColumns();

        foreach (DataRow item in PMScheduleList.Rows) {
            (FirstPM, LastPM) = GetMostRecentAndOldestPMsCompleted((int)item["ID"], (int)item["Service Plan"], item["Model"].ToString());

            item["Oldest Completed"] = LastPM == null ? DBNull.Value : LastPM;
            item["PM Completed"] = FirstPM == null ? DBNull.Value : FirstPM;
        }



    }


    #region Methods
    private void RunPMListSetup() {
        PMScheduleList = new DataTable();
        string FirstPM, LastPM, UAPM;
        

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

       

        //TODO: need to add check if contract has epired also

        /// Gets rid of devices that were UA, need to add check for expired contract in this loop
        foreach (DataRow item in PMScheduleList.Rows) {
            (FirstPM, LastPM) = GetMostRecentAndOldestPMsCompleted((int)item["ID"], (int)item["Service Plan"], item["Model"].ToString());

            //LastPM == null ? DBNull.Value;



            item["Oldest Completed"] = LastPM == null ? DBNull.Value : LastPM;
            item["PM Completed"] = FirstPM == null ? DBNull.Value : FirstPM;
            //item["UA"] = UAPM == null ? DBNull.Value : FirstPM;
        }

        //PMMonthColumnWidths.GetMaxColumnWidths(PMScheduleList);

    }

    private void AddPMCompletedColumns(List<(string Name, int index)> items) {
        string columnName;
        int columnNumber;

        foreach (var item in items) {
           
            columnName = item.Name;
            columnNumber = item.index;

            DataColumn dcName = new DataColumn(columnName, typeof(string));
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

    private (String? firstPM, String? lastPM) GetMostRecentAndOldestPMsCompleted(int customerID, int servicePlanID, string modelName) {
        //TODO: need to add check if contract has epired also

        int mdlID = modelName.ModelToID();
        string x = string.Empty;
        string test;

        string sqlMostRecentAndOldPMs =
            "SELECT [PMCompleted], [DeviceUnavailable] " +
            "FROM [tblEquipment] " +
            $"WHERE [CustomerAccountID_FK] = {customerID} " +
            $"AND [ModelID] = {mdlID} " +
            $"AND [ServicePlanID_FK] = {servicePlanID} " +
            "AND [tblEquipment.Archive] <> True " +
            "ORDER BY [PMCompleted] DESC";
        //"AND [ServicePlanStatusLU_cbo] <> 'Expired'" +
        // "AND [DeviceUnavailable] <> TRUE " +

        DataTable dte = accessDB.FetchDBRecordRequest(sqlMostRecentAndOldPMs);

        //if (customerID == 25){            //dte.Rows.Count == 1){
        //    if (dte.Rows[0]["DeviceUnavailable"] == "True") {
        //        test = "stupid";
        //    }
        //}

       

        String? lst = (Convert.IsDBNull(dte.Rows[dte.Rows.Count - 1]["PMCompleted"]) ? null : ( dte.Rows[dte.Rows.Count - 1]["PMCompleted"].ToString()));
        String? fst = (Convert.IsDBNull(dte.Rows[0]["PMCompleted"]) ? null : dte.Rows[0]["PMCompleted"].ToString());

        // DateTime? lst = (Convert.IsDBNull(dte.Rows[dte.Rows.Count - 1]["PMCompleted"]) ? null : (DateTime)dte.Rows[dte.Rows.Count - 1]["PMCompleted"]);
        //DateTime? fst = (Convert.IsDBNull(dte.Rows[0]["PMCompleted"]) ? null : (DateTime?)dte.Rows[0]["PMCompleted"]);

        //if (customerID == 8) {
        //    customerID.ContractStatus(out x);

        //}


        // if (customerID == 18) {
        if (fst.IsNullOrEmpty() || lst.IsNullOrEmpty()) {
            if (fst.IsNullOrEmpty()) {
                customerID.ContractStatus(mdlID, servicePlanID, out x);
                fst = x;
            }
            if (lst.IsNullOrEmpty()) {
                customerID.ContractStatus(mdlID, servicePlanID, out x);
                lst = x;
            }

            //check for expired contract    ServicePlanStatusLU_cbo
        }
        else {
            DateTime dateTimeObject = DateTime.Parse(fst);
            fst = dateTimeObject.ToString("MM/dd/yyyy");

            dateTimeObject = DateTime.Parse(lst);
            lst = dateTimeObject.ToString("MM/dd/yyyy");


        }
        //}

        return (fst, lst);
       
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
