using System.Runtime.CompilerServices;

namespace ContractWork.MVVM.CustomerDetails;
public partial class CustomerAccountSelectionViewModel : ObservableObject {

    AccessService accessDB;

    #region Const statements
    const string Lock = "Lock";
    const string Unlock = "Unlock";
    const bool Locked = false; // originally true
    const bool Unlocked = true; // originally false

    #endregion

    #region Observable Class calls
    [ObservableProperty]
    private ObservableCollection<CustomerAccountModel> _customerList;

    [ObservableProperty]
    private ObservableCollection<CustomerAccountModel> _tmpCustomerList;    //string

    [ObservableProperty]
    private CustomerAccountModel _customerAccount;
    
    [ObservableProperty]
    private CustomerAccountModel _selectedCustomer;

    [ObservableProperty]
    private CustomerAccountModel _account;

    #endregion

    #region Observable properties
    [ObservableProperty]
    private string _lockOrUnlockText = Unlock;

    [ObservableProperty]
    private bool _chkbxLockedOrUnlocked;

    [ObservableProperty]
    private bool _lockedOrUnlocked = Locked;

    [ObservableProperty]
    private bool _isCustomerListEnabled = true;

    [ObservableProperty]
    private string _searchText;

    [ObservableProperty]
    private bool _miniMeSelected = false;

    [ObservableProperty]
    private bool? _isCheckedViewArchived = false;

    [ObservableProperty]
    private string _viewingby = "Viewing Current";

    #endregion


    public string _customerAccountName { get; set; }
    public int _customerAccountID { get; set; }
    public string _selectedServicePlan { get; set; }
    

    public List<string> listOfSalesReps { get; set; }
    

    public NavigationService _navigationService { get; set; }

    public CustomerAccountSelectionViewModel(NavigationService navigationService, CustomerAccountModel customerAccount,
                                        [CallerArgumentExpression("customerAccount")] string nameofCustomerAccount = null) {
        _navigationService = navigationService;

        CustomerAccount = new();
        Account = new();
        accessDB = new();
        CustomerList = new();
        TmpCustomerList = new();

        CustomerAccountModel.GetSalesRepsList();
        listOfSalesReps = CustomerAccountModel.SalesRepsList;

        string tmp = nameofCustomerAccount.ToLower();
        if (tmp.Contains("update") || tmp.Contains("insert")) {
            IsCustomerListEnabled = false;
        }
        else IsCustomerListEnabled = true;

        if (customerAccount != null) { SelectedCustomer = customerAccount; }
            
        GetList();

    }

    private bool IsUnlockDisabled() => LockedOrUnlocked;

    private bool IsUpdateDisabled() => !LockedOrUnlocked;

    [RelayCommand(CanExecute = nameof(IsUpdateDisabled))]
    private void Undo() {
        MessageBox.Show("undo clicked");
    }

    [RelayCommand(CanExecute = nameof(IsUpdateDisabled))]
    private void UpdateDatabase() {
        MessageBox.Show("Update Account");
        string sqlUpdate =
            "UPDATE tblCustomerAccounts " +
            $"SET BillingAccount = {SelectedCustomer.BillingAccountNumber}, " +
            $"ShippingAccount = {SelectedCustomer.ShippingAccountNumber}, " +
            $"SalesRepLU_cbo = '{SelectedCustomer.SalesRep}', " +
            $"Archive = {SelectedCustomer.AccountArchived} " +
            $"WHERE MainAccount_PK = {SelectedCustomer.MainAccountNumber}";

        accessDB.AddToAccount(SQLInsert: sqlUpdate);

        MessageBox.Show("Update completed");
    }

    [RelayCommand]
    private void CustomerSearch() {
        CustomerList.Clear();

        foreach (CustomerAccountModel customer in TmpCustomerList) {
            if (customer.AccountName.ToLower().Contains(SearchText)) {
                CustomerList.Add(new CustomerAccountModel() { AccountName = customer.AccountName, CustomerAccountID = customer.CustomerAccountID });
            }
        }
        CurrentCustomerPicked();
    }

