//using static System.Net.Mime.MediaTypeNames;
using SQL = ContractWork.MVVM.CustomerDetails.CustomerDetailsSQLStatements;


namespace ContractWork.MVVM.CustomerDetails;


public interface ICustomerAccount {
    string AccountName { get; set; }
    int CustomerAccountID { get; set; }
    bool AccountArchived { get; set; }

}

public interface ICustomerBilling {
    int MainAccountNumber { get; set; }
    int? BillingAccountNumber { get; set; }
    int? ShippingAccountNumber { get; set; }
}

//int ICustomerBilling.MainAccountNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//int? ICustomerBilling.BillingAccountNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//int? ICustomerBilling.ShippingAccountNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
public partial class CustomerAccountModel : ObservableObject, ICustomerAccount, ICustomerBilling {

    public static List<string> AccountNameList; // { get; set; }
    public static List<string> SalesRepsList; //{ get; set; }


    public static Dictionary<int, string> GetAccountNameList() {        //public static Dictionary<int, string> GetAccountNameList() {
        AccessService accessDB = new();
        Dictionary<int, string> dictAccountNames = new();

        string sqlAccountNameList =
            "SELECT [AccountName], [CustomerAccountID] " +
            "FROM [tblCustomerAccounts] " +
            "WHERE [Archive] = 0 " +
            "ORDER BY AccountName";

        DataTable dt2 = accessDB.FetchDBRecordRequest(fullstatement: sqlAccountNameList);

        dictAccountNames = dt2.AsEnumerable()
      .ToDictionary<DataRow, int, string>(row => row.Field<int>(1),
                                row => row.Field<string>(0));

        return dictAccountNames;

    }

    public static void GetSalesRepsList() {
        AccessService db = new();

        string sqlGetSalesRepsList =
            "SELECT AccountRepName " +
            "FROM tblAccountRep_LU " +
            "WHERE AccountRepTypeID_LU = 2";
        DataTable dt = db.FetchDBRecordRequest(fullstatement: sqlGetSalesRepsList);

        SalesRepsList = dt.AsEnumerable()
                .Select(x => x["AccountRepName"].ToString()).ToList();

    }




    string[] notesParams = { "tblCustomerAccounts", "Notes", "CustomerAccountID" };

    AccessService accessDB;

    #region Interface properties
    [ObservableProperty]
    private string _accountName = string.Empty;
   
    [ObservableProperty]
    private int _customerAccountID;

    #endregion

    #region Property statements
    [ObservableProperty]
    private string _mainAccount;
    [ObservableProperty]
    private int _mainAccountNumber;
    partial void OnMainAccountNumberChanging(int value) {
        MainAccount = value.ToString();
    }

    [ObservableProperty]
    private string? _billingAccount;
    [ObservableProperty]
    private int? _billingAccountNumber;
    //partial void OnBillingAccountNumberChanging(int? value) {

    //}

    //TODO: due comparison between old and new to decide to update access billing account
    partial void OnBillingAccountNumberChanging(int? oldValue, int? newValue) {
        BillingAccount = newValue.ToString();
    }
   
    [ObservableProperty]
    private string? _shippingAccount;
    [ObservableProperty]
    private int? _shippingAccountNumber;
        //partial void OnShippingAccountNumberChanging(int? value) {

        //}
    //}
    //TODO: due comparison between old and new to decide to update access shipping account
    partial void OnShippingAccountNumberChanging(int? oldValue, int? newValue) {
        ShippingAccount = newValue.ToString();
    }

    [ObservableProperty]
    private string _serviceRep = "William Paulson";
    
    [ObservableProperty]
    private string _salesRep;

    [ObservableProperty]
    private bool _accountArchived;
    //partial void OnAccountArchivedChanged(bool value) {
    //    string updateAccountArchive =
    //        "SELECT ";
    //}

    [ObservableProperty]
    private string _oldMainAccount;
    [ObservableProperty]
    private int? _oldMainAccountNumber;
    partial void OnOldMainAccountNumberChanging(int? value) {
        OldMainAccount = value.ToString();
    }

