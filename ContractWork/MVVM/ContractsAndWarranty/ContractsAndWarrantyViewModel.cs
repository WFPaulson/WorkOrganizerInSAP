using SQL = ContractWork.MVVM.ContractAndAssets.ContractAndAssetsSQLStatements;
namespace ContractWork.MVVM.ContractsAndWarranty;

public partial class ContractsAndWarrantyViewModel : ObservableObject {

    #region Excel Observable Statements
    [ObservableProperty]
    private ExcelService _xlContractsAndWarranty;

    [ObservableProperty]
    private DataTable _excelDatatable;
    //partial void OnExcelDatatableChanging(DataTable value) {
    //    if (value != null) {
    //        updateExcelDataGrid(ExcelDatatable);
    //    }
    //}

    [ObservableProperty]
    private GVis _visibleColumns;

    [ObservableProperty]
    private bool _didXLFail;

    [ObservableProperty]
    private string _xlFileName = string.Empty;

    [ObservableProperty]
    private bool? _viewXLArchive;

    [ObservableProperty]
    private string _pickedColumn;

    [ObservableProperty]
    private string _filterByItem = string.Empty;

    [ObservableProperty]
    private bool _viewXLArchivedIsEnabled = false;

    [ObservableProperty]
    private string _xltextOverlay = "View by...";

    [ObservableProperty]
    private string _xlPickColumnTextOverlay = "Pick Column to Filter...";

    [ObservableProperty]
    private bool _filterByXLArchivedIsEnabled = false;

    [ObservableProperty]
    private string _xlSelectedView;

    [ObservableProperty]
    private bool _updateXLArchived = false;

    [ObservableProperty]
    private bool _xlArchiveCheckbox = false;

    [ObservableProperty]
    private bool _excelSortFilterIsChecked;

    [ObservableProperty]
    private string _pickedXLColForCompare;

    [ObservableProperty]
    private bool _pickedXLColForCompareIsEnabled;

    [ObservableProperty]
    private ObservableCollection<string> _excelFilterBy;

    [ObservableProperty]
    private ObservableCollection<string> _xlColList;

    #endregion

    #region Excel Properties
    public GC dgExcelColumnWidths { get; set; }
    public static string XLfailed;

    public List<int> DataGridMinColumnWidths { get; set; }
    public List<int> DataGridColumnWidths { get; set; }
    public List<int> NewColumnWidth { get; set; }

    #endregion


    #region Access Observable Statements
    [ObservableProperty]
    private AccessService _dbcontractsAndWarranty;

    [ObservableProperty]
    private DataTable _accessDatatable;

    [ObservableProperty]
    private string _didDBFail = string.Empty;

    [ObservableProperty]
    private string _dbFileName;

    [ObservableProperty]
    private string _dbtextOverlay = "View by...";

    [ObservableProperty]
    private string _changeArchiveStatus = "Archive Service Plan";

    [ObservableProperty]
    private bool? _viewDBArchive;

    [ObservableProperty]
    private bool _viewDBArchivedIsEnabled = false;

    [ObservableProperty]
    private string _dbSelectedView;

    [ObservableProperty]
    private bool _accessSortFilterIsChecked;

    [ObservableProperty]
    private bool _betweenDatePickerEnabled = false;

    [ObservableProperty]
    private bool _afterDatePickerEnabled = false;

    [ObservableProperty]
    private string _compareSelection;

    [ObservableProperty]
    private bool _areAccessAndExcelLoaded = false;

    [ObservableProperty]
    private string _openCloseAccessText = "Close Access";

    [ObservableProperty]
    private ObservableCollection<string> _dbTblList;

    [ObservableProperty]
    private ObservableCollection<string> _dbColList;

    #endregion

    #region Access Properties
    public static string DBfailed;
    public List<string> CompareList { get; set; }

    #endregion


    #region Sort or Filter Observable Statements
    [ObservableProperty]
    private ObservableCollection<string> _sortFilterList;

    [ObservableProperty]
    private string _txtbx1;

    [ObservableProperty]
    private string _txtbx2;
        
    [ObservableProperty]
    private string _txtbx3;
        
    [ObservableProperty]
    private string _txtbx4;
        
    [ObservableProperty]
    private string _txtbx5;
        
    [ObservableProperty]
    private string _txtbx6;
        
    [ObservableProperty]
    private bool _sortIsChecked;
        
    [ObservableProperty]
    private bool _filterIsChecked;

    [ObservableProperty]
    private ObservableCollection<string> _excelOrAccessColumns;

    [ObservableProperty]
    private ObservableCollection<string> _sortOrFilterList;
        
    [ObservableProperty]
    private ObservableCollection<string> _selectedSection;

    #endregion

    #region Sort-Filter Properties
    public bool SortOrFilterColumnItem { get; set; }
    public bool IsChecked { get; set; }
    public ObservableCollection<string> tmpSortFilterCollection { get; set; }

    #endregion


    #region Compare properties
    //ExcelCustomerAccount replacing with
    [ObservableProperty]
    private XLData _cl_XLcustomerAccount;
        
