using System;
using Microsoft.VisualBasic;

namespace ContractWork.MVVM.CustomerDetails;

public interface ICustomerEquipment {
    int EquipmentID { get; set; }
    string SerialNumber { get; set; }
}

public partial class CustomerEquipmentModel : ObservableObject, ICustomerAccount, ICustomerEquipment, ICustomerServicePlan {

    #region constant and dictionary

    List<string> LP15_versionList = new List<string>() {
        "v4",
        "v2",
        "v1",
        ""
    };
    List<string> Lucas_versionList = new List<string>() {
        "3.1",
        "2.2",
        "2.1",
        "2.0",
        ""
    };
    List<string> Other_versionList = new List<string>() {
        "1",
        ""
    };

    string[] PMCompletedParams = { "tblEquipment", "PMCompleted", "EquipmentID" };
    string[] PMCompletedCheckParams = { "tblEquipment", "PMCompletedCheck", "EquipmentID" };
    string[] ModelNumberParams = { "tblEquipment", "ModelID", "EquipmentID" };
    string[] VersionNumberParams = { "tblEquipment", "VersionID", "EquipmentID" };
    string[] PMMonthParameters = { "tblEquipment", "PMMonth", "EquipmentID" };
    string[] PMMonthNumberParameters = { "tblEquipment", "PMMonthNumber", "EquipmentID" };
    string[] StatusParameters = { "tblEquipment", "ServicePlanStatusLU_cbo", "EquipmentID" };
    string[] DeviceUnavailableParameters = { "tblEquipment", "DeviceUnavailable", "EquipmentID" };


    public static Dictionary<string, List<string>> ServicePlanListDict;

    #endregion

    AccessService accessDB;

    #region Interface properties

    [ObservableProperty]
    private string _accountName;
    partial void OnAccountNameChanged(string value) {
        if (value != null) { CheckServicePlanList(); }

    }
    private void CheckServicePlanList() {
        if (ServicePlanListDict == null) { ServicePlanListDict = new(); }
        if (!ServicePlanListDict.ContainsKey(AccountName)) {
            string sqlServicePlanList =
                "SELECT ServicePlanNumber " +
                "FROM tblServicePlan " +
                $"WHERE AccountName_cbo = '{AccountName}' AND Archive <> true";

            DataTable dt = accessDB.FetchDBRecordRequest(fullstatement: sqlServicePlanList);
            if (dt != null && dt.Rows.Count > 0) {

                List<string> tmpList = dt.AsEnumerable()
                .Select(x => x["ServicePlanNumber"].ToString()).ToList();

                ServicePlanListDict.Add(AccountName, tmpList);
            }
            else return;


        }
        //List<string> lst = dict.Values.ToList();
        //ServicePlanList = ServicePlanListDict[AccountName] as List<string>;
        ServicePlanList = ServicePlanListDict[AccountName];
    }

    public int CustomerAccountID { get; set; }
    public bool AccountArchived { get; set; }

    public int EquipmentID { get; set; }
    public string SerialNumber { get; set; }

    public int ServicePlanID { get; set; }

    [ObservableProperty]
    private string _servicePlanNumber;
    partial void OnServicePlanNumberChanged(string value) {
        if (value != null) {
            ServicePlanID = value.GetServicePlanID();
        }
        if (update == true)
        {
            AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(ServicePlanStatus, this, StatusParameters);
        }
    }

    #endregion


    #region commented
    //AccountNameList = dt.AsEnumerable()
    //            .Select(x => x["AccountNameTXT_PK"].ToString()).ToList();

    //cboContractList = dt.AsEnumerable()
    //            .Select(x => x["ServicePlanNumber"].ToString()).ToList();

    //if ((_servicePlanNumber != null) && (_servicePlanNumber != "xxxxxxx")) {     //(update == true) && 
    //    GetCorrectID(_servicePlanNumber);
    //}
    //if (update == true) {
    //Serviceplanid is not updated id
    // AccessDB.UpdateCustomerAccountDetails<CustomerEquipment>(ServicePlanID, this, ServicePlanNumberParams); //ModelNumber}
    //}
    //if (ServicePlanID != null && ServicePlanID != 0) {
    //    CheckServicePlanStatus(ServicePlanID);
    //}
    //}
    // }
    // }