    [ObservableProperty]
    private string _notes;
    

    //private string _initialValue;
    //public string InitValue {
    //    get => _initialValue;
    //    set {
    //        if (_initialValue != value) {
    //            _initialValue = value;
    //            OnPropertyChanged(nameof(InitValue));
    //        }
    //    }
    //}
    public string InitValue { get; set; }


    #endregion

    

    #region ctor
    public CustomerAccountModel() {
        accessDB = new();
    }

    //[RelayCommand]
    //private void UpdateAccountArchived(bool value) {

    //    AccountArchived = value;
    //}

    [RelayCommand]
    private void Undo() {
        Notes = InitValue;
    }

    [RelayCommand]
    private void LostFocusCompareValue() {
        AccessService.UpdateCustomerAccountDetails<CustomerAccountModel>(Notes, this, notesParams);
    }

    [RelayCommand]
    private void GotFocusInitialValue() {
        //InitValue = string.Empty;
        InitValue = Notes;
    }

    #endregion

    public bool Update { get; set; }
    

    public void tblCustomerAccountData(DataRow accountrow) {
        Update = false;

        AccountName = accountrow["AccountName"].ToString() ?? "";

        if (accountrow["CustomerAccountID"] != null) { CustomerAccountID = (int)accountrow["CustomerAccountID"]; }

        MainAccountNumber = (int)accountrow["MainAccount_PK"];

        BillingAccountNumber = accountrow["BillingAccount"] != DBNull.Value ? (int)accountrow["BillingAccount"] : (int?)null;
        ShippingAccountNumber = accountrow["ShippingAccount"] != DBNull.Value ? (int)accountrow["ShippingAccount"] : (int?)null;

        ServiceRep = accountrow["ProCareRepLU_cbo"].ToString() ?? "";
        SalesRep = accountrow["SalesRepLU_cbo"].ToString() ?? "";

        if (accountrow["Archive"] != null) { AccountArchived = Convert.ToBoolean(accountrow["Archive"]); }

        //OldMainAccountNumber = accountrow["OldMainAccount"] != DBNull.Value ? (int)accountrow["OldMainAccount"] : (int?)null;

        Notes = accountrow["Notes"].ToString() ?? "";
        InitValue = string.Empty;
        InitValue = Notes;

        Update = true;
    }

    public (ObservableCollection<CustomerEquipmentModel>, List<string>) GetCustomerEquipmentList(DataTable dtable) {
        AccessService accessDB = new();

        ObservableCollection<CustomerEquipmentModel> Equipment = new ObservableCollection<CustomerEquipmentModel>();
        List<string> cboContractList = new List<string>();

        if (dtable != null && dtable.Rows.Count > 0) {

            var columnName = dtable.Columns.Contains("AccountName") ? "AccountName"
                : dtable.Columns.Contains("AccountName_cbo") ? "AccountName_cbo" : null;

            for (int i = 0; i < dtable.Rows.Count; i++) {
                AccountName = dtable.Rows[i][columnName].ToString();
                if (!string.IsNullOrEmpty(AccountName)) { break; }
            } //  why looping Accountname - is a string not a array

            columnName = dtable.Columns.Contains("CustomerAccountID") ? "CustomerAccountID"
                : dtable.Columns.Contains("CustomerAccountID_FK") ? "CustomerAccountID_FK" : null;

            int _customerAccountID = (int)dtable.Rows[0][columnName];
            DataTable dt = accessDB.FetchDBRecordRequest(fullstatement: SQL.sqlServicePlanList(AccountName));

            cboContractList = dt.AsEnumerable()
                .Select(x => x["ServicePlanNumber"].ToString()).ToList();

            foreach (DataRow row in dtable.Rows) {
                Equipment.Add(new CustomerEquipmentModel(row));
            }
        }
        return (Equipment, cboContractList);
    }
}