    [ObservableProperty]
    private CustomerAccountModel _cl_DBcustomerAccount;
        
    [ObservableProperty]
    private List<XLData> _newSide;
        
    [ObservableProperty]
    private List<XLData> _expiredSide;
        
    [ObservableProperty]
    private string _column1Header;
        
    [ObservableProperty]
    private string _column2Header;
        
    [ObservableProperty]
    private string _column3Header;
        
    [ObservableProperty]
    private Visibility _customerAccountsIsEnabled = Visibility.Hidden;
        
    [ObservableProperty]
    private Visibility _servicePlanIsEnabled = Visibility.Hidden;

    #endregion

    public ObservableCollection<string> ViewList { get; set; }


    #region Customer Account



    #endregion


    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private ObservableCollection<XLData> _equipList;
        
    [ObservableProperty]
    private ObservableCollection<CustomerServicePlanModel> _servicePlanArchiveList;
        
    [ObservableProperty]
    private XLData _selectedCustomer;
        
    [ObservableProperty]
    private bool _selectServicePlan;
        
    [ObservableProperty]
    private ObservableCollection<XLData> _equipmentToArchive;
        




    public AccessService accessDB;
    public NavigationService _navigationService { get; set; }

    public ContractsAndWarrantyViewModel(NavigationService navigationService) {
        _navigationService = navigationService;

        accessDB= new ();
        
        DbcontractsAndWarranty = new();
        XlContractsAndWarranty = new();
        Cl_XLcustomerAccount = new();
        Cl_DBcustomerAccount = new();
        EquipList = new();
        ExcelDatatable = new();

        SelectedSection = new();
        // SortFilterList = new ObservableCollection<string>();

        //EquipList = new ObservableCollection<XLData>();
        
        //GetEquipList();
        RunInitialSetup();

    }
    

    [RelayCommand (CanExecute = nameof(IsContractSelected))]
    private void InspectContract()
    {
        //CustomerServicePlan planNumber = new CustomerServicePlan();
        //planNumber.ServicePlanNumber = SelectedCustomer.ServicePlanNumber;

        //ContractInspectWindow contractInspect = new ContractInspectWindow() {
        //    DataContext = _navigationService.CurrentViewModel = new ContractInspectViewModel(_navigationService, SelectedCustomer.MainAccountNumber)      //, Cl_XLcustomerAccount, dtEquipment)
        //};
        //contractInspect.Show();



        //ServicePlanDetailsWindow servicePlanDetailsWindow = new ServicePlanDetailsWindow()
        //{
        //    DataContext = _navigationService.CurrentViewModel = new ServicePlanDetailsViewModel(_navigationService, SelectedCustomer) { }
        //};
        //servicePlanDetailsWindow.Show();
    }
    private bool IsContractSelected() {
        //if (SelectedCustomer != null) {
        //    return SelectedCustomer.IsSelected;
        //}
        return false;
    }

    //private void GetEquipList() {
    //    EquipList.Add(new tempEquip("Billy Bob", "11111111", false));
    //    EquipList.Add(new tempEquip("Sally Slob", "22222222", false));
    //    EquipList.Add(new tempEquip("Karey Cob", "33333333", false));
    //    EquipList.Add(new tempEquip("Molly Moo", "44444444", false));
    //    EquipList.Add(new tempEquip("Polly Ploo", "55555555", false));
    //    EquipList.Add(new tempEquip("Zanny Zoo", "66666666", false));
    //}

    private void updateExcelDataGrid(DataTable dt) {
        VisibleColumns = new GVis();
        EquipList.Clear(); 
           
        foreach (DataRow row in ExcelDatatable.Rows) {
            EquipList.Add(new XLData(row));
        }
        if (EquipList != null && EquipList.Count > 0) {
            VisibleColumns = EquipList[0].VisCol;
        }
    }

    [RelayCommand]
    private void ArchiveByServicePlanNumber() {
        EquipmentToArchive = new ObservableCollection<XLData>();
        string planNumber = string.Empty;

        if (SelectedCustomer != null) {
            planNumber = SelectedCustomer.Contract;
        }

        bool chkvalue = SelectedCustomer.IsSelected;

        foreach (XLData equipment in EquipList) {
            if (equipment.Contract == planNumber) {
                equipment.IsSelected = chkvalue;
                EquipmentToArchive.Add(equipment);
            }

            else if (chkvalue != false) {
                equipment.IsSelected = !chkvalue;
            }
        }
    }

    [RelayCommand]
    private void Check() {
        Clipboard.SetText("Hello world");
    }

    [RelayCommand]
    private void SelectServicPlan() {
        //Clipboard.SetText("Hello world");
        //MessageBox.Show("Hello");
    }

    [RelayCommand(CanExecute = nameof(XLinViewCurrent))]
    private void XLArchiveEquipment() {
        //ArchiveSelectionWindow archiveSelectionWindow = new ArchiveSelectionWindow() {
        //    DataContext = _navigationService.CurrentViewModel = new ArchiveSelectionViewModel(_navigationService, ServicePlanExcel: SelectedCustomer)
        //};
    }
    private bool XLinViewCurrent() {
        //if (XlContractsAndWarranty.ViewArchived == ExcelWB.ViewCurrent) return true;
        //else 
            return false;
    }