    //private void GetCorrectID(string _planNumber) {
    //    ServicePlanID = accessDB.getServicePlanInfo(_planNumber);
    //}
    //private void CheckServicePlanStatus(int planID) {
    //    //CustomerServicePlan checkdate = new CustomerServicePlan(ServicePlanID);
    //    AccessDB accessDB = new AccessDB();
    //    DataTable dt = new DataTable();

    //    string sql =
    //        "SELECT [ServicePlanStatus] " +
    //        "FROM [tblServicePlan] " +
    //        $"WHERE [ServicePlanID] = {planID}";

    //    dt = accessDB.FetchDBRecordRequest(fullstatement: sql);

    //    if (ServicePlanStatus != dt.Rows[0]["ServicePlanStatus"].ToString()) {
    //        ServicePlanStatus = dt.Rows[0]["ServicePlanStatus"].ToString();
    //    }
    //    //ServicePlanStatus = dt.Rows[0]["ServicePlanStatus"].ToString();
    //    //update = false;

    //}

    #endregion

    #region Observalable Property statements

    [ObservableProperty]
    private int _modelID;

    [ObservableProperty]
    private string _modelNumber;
    
    partial void OnModelNumberChanged(string value) {
        ModelID = ModelNumberToModelID(value);
        CheckAbbaList(ModelID);

        if (update) AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(ModelID, this, ModelNumberParams);
        GetVersionList();
    }

    public void CheckAbbaList(int modelID) {
        if (modelID == 1) {
            ABBAList = GV.LP15ABBAList;
        }
        else if (modelID == 2) {
            ABBAList = GV.LucasABBAList;
        }
        else if (modelID == 4) {
            ABBAList = GV.LP20ABBAList;
        }
        else if (modelID == 6) {
            ABBAList = GV.LPCR2ABBAList;
        }

    }

    private string ModelIDToModelNumber(int i) {
        if (i != null) {
            return GV._modelLookUP[i];
        }
        else return GV._modelLookUP[0];
    } 
    private int ModelNumberToModelID(string s) {
        if (!string.IsNullOrEmpty(s)) {
            return GV._reversemodelLookUP[s];
        }
        else return 0;
    }  


    //private string ModelIDToModelNumber(int modelID) {
    //    return GV._modelLookUP[modelID];
    //}
    //private int ModelNumberToModelID(string modelNumber) {
    //    return GV._reversemodelLookUP[modelNumber];
    //}

    [ObservableProperty]
    private int _versionID;

    [ObservableProperty]
    private string _versionNumber;
    partial void OnVersionNumberChanged(string value) {
        VersionID = VersionNumberToVersionID(value);                         //GV._reverseVersionLookUP[value];
        if (update == true && VersionID != 0) {
            AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(VersionID, this, VersionNumberParams);
        }
    }

    //need to add if versionID = 0

    private string VersionIDToVersionNumber(int n) {
        if (n != null) {
            return GV._modelLookUP[n];
        }
        else return GV._versionLookUP[0];
    }

    private int VersionNumberToVersionID(string s) {
        if (!string.IsNullOrEmpty(s)) {
            return GV._reverseVersionLookUP[s];
        }
        else return GV._reverseVersionLookUP[""];
    } 

    [ObservableProperty]
    private bool _deviceUnavailable;
    partial void OnDeviceUnavailableChanged(bool value) {

        if (update == true) {
            AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(DeviceUnavailable, this, DeviceUnavailableParameters);  //DeviceUnavailable
        }

    }


    //TODO:look into why this is not updating field maybe look at adding OnServicePlanStatusChanging
        [ObservableProperty]
    private string _servicePlanStatus;
    partial void OnServicePlanStatusChanged(string value) {
        txtServicePlanStatus = value;
        if (update == true) {
            AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(ServicePlanStatus, this, StatusParameters);
        }
    }
    

    [ObservableProperty]
    private string _modelDescription;

    [ObservableProperty]
    private string _contractDescription;

    [ObservableProperty]
    private string _aBBAString;

