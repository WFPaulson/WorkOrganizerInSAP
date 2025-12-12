namespace ContractWork.MVVM.CustomerDetails;

public interface ICustomerServicePlan {
    int ServicePlanID { get; set; }
    string ServicePlanNumber { get; set; }

}

public partial class CustomerServicePlanModel : ObservableObject, ICustomerServicePlan {

    string[] ServicePlanStatusParams = { "tblServicePlan", "ServicePlanStatus", "ServicePlanID" };

    #region Property statements

    [ObservableProperty]
    private int _servicePlanID;
    

    [ObservableProperty]
    private string _accountName;
    

    [ObservableProperty]
    private string _servicePlanNumber;

    [ObservableProperty]
    private string _servicePlanStatus;
    partial void OnServicePlanStatusChanging(string value) {
        if (update) AccessService.UpdateCustomerAccountDetails<CustomerServicePlanModel>(ServicePlanStatus, this, ServicePlanStatusParams);
    }

    [ObservableProperty]
    private DateTime _startDate;

    [ObservableProperty]
    private string _contractStartDate;
    partial void OnContractStartDateChanged(string value) {
        DateToString(StartDate);
    }
    //public string ContractStartDate {
    //    get => DateToString(StartDate);   //StartDate.ToShortDateString();
    //}

    [ObservableProperty]
    private DateTime _expireDate;

    [ObservableProperty]
    private string _contractExpireDate;
    //public string ContractExpireDate {
    //    get => DateToString(ExpireDate);    //ExpireDate.ToShortDateString();
    //}

    [ObservableProperty]
    private DateTime? _poDate;
    partial void OnPoDateChanged(DateTime? value) {
        if (value == DateTime.MinValue) _poDate = null;
    }
    //partial void OnPoDateChanged(DateTime value) {
    //    if (value == DateTime.MinValue) _poDate = null;
    //}

    [ObservableProperty]
    private string _poExpireDate;
    //public string PoExpireDate {
    //    get => DateToString(PoDate);          //PODate.ToShortDateString();
    //}   

    private string DateToString(DateTime dateformat) {
        if (dateformat != DateTime.MinValue) {
            return dateformat.ToShortDateString();
        }
        else return "";
    }


    [ObservableProperty]
    private bool _poExpired;
    

    [ObservableProperty]
    private bool _expired;

    [ObservableProperty]
    private bool _archive;

    [ObservableProperty]
    private bool _inWork;

    [ObservableProperty]
    private string _notes;

    #endregion

    private bool update = true;

    public CustomerServicePlanModel() {

    }

    //Below notifies with message the status of the contract
    public CustomerServicePlanModel(int servicePlanID) {
        GetServicePlanStatus(servicePlanID);
    }