    //[RelayCommand]
    //private void MaximizeExcelDatagrid() {
    //    MaxDatagridWindow ExcelSpreadsheetPopup = new MaxDatagridWindow() {
    //        DataContext = _navigationService.CurrentViewModel = new MaxDatagridViewModel(_navigationService, ExcelDatatable, ExcelFilterBy, dgExcelColumnWidths)
    //    };
    //    ExcelSpreadsheetPopup.Show();
    //}

    [RelayCommand]
    private void OpenCloseAccessFile() {
        //if (OpenCloseAccessText == "Close Access") { CloseAccessFile(); }
        //else if (OpenCloseAccessText == "Open Access") {
        //    DbcontractsAndWarranty.GetNewFilelocation();
        //    OpenAccessFile(); 
        //}
    }

    private void RunInitialSetup() {
        GV._popupClosedByX = "";
        ViewList = new();
        ViewList.Add("Viewing By Current");
        ViewList.Add("Viewing By All");
        ViewList.Add("Viewing By Archived");

        SetCompareList();
        OpenExcelFile();
        if (GV._popupClosedByX == "Failed") { return; }
        OpenAccessFile();
        if (GV._popupClosedByX == "Failed") { return; }
    }

#region Excel Methods
    [RelayCommand]
    private void OpenExcelFile() {
        ExcelDatatable = new();
        ExcelDatatable.Clear();
        dgExcelColumnWidths = new();

        if (XlContractsAndWarranty != null) { XlContractsAndWarranty.CloseXL(); }
        (ExcelDatatable, DidXLFail) = XlContractsAndWarranty.OpenExcelFile(SQL.OpenExcelFileSQL(), FL.fileLocatinDict["ContractsFilePath"]);
        //"Edited$");  OpenExcelFileSQL OpenExcelFileWithDollarValueSQL

        if (DidXLFail) {
            GV._popupClosedByX = "Failed";
            MessageBox.Show("File or connection Failed");
            return;
        }

        if (ExcelDatatable == null) {
            MessageBox.Show("Excel DataTable is null");
            return;
        }

        dgExcelColumnWidths.GetMaxColumnWidths(ExcelDatatable);
            
        ViewXLArchivedIsEnabled = true;
        XltextOverlay = "Viewing By Current";
        XlFileName = FL.fileLocatinDict["AssetsFilePath"];       //GL.ExcelFileLocation;
        ExcelFilterBy = XlContractsAndWarranty.FilterBy;
        FilterByXLArchivedIsEnabled = true;

    }

    [RelayCommand]
    private void ViewArchivedinExcel() { //_xlSelectedView
        if (XlSelectedView == "Viewing By All") { XlContractsAndWarranty.ViewArchived = ExcelService.ViewAll; }
        else if (XlSelectedView == "Viewing By Current") { XlContractsAndWarranty.ViewArchived = ExcelService.ViewCurrent; }
        else if (XlSelectedView == "Viewing By Archived") { XlContractsAndWarranty.ViewArchived = ExcelService.ViewArchiveOnly; }

        //XlContractsAndWarranty.BuildSQLStatement();
        (ExcelDatatable, _) = XlContractsAndWarranty.RefreshSpreadSheet(SQL.OpenExcelFileSQL());
    }

    [RelayCommand]
    private void UpdateXLArchivedContracts() {
        MessageBox.Show("archive enabled");
    }

    [RelayCommand]
    private void FilterXLWorksheetBy() {
        //string SelectFrom = "SELECT ",
        //        All = "* ",
        //        From = "FROM ";
        //        //WSName,
        //        //Where = "WHERE ",
        //        //ColumnName,
        //        //Value,
        //        //FilterBy,
        //        //Distinct = " distinct";

        //SelectFrom += All + From;

        //XlContractsAndWarranty.PickedFilterColumn = PickedColumn;
        //if (!string.IsNullOrEmpty(PickedColumn)) {
        //    XlContractsAndWarranty.GetColumnItems("Single", "Expired");
        //    XlContractsAndWarranty.ViewArchived = XlArchiveCheckbox ? ExcelService.ViewAll : ExcelService.ViewCurrent;
            
        //    //XlContractsAndWarranty.BuildSQLStatement();
        //    ExcelDatatable = XlContractsAndWarranty.RefreshSpreadSheet(SQL.OpenExcelFileSQL());
        //}
    }

    private bool IsFilterImplemented() {
        if(!string.IsNullOrEmpty(PickedColumn)) return true;
        return false;
    }
    [RelayCommand(CanExecute = nameof (IsFilterImplemented))]
    private void ClearXLFilter() {
        //XlContractsAndWarranty.PickedFilterColumn = string.Empty;
        //XlContractsAndWarranty.PickedFilterItem = string.Empty;

        ////XlContractsAndWarranty.BuildSQLStatement();
        //ExcelDatatable = XlContractsAndWarranty.RefreshSpreadSheet();
            
        //XLPickColumnTextOverlay = "Pick Column to Filter...";
            
        //PickedColumn = string.Empty;
    }