    [ObservableProperty]
    private DateTime? _manufactureDate;

    [ObservableProperty]
    private DateTime? _serviceStartDate;

    [ObservableProperty]
    private DateTime? _serviceExpireDate;

    [ObservableProperty]
    private DateTime? _servicePOExpireDate;
    
    private DateTime? DateCheck(DateTime? dateformat) {
        if (dateformat != DateTime.MinValue) {
            return dateformat;
        }
        else return null;
    }

    [ObservableProperty]
    private string _equipmentManufDate;
    partial void OnEquipmentManufDateChanged(string value) {
        if (value != string.Empty) {
            ManufactureDate = DateTime.Parse(value);
        }
    }
    
    //public string EquipmentManufDate {
    //    get => DateToString(ManufactureDate);     //.ToShortDateString();
        
    //}

    [ObservableProperty]
    private bool _equipmentArchived;

    [ObservableProperty]
    private string _notes;



    /// <summary>
    /// All reference for PM Due Month
    /// </summary>

    [ObservableProperty] 
    private string _pmMonthName;

    //i think this is fixed now - need to get PM monthname / monthnumber / monthdue to update in tblequipment from tblserviceplan

    [ObservableProperty]
    private string _pmMonthDue;
    partial void OnPmMonthDueChanged(string value) {
        PmMonthName = DeconstructPMMonthDue(value);
        if (!string.IsNullOrEmpty(PmMonthName) && PmMonthName != "No Date") {
            PmMonthNumber = GV._monthNumberLookup[PmMonthName];
        }
        if (update)
            //value =
            //value = "'" + value + "'";
        //"'" + value.Substring(0, 3) + "'";
        AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(PmMonthName, this, PMMonthParameters);
    }

    private string DeconstructPMMonthDue(string value) {    // "May-5" 
        if (value != string.Empty && value != "No Date")  {
            return value.Substring(0, 3);
        }
        else { return value; }
    }

    private string ReconstructPMMonthDue(string pmMonth) {
        if (pmMonth != string.Empty && pmMonth != "No Date") { return pmMonth + "-" + GV._monthNumberLookup[pmMonth].ToString(); }
        else { return pmMonth; }
    }

    [ObservableProperty]
    private int _pmMonthNumber;
    partial void OnPmMonthNumberChanging(int value) {
        if (_pmMonthNumber != value) {
            _pmMonthNumber = GV._monthNumberLookup[_pmMonthName];
            AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(_pmMonthNumber, this, PMMonthNumberParameters);
        }
        else if (value == null) {
            // _pmMonthNumber = GV._monthNumberLookup[_pmMonthDue];
            AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(_pmMonthNumber, this, PMMonthNumberParameters);
        }

    }


    /// <summary>
    /// All reference for PM Completed date
    /// </summary>
    /// 
    [ObservableProperty]
    private DateTime? _pmcompleted;
    //partial void OnPmcompletedChanging(DateTime value) {
        // if (PMCompleted != null) return PMCompleted;      //String.Format("{0:d}", _PMCompleted);

    //    if (PMCompleted != value.ToString()) {
    //        if (!string.IsNullOrEmpty(value.ToString())) {
    //            if (value.ToString().IndexOf(" ") != -1) { PMCompleted = value.ToString().Substring(0, value.ToString().IndexOf(" ")); }
    //            else PMCompleted = value.ToString();
    //        }
    //        else PMCompleted = value.ToString();

    //    }       //        OnPropertyChanged(nameof(PMCompleted));
    //    else DateToString(Pmcompleted);
        
    //    if (update) AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(PMCompleted, this, PMCompletedParams);
    //}

    partial void OnPmcompletedChanged(DateTime? value) {
//PMCompleted = DateTime.Today.ToString("MM/dd/yy");
       PMCompleted = DateToString(value);       //value.ToShortDateString();
        if (update) AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(PMCompleted, this, PMCompletedParams);
    }

    //public DateTime _pmCompleted {
    //    get => _pmcompleted;
    //    set {
    //        if (_pmcompleted != value) {
    //            _pmcompleted = value;
    //            _PMCompleted = DateToString(_pmcompleted);
    //            OnPropertyChanged(nameof(_pmCompleted));
    //        }
    //    }
    //}