    private void GetServicePlanStatus(int servicePlanID) {
        AccessService accessDB = new();
        DataTable dt = new DataTable();
        //DataTable dt2 = new DataTable();
        bool answer = true;

        string sqlServicePlan =
            "SELECT [ServicePlanID], [ServicePlanNumber], [ServicePlanStatus], [ServicePlanStartDate], " +
            "[ServicePlanExpireDate], [POExpireDate], [Expired], [POExpired]" +
            "FROM [tblServicePlan] " +
            $"WHERE [ServicePlanID] = {servicePlanID}";

        string sqlUpdateEquipmentStatusPlan =
            "UPDATE tblServicePlan " +
            "INNER JOIN [tblEquipment] ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +          //tblEquipment.ServicePlanID_FK " + {servicePlanID}
            "SET tblEquipment.ServicePlanStatusLU_cbo = [tblServicePlan].[ServicePlanStatus] " +
            $"WHERE tblServicePlan.ServicePlanID = {servicePlanID}";

        dt = accessDB.FetchDBRecordRequest(fullstatement: sqlServicePlan);

        if (dt.Rows[0]["POExpireDate"] != DBNull.Value) {
            answer = (DateTime)dt.Rows[0]["POExpireDate"] > DateTime.Now;
        }

        if (dt.Rows[0]["ServicePlanStatus"].ToString() == "Active"
            || dt.Rows[0]["ServicePlanStatus"].ToString() == "Current") {

            if (((DateTime)dt.Rows[0]["ServicePlanStartDate"] < DateTime.Now) &&
                ((DateTime)dt.Rows[0]["ServicePlanExpireDate"] > DateTime.Now) &&
                (answer == true)) { }

            else if (((DateTime)dt.Rows[0]["ServicePlanExpireDate"] < DateTime.Now) || (answer == false)) {

                string message = $"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} ";

                if (!answer) { message += GL.nl + "PO has expired "; }
                if ((DateTime)dt.Rows[0]["ServicePlanExpireDate"] < DateTime.Now) {
                    message += GL.nl + "Contract has expired";
                }
                update = true;
                ServicePlanStatus = "Expired";
                update = false;
            }
            else MessageBox.Show($"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} is NOT Current or Expired??? ");
        }

        else if (dt.Rows[0]["ServicePlanStatus"].ToString() == "Expired") {
            if ((DateTime)dt.Rows[0]["ServicePlanExpireDate"] < DateTime.Now) {
                accessDB.AddToAccount(SQLInsert: sqlUpdateEquipmentStatusPlan);
            }
            else if (answer == false) {
                MessageBox.Show($"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} PO has expired ");
            }
            else MessageBox.Show($"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} has NOT expired??? ");
        }

        else if (dt.Rows[0]["ServicePlanStatus"].ToString() == "Future Contract") {
            if ((DateTime)dt.Rows[0]["ServicePlanStartDate"] > DateTime.Now) { }
            else if (((DateTime)dt.Rows[0]["ServicePlanStartDate"] < DateTime.Now) && ((DateTime)dt.Rows[0]["ServicePlanExpireDate"] > DateTime.Now)) {
                MessageBox.Show($"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} is NOT a Future Contract, but should be Current {GL.nl} " +
                "Changing status to Current, you will need to Refresh this customer for updated data");
                update = true;
                ServicePlanStatus = "Current";
                update = false;
                accessDB.AddToAccount(SQLInsert: sqlUpdateEquipmentStatusPlan);
            }
            else MessageBox.Show($"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} this is NOT Future or Current, Maybe expired??? ");
        }
    }

    public CustomerServicePlanModel(DataRow servicePlanrow) {
        update = false;
        if (servicePlanrow.Table.Columns.Contains("ServicePlanID")) {
            if (servicePlanrow["ServicePlanID"] != null) {
                ServicePlanID = (int)servicePlanrow["ServicePlanID"];
                GetServicePlanStatus(ServicePlanID);
            }
        }

        if (servicePlanrow.Table.Columns.Contains("AccountName_cbo")) {
            AccountName = servicePlanrow["AccountName_cbo"].ToString() ?? "";
        }
        if (servicePlanrow.Table.Columns.Contains("ServicePlanNumber")) {
            ServicePlanNumber = servicePlanrow["ServicePlanNumber"].ToString() ?? "";
        }
        if (servicePlanrow.Table.Columns.Contains("ServicePlanStatus")) {
            ServicePlanStatus = servicePlanrow["ServicePlanStatus"].ToString() ?? "";
        }
        if (servicePlanrow.Table.Columns.Contains("ServicePlanStartDate")) {
            if (servicePlanrow["ServicePlanStartDate"] != DBNull.Value) {
                StartDate = (DateTime)servicePlanrow["ServicePlanStartDate"];
            }
        }
        if (servicePlanrow.Table.Columns.Contains("ServicePlanExpireDate")) {
            if (servicePlanrow["ServicePlanExpireDate"] != DBNull.Value) {
                ExpireDate = (DateTime)servicePlanrow["ServicePlanExpireDate"];
            }
        }
        if (servicePlanrow.Table.Columns.Contains("POExpireDate")) {
            if (servicePlanrow["POExpireDate"] != DBNull.Value) {
                _poDate = (DateTime)servicePlanrow["POExpireDate"];
            }
        }
        if (servicePlanrow.Table.Columns.Contains("POExpired")) {
            if (servicePlanrow["POExpired"] != null) { PoExpired = Convert.ToBoolean(servicePlanrow["POExpired"]); }
        }
        if (servicePlanrow.Table.Columns.Contains("Expired")) {
            if (servicePlanrow["Expired"] != null) { Expired = Convert.ToBoolean(servicePlanrow["Expired"]); }
        }
        if (servicePlanrow.Table.Columns.Contains("Archive")) {
            if (servicePlanrow["Archive"] != null) { Archive = Convert.ToBoolean(servicePlanrow["Archive"]); }
        }
        if (servicePlanrow.Table.Columns.Contains("InWork")) {
            if (servicePlanrow["InWork"] != null) { InWork = Convert.ToBoolean(servicePlanrow["InWork"]); }
        }
        if (servicePlanrow.Table.Columns.Contains("Notes")) {
            Notes = servicePlanrow["Notes"].ToString() ?? "";
        }

        update = true;
    }