    private bool AnyExcelToClose() {
        if (XlContractsAndWarranty.XLDatatable == null ||
            XlContractsAndWarranty.XLDatatable.Rows.Count <= 0) { return false; }
        else return true;
    }

    [RelayCommand(CanExecute = nameof(AnyExcelToClose))]
    private void CloseExcelFile() {
        ExcelDatatable = XlContractsAndWarranty.CloseExcel();
        //UpdateXLArchived = false;
    }

#endregion

#region Access Methods
    private void OpenAccessFile() {
        AccessDatatable = new();
        AccessDatatable.Clear();

        if (DbcontractsAndWarranty != null) { DbcontractsAndWarranty.CloseDB(); }
        (AccessDatatable, DidDBFail) = DbcontractsAndWarranty.OpenAccessFile(ContractAndAssetsSQLStatements.OpenAccessFileSQL(), FL.fileLocatinDict["AccessFilePath"], "tblServicePlan");

        if (DidDBFail == "Failed") {
            GV._popupClosedByX = "Failed";
            MessageBox.Show("File or connection Failed");
            return;
        }

        if (AccessDatatable == null) {
            MessageBox.Show("Excel DataTable is null");
            return;
        }
        
        ViewDBArchivedIsEnabled = true;
        DbtextOverlay = "Viewing By Current";
        ChangeArchiveStatus = "Archive Service Plan";
        DbFileName = DashboardViewModel.fileLocatinDict["AccessFilePath"];
        BetweenDatePickerEnabled = true;
        AfterDatePickerEnabled = true;
        OpenCloseAccessText = "Close Access";
        
    }

    [RelayCommand]  //_dbSelectedView
    private void SetViewInAccessDataGrid() {
        if (DbSelectedView == "Viewing By Current") {
            DbcontractsAndWarranty.ViewArchived = AccessService.ViewCurrent;
            ChangeArchiveStatus = "Archive Service Plan";
        }
        else if (DbSelectedView == "Viewing By All") { 
            DbcontractsAndWarranty.ViewArchived = AccessService.ViewAll;
            ChangeArchiveStatus = "Nothing to do...";
        }
        else if (DbSelectedView == "Viewing By Archived") {
            DbcontractsAndWarranty.ViewArchived = AccessService.ViewArchivedOnly;
            ChangeArchiveStatus = "Remove Service Plan Archive";
        }

        //DbcontractsAndWarranty.BuildSQLStatement();
        AccessDatatable = DbcontractsAndWarranty.RefreshDB(SQL.OpenAccessFileSQL());
    }

    private bool IsAccessDataGridLoaded() {
        if (DbcontractsAndWarranty.DBDatatable == null ||
            DbcontractsAndWarranty.DBDatatable.Rows.Count <= 0 ||
            DbcontractsAndWarranty.ViewArchived != AccessService.ViewAll ||
            XlContractsAndWarranty.XLDatatable == null ||
            XlContractsAndWarranty.XLDatatable.Rows.Count <= 0 ||
            XlContractsAndWarranty.ViewArchived != ExcelService.ViewAll) { return false; }
        return true;
    }

    [RelayCommand(CanExecute =nameof(IsAccessDataGridLoaded))]
    private void UpdateExcelArchivedFromAccess() {
        //ObservableCollection<string> DBArchiveList = new ObservableCollection<string>();

        //MessageBox.Show($"get list {GL.nl} Press to Continue");
        //DBArchiveList = DbcontractsAndWarranty.ReadArchived("ServicePlanNumber");

        //MessageBox.Show($"update archives {GL.nl} Press to Continue");
        //XlContractsAndWarranty.UpdateExcelArchivedServicePlans(DBArchiveList);

        //MessageBox.Show($"Refresh database {GL.nl} Press to Continue");
        //AccessDatatable = DbcontractsAndWarranty.RefreshDB(SQL.OpenAccessFileSQL());

        //MessageBox.Show($"Done {GL.nl} Press to Continue");
    }

    private bool IsAccessDataGridViewCorrect() {
        if (DbcontractsAndWarranty.DBDatatable == null || 
            DbcontractsAndWarranty.DBDatatable.Rows.Count <= 0 || 
            DbcontractsAndWarranty.ViewArchived == AccessService.ViewAll) { return false; }
            
        return true;
    }

    [RelayCommand(CanExecute = nameof(IsAccessDataGridViewCorrect))]
    private void UpdateAcessServicePlanArchived() {
        //if (DbcontractsAndWarranty.ViewArchived == AccessService.ViewCurrent) {
        //    DbcontractsAndWarranty.UpdateToArchived(true);
        //}
        //else if (DbcontractsAndWarranty.ViewArchived == AccessService.ViewArchivedOnly) {
        //    DbcontractsAndWarranty.UpdateToArchived(false);
        //}
    }

    
        

