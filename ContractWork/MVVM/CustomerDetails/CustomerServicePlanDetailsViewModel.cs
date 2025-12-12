using SQL = ContractWork.MVVM.CustomerDetails.CustomerDetailsSQLStatements;
namespace ContractWork.MVVM.CustomerDetails;

public partial class CustomerServicePlanDetailsViewModel : ObservableObject {

    AccessService accessDB;

    #region Property statements
    [ObservableProperty]
    private List<string> _planListcmb;

    [ObservableProperty]
    private string _plansStatus;
    
    [ObservableProperty]
    private bool _unlock = false;
    
    [ObservableProperty]
    private string _unlockOrLockText = "Unlock";
    
    [ObservableProperty]
    private CustomerServicePlanModel _currentServicePlan;
    
    [ObservableProperty]
    private CustomerEquipmentModel _currentEquipment;
    
    [ObservableProperty]
    private ObservableCollection<ServicePlanDetailsModel> _currentEquipmentList;
    
    [ObservableProperty]
    private CustomerServicePlanModel _currentCustomer;
    
    [ObservableProperty]
    private bool _equipmentArchived;

    #endregion

    private int planID;


    //public string nameOfSendingMethod { get; set; }
    public NavigationService _navigationService { get; set; }

    public CustomerServicePlanDetailsViewModel(NavigationService navigationService, int currentServicePlan) {
       // nameOfSendingMethod = new StackFrame(1).GetMethod().Name;
        _navigationService = navigationService;
       // CurrentCustomer = new(currentServicePlan);
       planID = currentServicePlan;

        accessDB = new();
        CurrentServicePlan = new();
        CurrentEquipmentList = new();

        GetCurrentCSPModel();
        GetCustomerServicePlanDetails();
        //GetServicePlanEquipmentList();
    }

    private void GetCurrentCSPModel() {
        DataTable dt = new ();
        //CurrentCustomer = new();
        string sqlGetServicePlan =
            "SELECT [ServicePlanID], [AccountName_cbo], [ServicePlanNumber], [ServicePlanStatus], [ServicePlanStartDate], " +
            "[ServicePlanExpireDate], [POExpireDate], [Expired], [POExpired]" +
            "FROM [tblServicePlan] " +
            $"WHERE [ServicePlanID] = {planID}";

        dt = accessDB.FetchDBRecordRequest(sqlGetServicePlan);
        CurrentCustomer = new CustomerServicePlanModel(dt.Rows[0]);

    }

    [RelayCommand]
    private void SerialNumberArchive() {
        MessageBox.Show($"need to archive serial number value {EquipmentArchived}");
    }

    [RelayCommand]
    private void ServicePlanArchive(bool value) {
        AccessService db = new();

        //cant archive if equipment is on contract
        //need to add a test for existing equipment on contract if archiving

        string sqlArchiveServiceplan =
            "UPDATE tblServicePlan " +
            $"SET Archive = {value} " +
            $"WHERE ServicePlanID = {CurrentServicePlan.ServicePlanID}";

        db.AddToAccount(SQLInsert: sqlArchiveServiceplan);
    }

    [RelayCommand]
    private void UnlockStatus() {
        if (UnlockOrLockText == "Unlock") {
            UnlockOrLockText = "Lock";
            Unlock = true;
        }
        else {
            UnlockOrLockText = "Unlock";
            Unlock = false;
        }
    }