    //[ObservableProperty]
    //private string _pMCompleted;
    //partial void OnPMCompletedChanging(string value) {

    //    if (!string.IsNullOrEmpty(value)) {
    //        if (value.IndexOf(" ") != -1) { PMCompleted = value.Substring(0, value.IndexOf(" ")); }
    //        else PMCompleted = value;
    //    }

    //}

    //partial void OnPMCompleteChanged(string value) {
    //    if (update) AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(PMCompleted, this, PMCompletedParams);
    //}

    [ObservableProperty]
    private string _pMCompleted;
    partial void OnPMCompletedChanged(string value) {
        if (value == "" || value == null) { }
        else {
            if (update) AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(PMCompleted, this, PMCompletedParams);
        
        }
    }
    //partial void OnPMCompletedChanging(string value) {
    //    if (value != null) {


    //    }

    //}
    // public string PMCompleted { get; set; }
    // get;// {
    //if (PMCompleted != null) return PMCompleted;      //String.Format("{0:d}", _PMCompleted);
    //else return DateToString(Pmcompleted);
    //}

    //}
    //}
    private string DateToString(DateTime? dateformat) {
        if (dateformat.HasValue && dateformat != DateTime.MinValue) {
            return String.Format("{0:MM/dd/yy}", dateformat);
                //dateformat.Value.ToShortDateString();
        }
        else return "";
    }


    [ObservableProperty]
    private bool _pMCompletedCheck;
    partial void OnPMCompletedCheckChanged(bool value) {
        if (update) AccessService.UpdateCustomerAccountDetails<CustomerEquipmentModel>(PMCompletedCheck, this, PMCompletedCheckParams);
    }

    public DateTime? UndoDate { get; set; }

    public int DeviceCount { get; set; }

    public bool ServiceExpired { get; set; }
    public bool POExpired { get; set; }

    public string txtServicePlanStatus { get; set; }

    #endregion


    #region Class calls
    [ObservableProperty]
    private List<string> _verList;

    [ObservableProperty]
    private List<string> _aBBAList;
    

    public List<string> ModelList { get; set; }

    //TODO: might need to change this to observableproperty
    public List<string> StatusList { get; set; }

    public List<string> PMMonthList { get; set; }
    public List<string> ServicePlanList { get; set; }

    #endregion

    private bool update = false;

    public bool IsSelected { get; set; }

    public CustomerEquipmentModel() {
        update = false;
        accessDB = new();

        RunInitialSetup();
    }

    public CustomerEquipmentModel(CustomerEquipmentModel entity) {
        accessDB = new();

        ModelList = GV._modelList;
        StatusList = GV._statusList;
        PMMonthList = GV._pmMonthList;

        EquipmentID = entity.EquipmentID;
        CustomerAccountID = entity.CustomerAccountID;
        AccountName = entity.AccountName;
        ServicePlanID = entity.ServicePlanID;
        ServicePlanNumber = entity.ServicePlanNumber;
        SerialNumber = entity.SerialNumber;
        ModelID = entity.ModelID;
        ModelNumber = entity.ModelNumber;
        VersionID = entity.VersionID;
        VersionNumber = entity.VersionNumber;
        PmMonthName = entity.PmMonthName;
        PmMonthDue = entity.PmMonthDue;
        PmMonthNumber = entity.PmMonthNumber;
        Pmcompleted = entity.Pmcompleted;
        PMCompletedCheck = entity.PMCompletedCheck;
        DeviceUnavailable = entity.DeviceUnavailable;
        ServicePlanStatus = entity.ServicePlanStatus;
        ModelDescription = entity.ModelDescription;
        ContractDescription = entity.ContractDescription;
        ABBAString = entity.ABBAString;
        ManufactureDate = entity.ManufactureDate;
        EquipmentArchived = entity.EquipmentArchived;
        Notes = entity.Notes;

    }
    public CustomerEquipmentModel(DataRow dt) {
        update = false;
        accessDB = new();

        RunInitialSetup();
        CustomerEquipmentScrubData(dt);
       
    }