    private bool TableAndColumnPicked() {
        //if (!string.IsNullOrEmpty(PickedDBTblForCompare) &&
        //    !string.IsNullOrEmpty(PickedDBColForCompare)) { 
                
            return true; //}
        //return false;
    }
    [RelayCommand(CanExecute = nameof(TableAndColumnPicked))]
    private void RefreshDatabase() {
        //DbcontractsAndWarranty.BuildSQLStatement(table: PickedDBTblForCompare, filterColumn: PickedDBColForCompare);
        AccessDatatable = DbcontractsAndWarranty.RefreshDB(SQL.OpenAccessFileSQL());
        // GetResultsIsEnabled = true;

    }
    private bool RefreshAndExcelTableLoaded() {
        if (TableAndColumnPicked() && !string.IsNullOrEmpty(PickedXLColForCompare)) {
            return true;
        }
        return false;
    }
        

    private bool IsDatabaseTblReady() {
        if (DbcontractsAndWarranty.DBDatatable.Rows.Count <= 0) { return false; }
        else return true;
    }

    private bool AnyAccessToClose() {
        if (DbcontractsAndWarranty.DBDatatable == null || 
            DbcontractsAndWarranty.DBDatatable.Rows.Count <= 0) { return false; }
        return true;
    }
    [RelayCommand (CanExecute = nameof(AnyAccessToClose))]
    private void CloseAccessFile() {
        AccessDatatable = DbcontractsAndWarranty.CloseAccess();
        BetweenDatePickerEnabled = false;
        AfterDatePickerEnabled = false;
        OpenCloseAccessText = "Open Access";
        //PickedDBColForCompareIsEnabled = false;
        //PickedDBTblIsEnabled = false;
    }

    private void AddNewAccount() {
        //string tmpInsert = $"INSERT INTO [tblCustomerAccounts]([AccountNameTXT_FK],[MainAccount],[BillingAccount],[ShippingAccount]," +
        //                    $"[ProCareRepLU_cbo],[SalesRepLU_cbo],[Archive],[Notes])" +
        //                    $"VALUES('{customerAccount.AccountName}','{customerAccount.MainAccountNumber}','{customerAccount.BillingAccountNumber}'," +
        //                    $"'{customerAccount.ShippingAccountNumber}','{customerAccount.ServiceRep}','{customerAccount.SalesRep}'," +
        //                    $"{customerAccount.AccountArchived},'{customerAccount.Notes}')";



    }

    #endregion

#region Compare Stuff Methods

    private void SetCompareList() {
        CompareList = new();
        CompareList.Add("Customer Accounts");
        CompareList.Add("ServicePlans");
    }

    [RelayCommand(CanExecute = nameof(AreBothAccessAndExcelLoaded))]
    private void RunCompareScript() {
        switch (CompareSelection) {
            case "Customer Accounts":
                Column1Header = "Main Account #"; Column2Header = "Account Name";
                CustomerAccountsIsEnabled = Visibility.Visible;
                ServicePlanIsEnabled = Visibility.Hidden;
                Compare("MainAccount_PK", "tblCustomerAccounts", "MainAccount");
                //PickedXLColForCompare = 
                break;
            case "ServicePlans":
                Column1Header = "Service Plan"; Column2Header = "Account Name"; Column3Header = "Archived?";
                ServicePlanIsEnabled = Visibility.Visible;
                CustomerAccountsIsEnabled = Visibility.Hidden;
                Compare("ServicePlanNumber", "tblServicePlan", "ServicePlanNumber"); 
                break;

        }
    }
    private bool AreBothAccessAndExcelLoaded() {
        if (DbcontractsAndWarranty.DBDatatable != null &&
            DbcontractsAndWarranty.DBDatatable.Rows.Count > 0 &&
            DbcontractsAndWarranty.ViewArchived == AccessService.ViewCurrent &&
            XlContractsAndWarranty.XLDatatable != null &&
            XlContractsAndWarranty.XLDatatable.Rows.Count > 0 &&
            XlContractsAndWarranty.ViewArchived == ExcelService.ViewCurrent) { AreAccessAndExcelLoaded = true; return true; }
        AreAccessAndExcelLoaded = false; return false;
    }