    public void ServicePlanScrubData(DataRow servicePlanrow) {
        //DataRow servicePlanrow = dt.Rows[0];

        if (servicePlanrow["ServicePlanID"] != null) {
            ServicePlanID = (int)servicePlanrow["ServicePlanID"];
        }
        //ServicePlanID = (int)servicePlanrow["ServicePlanID"];
        AccountName = servicePlanrow["AccountName_cbo"].ToString() ?? "";
        ServicePlanNumber = servicePlanrow["ServicePlanNumber"].ToString() ?? "";
        ServicePlanStatus = servicePlanrow["ServicePlanStatus"].ToString() ?? "";

        //StartDate = (DateTime)servicePlanrow["ServicePlanStartDate"];
        if (servicePlanrow["ServicePlanStartDate"] != DBNull.Value) {
            StartDate = (DateTime)servicePlanrow["ServicePlanStartDate"];
        }

        //ExpireDate = (DateTime)servicePlanrow["ServicePlanExpireDate"];
        if (servicePlanrow["ServicePlanExpireDate"] != DBNull.Value) {
            ExpireDate = (DateTime)servicePlanrow["ServicePlanExpireDate"];
        }

        //PODate = (DateTime)servicePlanrow["POExpireDate"];
        if (servicePlanrow["POExpireDate"] != DBNull.Value) {
            PoDate = (DateTime)servicePlanrow["POExpireDate"];
        }

        //var poexpired = Convert.ToBoolean(servicePlanrow["POExpired"]);
        if (servicePlanrow["POExpired"] != null) { PoExpired = Convert.ToBoolean(servicePlanrow["POExpired"]); }

        //var expired = Convert.ToBoolean(servicePlanrow["Expired"]);
        if (servicePlanrow["Expired"] != null) { Expired = Convert.ToBoolean(servicePlanrow["Expired"]); }

        //var archive = Convert.ToBoolean(servicePlanrow["Archive"]);
        if (servicePlanrow["Archive"] != null) { Archive = Convert.ToBoolean(servicePlanrow["Archive"]); }

        //var inwork = Convert.ToBoolean(servicePlanrow["InWork"]);
        if (servicePlanrow["InWork"] != null) { InWork = Convert.ToBoolean(servicePlanrow["InWork"]); }

        Notes = servicePlanrow["Notes"].ToString() ?? "";

    }

    //public static bool DoesThisServicePlanExist<T>( T servicePlan)
    //{
    //    AccessDB accessDB = new AccessDB();
    //    string sqlstatement = string.Empty;

    //    var servicePlanType = servicePlan.GetType();

    //    if (servicePlanType == typeof(string)) {
    //        sqlstatement =
    //            "SELECT * " +
    //            "FROM tblServicePlan " +
    //            $"WHERE ServicePlanNumber = '{servicePlan}'";
    //    }
    //    else if (servicePlanType == typeof(int)) {
    //        sqlstatement =
    //            "SELECT * " +
    //            "FROM tblServicePlan " +
    //            $"WHERE ServicePlanID = {servicePlan}";
    //    }
    //    else { MessageBox.Show("its neither Service plan number or ID, something when wrong???"); }

    //    DataTable dt = accessDB.FetchDBRecordRequest(fullstatement: sqlstatement);

    //    if (dt == null || dt.Rows.Count <= 0) { return false; }
    //    else { return true; }
    //}



    public static ObservableCollection<string> GetContractList(string accountName) {
            AccessService accessDB = new();
            ObservableCollection<string> contractList = new ObservableCollection<string>();


            string sqlstatement =
                "SELECT * " +
                "FROM tblServicePlan " +
                $"WHERE AccountName_cbo = {accountName} ";

            DataTable dt = accessDB.FetchDBRecordRequest(fullstatement: sqlstatement);

            foreach (DataRow row in dt.Rows) {
                contractList.Add(row["ServicePlanNumber"].ToString());
            }

            return contractList;
    }


    [RelayCommand]
    private void ClipboardCopy(string stringToClipboard) {
        stringToClipboard.CopytoClipboard();
    }


}