    public void RunInitialSetup() {
        ABBAList = new List<string>();

        ModelList = GV._modelList;
        StatusList = GV._statusList;
        PMMonthList = GV._pmMonthList;
    }

    [RelayCommand]
    private void UpdateDatabase() {
        MessageBox.Show("in customer equipment > Update DB");
    }

    [RelayCommand]
    private void ItemIsDirty() {
        MessageBox.Show($"need to archive serial number22 value");
        //EquipmentIs = Dirty;
    }
   
    [RelayCommand]
    private void ClipboardCopy(string stringToClipboard) {
        stringToClipboard.CopytoClipboard();
    }
    
    public void CustomerEquipmentScrubData(DataRow _row) {
        update = false;

        if (_row.Table.Columns.Contains("EquipmentID")) {
            EquipmentID = (int)_row["EquipmentID"];
        }

        if (_row.Table.Columns.Contains("CustomerAccountID_FK")) {
            CustomerAccountID = (int)_row["CustomerAccountID_FK"];
        }

        if (_row.Table.Columns.Contains("AccountName")) {
            AccountName = _row["AccountName"].ToString();
        }
        else if (_row.Table.Columns.Contains("AccountName_cbo")) {
            AccountName = _row["AccountName_cbo"].ToString();
        }

        if ((_row.Table.Columns.Contains("ServicePlanID_FK")) &&
            (_row.Table.Columns.Contains("ServicePlanNumber"))) {
            ServicePlanID = (int)_row["ServicePlanID_FK"];
            ServicePlanNumber = _row["ServicePlanNumber"].ToString() ?? "";

        }
        else if (_row.Table.Columns.Contains("ServicePlanID_FK")) {
            ServicePlanID = (int)_row["ServicePlanID_FK"];
            ServicePlanNumber = ServicePlanID.getServicePlanInfo();

        }
        else if (_row.Table.Columns.Contains("ServicePlanNumber")) {
            ServicePlanNumber = _row["ServicePlanNumber"].ToString() ?? "";
            //ServicePlanID = accessDB.getServicePlanInfo(ServicePlanNumber);
            ServicePlanID = ServicePlanNumber.getServicePlanInfo();
        }

        if (_row.Table.Columns.Contains("EquipmentSerial")) {
            SerialNumber = _row["EquipmentSerial"].ToString() ?? "";
        }
        else if (_row.Table.Columns.Contains("EquipmentSerialNumber")) {
            SerialNumber = _row["EquipmentSerialNumber"].ToString() ?? "";
        }

        if (_row.Table.Columns.Contains("ModelID")) {
            ModelID = (int)_row["ModelID"];
            ModelNumber = GV._modelLookUP[ModelID];
        }
        if (_row.Table.Columns.Contains("VersionID")) {
            VersionID = (int)_row["VersionID"];
            VersionNumber = GV._versionLookUP[VersionID];
        }

        if (_row.Table.Columns.Contains("PMMonth")) {
            PmMonthName = _row["PMMonth"].ToString() ?? "";
            if (PmMonthName != string.Empty) {
                PmMonthNumber = GV._monthNumberLookup[PmMonthName];
                PmMonthDue = PMMonthList[PmMonthNumber];
            }
            else if (PmMonthName == string.Empty) {
                PmMonthDue = PMMonthList[0];
            }
            //PmMonthDue = _row["PMMonth"].ToString() ?? "";
        }
        else if (_row.Table.Columns.Contains("PMMonthNumber")) { // if (dt["PMMonthNumber"] != DBNull.Value) {
            PmMonthNumber = (int)_row["PMMonthNumber"];
            PmMonthDue = PMMonthList[PmMonthNumber];
        }
        else PmMonthDue = PMMonthList[0];

        if (_row.Table.Columns.Contains("PMCompleted")) {
            if (_row["PMCompleted"] != DBNull.Value) {
                Pmcompleted = (DateTime)_row["PMCompleted"];
            }

                //PMCompletedCheck = Convert.ToBoolean(_row["PMCompleted"]);
        }

        if (_row.Table.Columns.Contains("PMCompletedCheck")) {
                PMCompletedCheck = Convert.ToBoolean(_row["PMCompletedCheck"]);
        }

        if (_row.Table.Columns.Contains("DeviceUnavailable")) {
            DeviceUnavailable = Convert.ToBoolean(_row["DeviceUnavailable"]);
        }

        if (_row.Table.Columns.Contains("ServicePlanStatusLU_cbo")) {
            ServicePlanStatus = _row["ServicePlanStatusLU_cbo"].ToString() ?? "";
        }
        else if (_row.Table.Columns.Contains("ServicePlanStatus")) {
            ServicePlanStatus = _row["ServicePlanStatus"].ToString() ?? "";
        }

        if (_row.Table.Columns.Contains("ModelDescription")) {
            ModelDescription = _row["ModelDescription"].ToString() ?? "";
        }

        if (_row.Table.Columns.Contains("ContractDescription")) {
            ContractDescription = _row["ContractDescription"].ToString() ?? "";
        }

        if (_row.Table.Columns.Contains("ABBAString")) {
            ABBAString = _row["ABBAString"].ToString() ?? "";
        }

        if (_row.Table.Columns.Contains("ManufactureDate")) {
            if (_row["ManufactureDate"] != DBNull.Value) {
                ManufactureDate = ((DateTime)_row["ManufactureDate"]);
            }
        }

        if (_row.Table.Columns.Contains("Archive")) {
            EquipmentArchived = Convert.ToBoolean(_row["Archive"]);
        }

        if (_row.Table.Columns.Contains("Notes")) {
            Notes = _row["Notes"].ToString() ?? "";
        }

        update = true;

    }