    private void Compare(string dbColumnName, string tblName, string xlColumnName) {
        string Distinct = "DISTINCT";
            //All = "* ";
        //List<string> duplicates = new List<string>();   
        string listofXLduplicates = string.Empty;
        string listofDBduplicates = string.Empty;
        //string combinedDuplicates = string.Empty;

        Dictionary<string, string> dictDBServicePlanNumbers = new();
        Dictionary<string, string> dictXLServicePlanNumbers = new();
        NewSide = new List<XLData>();
        ExpiredSide = new List<XLData>();
        bool isArchived;

        //if (Column1Header == "Service Plan") {
//(listofDBduplicates, dictDBServicePlanNumbers) = DbcontractsAndWarranty.GetServicePlansForCompare(Distinct, dbColumnName, tblName);
//        (listofXLduplicates, dictXLServicePlanNumbers) = XlContractsAndWarranty.GetXLColumnStrStrForCompare(Distinct, xlColumnName);
        //}
        //else {
            //  (listofDBduplicates, dictDBServicePlanNumbers) = DbcontractsAndWarranty.GetServicePlansForCompare(Distinct, dbColumnName, tblName);
            //  (listofXLduplicates, dictXLServicePlanNumbers) = XlContractsAndWarranty.GetXLColumnStrStrForCompare(Distinct, xlColumnName);
        //}
        //DataTable dt = XlContractsAndWarranty.GetXLColumnStrStrForCompare(Distinct, xlColumnName);

        //bool exists;
        //string acctName = string.Empty;

        //foreach (DataRow row in dt.Rows) {
        //    exists = dictXLServicePlanNumbers.TryGetValue(row[0].ToString(), out acctName);

        //    if (!exists) {
        //        dictXLServicePlanNumbers.Add(row[0].ToString(), row[1].ToString());
        //    }


        //    else if (exists) {
        //        //MessageBox.Show($"{row[0]} already exists, Value is: {acctName}");
        //        duplicates.Add(row[0].ToString());
        //    }

        //}


        //if (Column1Header == "Service Plan") {

            


        foreach (var key in dictXLServicePlanNumbers.Keys) {
            if (dictDBServicePlanNumbers.ContainsKey(key)) {    //current both have service Plan
                dictDBServicePlanNumbers.Remove(key);           //current Expired service Plan
            }
            else {                                              //new service Plan
                var value = dictXLServicePlanNumbers[key];
                if (NewSide == null) NewSide = new List<XLData>();
            //    NewSide.Add(new XLData { ServicePlanNumber = key, AccountName = value });
                if (CompareSelection == "Customer Accounts")
                {
                    NewSide.Add(new XLData { CustomerAccount = key, CustomerName = value });

                }
                else NewSide.Add(new XLData { Contract = key, CustomerName = value });
            }
        }

        foreach (var key in dictDBServicePlanNumbers.Keys)
        {
            var value = dictDBServicePlanNumbers[key];
            if (ExpiredSide == null) { ExpiredSide = new List<XLData>(); }

            if (CompareSelection == "Customer Accounts")
            {
                int.TryParse(key, out int acctNumber);
                if (!acctNumber.isAccountArchived())
                {
                    ExpiredSide.Add(new XLData { CustomerAccount = key, CustomerName = value });
                }
            }
            else
            {
                //need to test if serviceplannumber it archived?
                if (!key.isContractArchived())
                {
                    ExpiredSide.Add(new XLData { Contract = key, CustomerName = value });
                }
            }
        }

            //ExpiredSide.Add(new XLData { ServicePlanNumber = key, AccountName = value });

        //}
        // }

        //else if (Column1Header == "Account Name") {
            // foreach (var key in dictXLServicePlanNumbers.Keys) {
                // if (dictDBServicePlanNumbers.ContainsKey(key)) {    //current both have service Plan
                //    dictDBServicePlanNumbers.Remove(key);           //current Expired service Plan
                // }
                // else {                                              //new service Plan
                //    var value = dictXLServicePlanNumbers[key];

                    // if (value.ToLower() == "true") { isArchived = true; }
                    //else  { isArchived = false; }   //if (value.ToLower() == "false")

                    // if (NewSide == null) NewSide = new List<XLData>();

//                       NewSide.Add(new XLData { AccountName = key, AccountArchived = isArchived });

                //}
            //}

            //foreach (var key in dictDBServicePlanNumbers.Keys) {
                //  var value = dictDBServicePlanNumbers[key];
                // if (value.ToLower() == "true") { continue; }               //isArchived = true; }
                // else { isArchived = false; }   //if (value.ToLower() == "false")

                // if (ExpiredSide == null) ExpiredSide = new List<XLData>();
                // ExpiredSide.Add(new XLData { AccountName = key, AccountArchived = isArchived });
            //}
    //     }

        if (!string.IsNullOrEmpty(listofXLduplicates)) {
            MessageBox.Show($"List of XL duplicates: {listofXLduplicates}");

        }
        if (!string.IsNullOrEmpty(listofDBduplicates)) {
            MessageBox.Show($"List of DB duplicates: {listofDBduplicates}");

        }

    }


    private void CompareCustomerAccounts() {
            MessageBox.Show("Customer Accounts");
        }


    #endregion

    #region Sort Filter Methods   
    [RelayCommand]
    private void SortOrFilterColumnChoose() {

    }

    [RelayCommand(CanExecute = nameof(AnyExcelToClose))]
    private void ExcelSortOrFilter() {
        ExcelOrAccessColumns = new ObservableCollection<string>();

        foreach (var item in ExcelFilterBy) {

            ExcelOrAccessColumns.Add(item);
        }

        //foreach (var item in MetalRateOnDate) {
        //    var clone = (RateModel)item.Clone();
        //    AllMetalRate.Add(clone);
        //}

        // ExcelOrAccessColumns = ExcelFilterBy;
            

        // tmpSortFilterCollection = ExcelFilterBy;

        //string SQLStatement;
        //string lbl;
        //string txtbx;
        //int row = 1;

    }

    [RelayCommand(CanExecute = nameof(AnyExcelToClose))]
    private void AccessSortOrFilter() {
        MessageBox.Show("access clicked");
    }