    private void GetCustomerServicePlanDetails() {
        AccessService accessDB = new();
        ServicePlanDetailsModel csp = new ServicePlanDetailsModel();
        MessageBoxResult answer = MessageBoxResult.Yes;

        PlanListcmb = GV._statusList;

        if (CurrentCustomer.ServicePlanID != 0){
            CurrentServicePlan.ServicePlanID = CurrentCustomer.ServicePlanID;
        }
        else{
            CurrentServicePlan.ServicePlanID = CurrentCustomer.ServicePlanNumber.getServicePlanInfo();
        }
        //_ = accessDB.GetServicePlanStatus(currentServicePlan.ServicePlanID, out string status);
        //string status = currentServicePlan.ServicePlanID.GetServicePlanStatus();
        string status = CurrentServicePlan.ServicePlanID.GetServicePlanStatus();

        //MessageBox.Show($"the contract status is {status}");

        if (CurrentServicePlan.ServicePlanID == 0) {
            //MessageBoxResult answer;

            MessageBox.Show($"This Service Plan: {CurrentCustomer.ServicePlanNumber} does NOT exist");

            answer = MessageBox.Show($"This Service Plan: {CurrentCustomer.ServicePlanNumber} does NOT exist" + GL.nl + 
                "Do you want to create this Service Plan?", "?Continue?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No) { return; }

            //ServicePlanEntryWindow ServicePlanEntry = new ServicePlanEntryWindow() {
            //    DataContext = _navigationService.CurrentViewModel = new ServicePlanEntryViewModel(_navigationService, CL_XLcustomerAccount, dtEquipment)
            //};
            //ServicePlanEntry.ShowDialog();

            return;
        }

        if (CurrentServicePlan.ServicePlanID.isContractArchived()) {
            MessageBox.Show($"This Service Plan: {CurrentCustomer.ServicePlanNumber} " + GL.nl +
                                "is already Archived!");
            CurrentCustomer.ServicePlanID = CurrentServicePlan.ServicePlanID;
            CurrentServicePlan = CurrentCustomer;

            return;
        }

        string sqlstatement =
        "SELECT tblServicePlan.ServicePlanID, tblServicePlan.Archive, tblServicePlan.ServicePlanNumber, " +
        "tblServicePlan.ServicePlanStatus, tblServicePlan.AccountName_cbo, " +
        "tblServicePlan.ServicePlanStartDate, tblServicePlan.ServicePlanExpireDate, tblServicePlan.POExpireDate, " +
        "tblEquipment.EquipmentSerial, tblEquipment.ModelID, tblEquipment.VersionID, tblEquipment.Archive " +
        "FROM tblServicePlan INNER JOIN tblEquipment ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +
        $"WHERE tblServicePlan.ServicePlanID =  {CurrentServicePlan.ServicePlanID}";

        DataTable dt = accessDB.FetchDBRecordRequest(fullstatement: sqlstatement);

        //if (!nameOfSendingMethod.ToLower().Contains("serviceplandetailspopup")) {

        if (dt.Rows.Count <= 0) {
            //MessageBoxResult answer;

            answer = MessageBox.Show($"This Service Plan is active but no equipment " + GL.nl +
                                "Do you want to add equipment to this Service Plan?", "?Continue?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (answer == MessageBoxResult.No) {
                sqlstatement =
                    "SELECT ServicePlanID, Archive, ServicePlanNumber, " +
                    "ServicePlanStatus, AccountName_cbo, " +
                    "ServicePlanStartDate, ServicePlanExpireDate, POExpireDate " +
                    "FROM tblServicePlan " +
                    $"WHERE ServicePlanID =  {CurrentServicePlan.ServicePlanID}";

                dt = accessDB.FetchDBRecordRequest(fullstatement: sqlstatement);
                    

                 
            }
            else { 
                dt = new DataTable();
                dt = accessDB.FetchDBRecordRequest(fullstatement: sqlstatement);
            }
        }

        var values = dt.Rows[0];

        CurrentServicePlan.Archive = Convert.ToBoolean(values[1]);
        CurrentServicePlan.ServicePlanNumber = values[2].ToString();
        CurrentServicePlan.ServicePlanStatus = values[3].ToString();
        CurrentServicePlan.AccountName = values[4].ToString();
        CurrentServicePlan.StartDate = (DateTime)values[5];
        CurrentServicePlan.ExpireDate = (DateTime)values[6];

        if (values[7] != DBNull.Value) {
            CurrentServicePlan.PoDate = (DateTime)values[7];
        }

        if (answer == MessageBoxResult.Yes) {

            foreach (DataRow row in dt.Rows) {
                csp = new ServicePlanDetailsModel {
                    AccountName = CurrentServicePlan.AccountName,
                    SerialNumber = row["EquipmentSerial"].ToString(),
                    ModelID = (int)row["ModelID"],
                    VersionID = (int)row["VersionID"],
                    EquipmentArchived = Convert.ToBoolean(row["tblEquipment.Archive"])
                };
                csp.Model = GV._modelLookUP[csp.ModelID];
                csp.Version = GV._versionLookUP[csp.VersionID];
                CurrentEquipmentList.Add(csp);
            }
        }
    }

    private void GetServicePlanEquipmentList() {
        DataTable dt = new DataTable();
        CustomerEquipmentModel ce;
        
        string sqlstatement = SQL.CustomerServicePlanEquipmentList(CurrentCustomer.ServicePlanID);
        
        dt = accessDB.FetchDBRecordRequest(fullstatement: sqlstatement);
        foreach (DataRow row in dt.Rows) {
            ce = new() {
                //AccountName = row["AccountName_cbo"].ToString() ?? "",
                //ServicePlanNumber = row["ServicePlanNumber"].ToString() ?? "",
                //ServicePlanStatus = row["ServicePlanStatusLU_cbo"].ToString() ?? "",
                SerialNumber = row["EquipmentSerial"].ToString() ?? "",
                ModelID = (int)row["ModelID"],
                VersionID = (int)row["VersionID"],
                //_pmCompleted = (DateTime)row["PMCompleted"],
                //pmMonthName = row["PMMonth"].ToString() ?? "",
                EquipmentArchived = Convert.ToBoolean(row["Archive"])

            };
            ce.ModelNumber = GV._modelLookUP[ce.ModelID];
            ce.VersionNumber = GV._versionLookUP[ce.VersionID];
            //CurrentEquipmentList.Add(ce);
        }
    }
}

public partial class ServicePlanDetailsModel : ObservableObject{
    
    public int ServicePlanID { get; set; }
    public string AccountName { get; set; }
    public string SerialNumber { get; set; }

    //[ObservableProperty]
    //private bool _equipmentArchived;

    


    public int ModelID { get; set; }
        
    [ObservableProperty]
    private string _model;
    partial void OnModelChanging(string value) {
        ModelID = GV._reversemodelLookUP[value];
    }
    partial void OnModelChanged(string value) {
        _model = GV._modelLookUP[ModelID];
    }

    public int VersionID { get; set; }

    [ObservableProperty]
    private string _version;
    partial void OnVersionChanging(string value) {
        VersionID = GV._reverseVersionLookUP[value];
    }
    partial void OnVersionChanged(string value) {
        _version = GV._versionLookUP[VersionID];
    }


    [ObservableProperty]
    private bool _equipmentArchived;
    partial void OnEquipmentArchivedChanged(bool value)
    {
       _equipmentArchived = value;
        AccessService accessDB = new();
        string updateSQL =
            $"UPDATE [tblEquipment] " +
            $"SET [Archive] = {value} " +
            $"WHERE [tblEquipment.EquipmentSerial] = '{SerialNumber}'";
        accessDB.AddToAccount(SQLInsert: updateSQL);

        //TODO: Move serial Number Account to No Contract 11111111

    }

}