    [RelayCommand]
    private void LockOrUnlock() {
        if (LockedOrUnlocked == Unlocked) { LockedOrUnlocked = Locked; LockOrUnlockText = Unlock; }
        else { LockedOrUnlocked = Unlocked; LockOrUnlockText = Lock; }
    }

    [RelayCommand]
    private void OpenCustomerAccountDetails() {

        if (MiniMeSelected) {
            CustomerAcctDetailsMiniWindow CustomerAccountDetails = new CustomerAcctDetailsMiniWindow() {
                DataContext = _navigationService.CurrentViewModel =
                    new CustomerAccountDetailsViewModel(_navigationService, SelectedCustomer)
            };
            CustomerAccountDetails.Show();

        }
        else { 
            CustomerAccountDetailsWindow CustomerAccountDetails = new CustomerAccountDetailsWindow() {
                DataContext = _navigationService.CurrentViewModel =
                    new CustomerAccountDetailsViewModel(_navigationService, SelectedCustomer)
            };
            CustomerAccountDetails.Show();
        }
    }

    [RelayCommand]
    private void CurrentCustomerPicked() {
        DataTable dbTable = new DataTable();
            
        int customer;

        if (SelectedCustomer != null) { customer = SelectedCustomer.CustomerAccountID; }
            
        else if (_customerAccountID != null) {
            customer = _customerAccountID;
            SelectedCustomer = new();
        }
            
        else return;

        string currentCustomerSQL = "SELECT * " +
            "FROM [tblCustomerAccounts] " +
            $"WHERE [CustomerAccountID] = {customer}";

        dbTable = accessDB.FetchDBRecordRequest(currentCustomerSQL);

        Account.tblCustomerAccountData(dbTable.Rows[0]);

        SelectedCustomer.tblCustomerAccountData(dbTable.Rows[0]);
    }

    [RelayCommand]
    private void IncludeArchivedServicePlans() {
        GetList();
    }

    private void GetList(CustomerAccountModel customerAccount = null) {
        DataTable dbTable = new DataTable();
        CustomerList.Clear();
        TmpCustomerList.Clear();
        accessDB.ViewArchived = AccessService.ViewCurrent;
        //int? viewArchived;

        string getListSQL = "SELECT [AccountName], [CustomerAccountID] " +
            "FROM [tblCustomerAccounts] ";

        if (IsCheckedViewArchived == false) {
            getListSQL += "WHERE [Archive] = 0 ";
            Viewingby = "Viewing Current";
        }
        else if (IsCheckedViewArchived == true) {
            getListSQL += "WHERE [Archive] = -1 ";
            Viewingby = "Viewing Archived";
        }
         
        else Viewingby = "Viewing All";

        getListSQL += "ORDER BY [AccountName]";

        dbTable = accessDB.FetchDBRecordRequest(getListSQL);

        foreach (DataRow row in dbTable.Rows) {
            CustomerList.Add(new() { AccountName = row["AccountName"].ToString(), CustomerAccountID = (int)row["CustomerAccountID"] });
            TmpCustomerList.Add(new() { AccountName = row["AccountName"].ToString(), CustomerAccountID = (int)row["CustomerAccountID"] });
        }

        if (SelectedCustomer == null) {
            SelectedCustomer = new();
            SelectedCustomer = CustomerList[0];
            _customerAccountID = SelectedCustomer.CustomerAccountID;
        }

        CurrentCustomerPicked();
    }


    [RelayCommand]
    private void UpdateAccountArchived(bool value) {
        string updateAccountArchive =
            "UPDATE [tblCustomerAccounts] " +
            $"SET [Archive] = {value} " +
            $"WHERE [MainAccount_PK] = {SelectedCustomer.MainAccountNumber}";

        accessDB.AddToAccount(updateAccountArchive);
    }
}

//namespace System.Runtime.CompilerServices {
//    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
//    internal sealed class CallerArgumentExpressionAttribute : Attribute {
//        public CallerArgumentExpressionAttribute(string parameterName) {
//            ParameterName = parameterName;
//        }

//        public string ParameterName { get; }
//    }
//}