    private bool IsExcelOrAccessLoaded() {
        if (XlContractsAndWarranty.XLDatatable != null && XlContractsAndWarranty.XLDatatable.Rows.Count > 0 ||
                DbcontractsAndWarranty.DBDatatable != null && DbcontractsAndWarranty.DBDatatable.Rows.Count > 0) { 
            return true; 
        }
        else return false;
    }
    [RelayCommand(CanExecute = nameof(IsExcelOrAccessLoaded))]
    private void Sort() {
        MessageBox.Show($"sort value {SortIsChecked}");
    }

    [RelayCommand(CanExecute = nameof(IsExcelOrAccessLoaded))]
    private void Filter() {
        SortFilterList = new ObservableCollection<string>();
        string SelectBy = "SELECT ";  //may not need
        string OrderBy = string.Empty;
        string orderList = string.Empty;
        string selectList = string.Empty;
        string tbx = string.Empty;

        //PropertyInfo dateProperty = typeof(DateTime).GetProperty("Date");   //typeof(ContractsAndWarrantyViewModel).GetProperty(txtbx1);  ???
        //PropertyInfo utcNowProperty = typeof(DateTime).GetProperty("UtcNow");

        for (int i = 1; i < 7; i++) {
            tbx = "txtbx";
            tbx += i;
                
            switch (tbx) {
                case "txtbx1": tbx = Txtbx1; break;
                case "txtbx2": tbx = Txtbx2; break;
                case "txtbx3": tbx = Txtbx3; break;
                case "txtbx4": tbx = Txtbx4; break;
                case "txtbx5": tbx = Txtbx5; break;
                case "txtbx6": tbx = Txtbx6; break;
                default: break; 
            }

            if (!string.IsNullOrEmpty(tbx)) {
                SortFilterList.Add(tbx);
                orderList += SortFilterList[i-1] + ", ";
                selectList += "[" + SortFilterList[i-1] + "], ";
            }
        }
        OrderBy += orderList;
        SelectBy += selectList;
        OrderBy = OrderBy.Remove(OrderBy.Length - 2);
        SelectBy = SelectBy.Remove(SelectBy.Length - 2);
        SelectBy += " ";
        OrderBy = "'" + OrderBy + "'";


        //XlContractsAndWarranty.BuildSQLStatement(Select: SelectBy, OrderByColumns: OrderBy);
        ExcelDatatable = null;
        ExcelDatatable = new DataTable();
            
        (ExcelDatatable, _) = XlContractsAndWarranty.RefreshSpreadSheet(SQL.OpenAccessFileSQL());

    }

    private bool AnythingToClear() {
        if (FilterIsChecked || SortIsChecked || AccessSortFilterIsChecked || ExcelSortFilterIsChecked) {
            return true;
        }
        else return false;
    }
    [RelayCommand(CanExecute = nameof(AnythingToClear))]
    private void ClearAllSortOrFilter() {
            FilterIsChecked = false;
            SortIsChecked = false;
            AccessSortFilterIsChecked = false;
            ExcelSortFilterIsChecked = false;
            ExcelOrAccessColumns.Clear();

            Txtbx1 = string.Empty;
            Txtbx2 = string.Empty;
            Txtbx3 = string.Empty;
            Txtbx4 = string.Empty;
            Txtbx5 = string.Empty;
            Txtbx6 = string.Empty;

            ExcelDatatable = new DataTable();
            ExcelDatatable.Clear();

//(ExcelDatatable, DidXLFail) = XlContractsAndWarranty.ReOpenDefaultExcelFile();

            if (!DidXLFail) {
                MessageBox.Show("File or connection Failed");
                return;
            }
        }

#endregion

#region Contract stuff Methods

    
    [RelayCommand(CanExecute = nameof(ExpiredServicePlanList))]
    private void RemoveExpired() {
        //AccessDB accessDB= new AccessDB();
        //CustomerAccount customer = new CustomerAccount();

        //accessDB.ViewArchived = AccessDB.ViewAll;

        //DataTable dbTable = accessDB.FetchDBRecordRequest("*", "tblCustomerAccounts", "MainAccount_FK", intfilterItem: SelectedCustomer.MainAccountNumber );
        //customer.tblCustomerAccountData(dbTable.Rows[0]);

        //ArchiveSelectionWindow viewCustomer = new ArchiveSelectionWindow() {
        //    DataContext = _navigationService.CurrentViewModel = new ArchiveSelectionViewModel(_navigationService, customer)
        //};
        //viewCustomer.Show();
        ////////////////////////Below uncomment////////////////////////////////////////
        //if (SelectedCustomer == null) {
        //    MessageBox.Show("Selected Customer Null, please try to pick again");
        //    return;
        //}

        //ContractInspectWindow contractInspect = new ContractInspectWindow() {
        //    DataContext = _navigationService.CurrentViewModel = new ContractInspectViewModel(_navigationService, SelectedCustomer.MainAccountNumber) // SelectedCustomer)      //, Cl_XLcustomerAccount, dtEquipment)
        //};
        //contractInspect.Show();
    }
    private bool ExpiredServicePlanList() {
        if (ExpiredSide != null && ExpiredSide.Count > 0) { return true; }
        return false;
    }