    [RelayCommand]
    private void SerialNumberArchive() {
        MessageBox.Show($"need to archive serial number22 value {EquipmentArchived}");
    }

    [RelayCommand]
    private void Check() {
        Clipboard.SetText("Hello world");
    }

    [RelayCommand]
    private void PMDone(bool value) {
        //accessDB = new();
        string sqlPMDoneUpdae =
            "UPDATE [tblEquipment] " +
            $"SET [PMCompletedCheck] = {value} " +
            $"WHERE [EquipmentSerial] = '{SerialNumber}'";

        accessDB.AddToAccount(SQLInsert: sqlPMDoneUpdae);
    }




    [RelayCommand]
    private void SetAllPMDueToSame(string scheduledPMMonth) {

    }


    [RelayCommand]
    private void Clear() {
        if (Pmcompleted != null) {
            UndoDate = Pmcompleted;
        }
        //PMCompleted = string.Empty;
        Pmcompleted = null;
    }

    [RelayCommand(CanExecute = nameof(IsThereAnUndoDate))]
    private void Revert() {
        Pmcompleted = UndoDate;
    }
    private bool IsThereAnUndoDate() {
       // if (string.IsNullOrEmpty(UndoDate)) { return false; }
        if (UndoDate.HasValue && UndoDate != DateTime.MinValue) { return false; }
        else return true;
    }


    [RelayCommand]
    private void Today() {
        UndoDate = Pmcompleted;
        //PMCompleted = DateTime.Today.ToString("MM/dd/yy");       //ToShortDateString();
        Pmcompleted = DateTime.Now;
    }

    [RelayCommand]
    private void ChangeVersion() {
        if (VersionNumber != "") {
            VersionNumber = "";
            GetVersionList();
        }
    }

    private void GetVersionList() {
        switch (ModelNumber) {
            case "LP15":
            VerList = LP15_versionList; break;

            case "Lucas":
            VerList = Lucas_versionList; break;

            default:
            VerList = Other_versionList; break;
        }
    }


    //ServicePlanList = new List<string>()
    //{
    //        "home",
    //        "away",
    //        "current",
    //        "Future Contract"
    //};
    //public static bool DoesEquipmentExist(string serialNumber) {
    //    AccessDB db = new AccessDB();

    //    string sql =
    //        "SELECT * " +
    //        "FROM tblEquipment " +
    //        $"WHERE EquipmentSerial = '{serialNumber}'";
    //    DataTable dt = db.FetchDBRecordRequest(fullstatement: sql);
    //    if (dt.Rows.Count > 0) { return true; }
    //    else return false;
    //}
}