    [RelayCommand(CanExecute = nameof(NewServicePlanList))]
    private void CreateNew() {
        if (SelectedCustomer != null) {
            if (string.IsNullOrEmpty(SelectedCustomer.Contract)) { CreateNewAccount(); }
            else { AddToServicePlan(); }
        }
    }
    private bool NewServicePlanList() {
        if (NewSide != null && NewSide.Count > 0) { return true; }
        return false;
    }

    private void AddToServicePlan() {
            DataTable dt = new DataTable();
            MessageBoxResult answer;
            bool result;
            int count = 0;

            string filteritem = $"{SelectedCustomer.Contract}";
            
//XlContractsAndWarranty.BuildSQLStatement(filterColumn: "ServicePlanNumber", filterItem: filteritem);

            //do {
            //    dt = XlContractsAndWarranty.GetRecord();
            
            //    Cl_XLcustomerAccount.ExcelScrubDataTable(dt.Rows[0]);
            //    result = DoesThisCustomerHaveAnAccount($"{Cl_XLcustomerAccount.AccountName}");
            //    if (count > 0) {
            //        answer = MessageBox.Show("ServicePlan was not added. Do you want to continue", "?Continue?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //        if (answer == MessageBoxResult.No) { return; }
            //    }
            //    if (!result) {
            //        CreateNewAccount();
            //        count++;
            //    }
            //} while (result == false);

            //count= 0;
            //do {
            //    result = Cl_XLcustomerAccount.ServicePlanNumber.DoesThisServicePlanExist();                //CSP.DoesThisServicePlanExist($"{Cl_XLcustomerAccount.ServicePlanNumber}");
            //    if ((count > 0) && (result == false)) {
            //        answer = MessageBox.Show("ServicePlan was not added. Do you want to continue", "?Continue?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //        if (answer == MessageBoxResult.No) { return; }
            //    }
            //    if (!result) {
            //        //comes from here first
            //        CreateServicePlanToAccount();
            //        count++;
            //    }
            //} while (result == false);

            
        }

        

    private void CreateServicePlanToAccount() {

        //string sqlEquipmentList =
        //    "SELECT [ServicePlanNumber], [EquipmentSerialNumber], [ServicePlanStatus], [ServicePlanStartDate], [POExpireDate], " +
        //    "[ServicePlanExpireDate], [Contract Description], [Model Description] " +
        //    "FROM [Edited$] " +
        //    $"WHERE [ServicePlanNumber] = '{Cl_XLcustomerAccount.ServicePlanNumber}'";

        //XlContractsAndWarranty.BuildSQLStatement(fullSQLStatement: sqlEquipmentList);

        //string _servicePlanNumber = Cl_XLcustomerAccount.ServicePlanNumber;

        //DataTable dtEquipment = XlContractsAndWarranty.GetRecord();
        //Cl_XLcustomerAccount = GetSelectedCustomerData(SelectedCustomer);

            
        ////comes here first to create serviceplan, this first one does not send equipment list only creates the service plan
        ////need to have a new serviceplan window to view and add ServicePlanEntryWindow
        //ServicePlanEntryWindow ServicePlanEntry = new ServicePlanEntryWindow() {
        //    DataContext = _navigationService.CurrentViewModel = new ServicePlanEntryViewModel(_navigationService, Cl_XLcustomerAccount, dtEquipment)
        //};
        //ServicePlanEntry.ShowDialog();

        ////!!! if you exit and dont create a account service plan it will keep looping need to setup an exit from the do while loop
    }

    private bool DoesThisCustomerHaveAnAccount(string filterItem) {
        //DataTable dt = new DataTable();
        //AccessService accessDB = new();

        //dt = accessDB.FetchDBRecordRequest("AccountName", "tblAccountName_LU", "AccountName", filterItem);
        //if (dt == null || dt.Rows.Count <= 0) { return false; }
        //else { return true; }
        return false;
    }

    private void CreateNewAccount() {
        //Cl_XLcustomerAccount = GetSelectedCustomerData(SelectedCustomer);

        //CustomerAccountEntryWindow customerAccountEntry = new CustomerAccountEntryWindow() {
        //    DataContext = _navigationService.CurrentViewModel = new CustomerAccountEntryViewModel(_navigationService, Cl_XLcustomerAccount)
        //};
        //customerAccountEntry.Show();
    }

    private XLData GetSelectedCustomerData(XLData _selectCust) {
            DataTable dt = new DataTable();
            string item = string.Empty;
            string column = string.Empty;

            //change to MainAccountNumber

            if (string.IsNullOrEmpty(_selectCust.Contract)) { 
                item = $"{_selectCust.CustomerName}";
                column = "AccountName";
            }
            else { 
                item = $"{_selectCust.Contract}";
                column = "ServicePlanNumber";
            }

            //XlContractsAndWarranty.BuildSQLStatement(filterColumn: column, filterItem: item);
           // dt = XlContractsAndWarranty.GetRecord();
           // Cl_XLcustomerAccount.ExcelScrubDataTable(dt.Rows[0]);

            return Cl_XLcustomerAccount;
        }
}
    #endregion
