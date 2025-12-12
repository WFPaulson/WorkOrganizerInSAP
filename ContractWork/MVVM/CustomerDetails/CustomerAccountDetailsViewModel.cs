using System.Reflection.Metadata;
using static ContractWork.MVVM.Popups.NewInventoryItemPopup;
using SQL = ContractWork.MVVM.CustomerDetails.CustomerDetailsSQLStatements;

namespace ContractWork.MVVM.CustomerDetails; 
public partial class CustomerAccountDetailsViewModel : ObservableObject {
   
    AccessService accessDB;

    #region Observable properties
    [ObservableProperty]
    private ObservableCollection<CustomerEquipmentModel> _customerEquipmentList;

    [ObservableProperty]
    private ObservableCollection<CustomerServicePlanModel> _customerServicePlanList;

    [ObservableProperty]
    private CustomerAccountModel _currentCustomer;

    [ObservableProperty]
    private ObservableCollection<CustomerAccountModel> _customerList;

    [ObservableProperty]
    private ObservableCollection<CustomerAccountModel> _tmpCustomerList;

    [ObservableProperty]
    private CustomerAccountModel _selectedCustomer;
    partial void OnSelectedCustomerChanged(CustomerAccountModel value) {
        _selectedPmMonthDue = "";
    }

    [ObservableProperty]
    private CustomerAccountModel _customerAccount;

    [ObservableProperty]
    private CustomerEquipmentModel _customersEquipment;

    [ObservableProperty]
    private CustomerServicePlanModel _selectedServicePlan;

    [ObservableProperty]
    private string _searchText;
    
    [ObservableProperty]    
    private bool? _isCheckedCustomerArchived = false;

    [ObservableProperty]
    private bool? _isCheckedEquipmentArchived = false;

    [ObservableProperty]
    private bool? _isCheckedServicePlanArchived = false;

    [ObservableProperty]
    private string _customerViewingby = "Viewing Current";

    [ObservableProperty]
    private string _servicePlanViewingby = "Viewing Current";

    [ObservableProperty]
    private string _equipmentViewingby = "Viewing Current";

    [ObservableProperty]
    private List<string> _cboContractList;
    
    //TODO: need to add a get statement which is _customerEquipmentList.FirstOrDefault().PmMonthDue

    [ObservableProperty]
    private bool _allSelected;

    [ObservableProperty]
    private string _selectedPmMonthDue;     // = "";


    #endregion

    public string _customerAccountName { get; set; }

    public List<string> listOfSalesReps { get; set; }

    public NavigationService _navigationService { get; set; }

    public CustomerAccountDetailsViewModel(NavigationService navigationService, CustomerAccountModel currentCustomer) {
        
        _navigationService = navigationService;
        SelectedCustomer = currentCustomer;
        listOfSalesReps = CustomerAccountModel.SalesRepsList;
        accessDB = new();
        CustomerEquipmentList = new();
        CustomerServicePlanList = new();

        CustomerList = new();
        TmpCustomerList = new();

        GetCustomerList();
    }


    [RelayCommand]
    public void SetPMMonthChanged(string dateChanged) {
        SelectedPmMonthDue = dateChanged;
        if (AllSelected) {
            SetAllPMDueToSame();
        }

    }

    [RelayCommand]
    public void SetAllPMDueToSame() {
        foreach (var item in CustomerEquipmentList) {
            item.PmMonthDue = SelectedPmMonthDue;
        }
        AllSelected = false;
    }

    [RelayCommand]
    public void Refresh() {
        ContractAndAssetsViewModel.UpdateExpireStatus();
        CurrentCustomerPicked();
    }

    [RelayCommand]
    private void IncludeArchivedServicePlan() {
        GetCustomerServicePlanList();
    }

    [RelayCommand]
    private void IncludeArchivedCustomer() {
        GetCustomerList();
    }

    [RelayCommand]
    private void IncludeArchivedEquipment() {
        GetCustomerEquipmentList();
    }

    [RelayCommand]
    private void ServicePlanDetailsPopup() {
        
        int selectedPlanNumber = SelectedServicePlan.ServicePlanNumber.GetServicePlanID();
        CustomerServicePlanDetailsWindow serviceplandetailsWindow = new() {
            DataContext = _navigationService.CurrentViewModel = 
                new CustomerServicePlanDetailsViewModel(_navigationService, selectedPlanNumber)
        };
        serviceplandetailsWindow.ShowDialog();
    }

    private void GetCustomerEquipmentList(int id = 0) {
        CboContractList = new();
        int _customerID = 0;

        CustomerEquipmentList.Clear();

        if (SelectedCustomer.CustomerAccountID != 0) {
            _customerID = SelectedCustomer.CustomerAccountID;
        }
        else if (SelectedCustomer.CustomerAccountID == 0) {
            if (SelectedCustomer.MainAccountNumber != 0) {
                _customerID = SelectedCustomer.MainAccountNumber.getMainAccountOrCustID();
            }
            else if (CustomerAccount.MainAccountNumber != 0) {
                _customerID = CustomerAccount.MainAccountNumber.getMainAccountOrCustID();
            }
        }
        else {
            MessageBox.Show("ID FAILs ???");
            return;
        }

        string sqlJoinStatement = SQL.CustomerAccountEquipmentList(_customerID);

        if (IsCheckedEquipmentArchived == true) {
            sqlJoinStatement += $"AND (tblEquipment.[Archive]) = -1 ";
            EquipmentViewingby = "Viewing Archived";
        }
        //false is current
        else if (IsCheckedEquipmentArchived == false) {
            sqlJoinStatement += $"AND (tblEquipment.[Archive]) = 0 ";
            EquipmentViewingby = "Viewing Current";
        } 
        // show all
        else { EquipmentViewingby = "Viewing All"; }

        //ToDo: check this list

        DataTable equipmentList = accessDB.FetchDBRecordRequest(sqlJoinStatement);

        (CustomerEquipmentList, CboContractList) = SelectedCustomer.GetCustomerEquipmentList(equipmentList);
    }

    private void GetCustomerServicePlanList() {
        CustomerServicePlanList.Clear();
        int _customerID = SelectedCustomer.CustomerAccountID;
        int _mainAccount = _customerID.getShippingAccountNumberFromCustID();

        //need to change accountname to custid or main account #
        string sqlJoinStatement =
            "SELECT DISTINCT ServicePlanNumber, ServicePlanStatus, ServicePlanStartDate, ServicePlanExpireDate, " +
            "Expired " +
            "FROM tblServicePlan " +    
            $"WHERE tblServicePlan.[ShipToAccountNumber] = {_mainAccount} ";      //(tblEquipment.[CustomerAccountID_FK]) = {_customerID} ";


        //true is archived only
        if (IsCheckedServicePlanArchived == true) { sqlJoinStatement += $"AND (tblServicePlan.[Archive]) = -1 ";
            ServicePlanViewingby = "Viewing Archived"; 
        }
        //false is current
        else if (IsCheckedServicePlanArchived == false) { sqlJoinStatement += $"AND (tblServicePlan.[Archive]) = 0 ";
            ServicePlanViewingby = "Viewing Current"; 
        }
        // show all
        else { ServicePlanViewingby = "Viewing All"; }

        sqlJoinStatement += "ORDER BY tblServicePlan.ServicePlanStatus, tblServicePlan.ServicePlanExpireDate, " +
                            "tblServicePlan.ServicePlanNumber";


       

        DataTable servicePlanList = accessDB.FetchDBRecordRequest(sqlJoinStatement);
        foreach (DataRow row in servicePlanList.Rows) {
            CustomerServicePlanList.Add(new CustomerServicePlanModel(row));
        }

    }

    [RelayCommand]
    private void CustomerSearch() {
        CustomerList.Clear();

        if (SearchText == null || SearchText == "") {
            //CustomerList.Clear();
            foreach (CustomerAccountModel customer in TmpCustomerList) {
                CustomerList.Add(new() { AccountName = customer.AccountName  });
            }
            return;
        }
        //CustomerList.Clear();
        //originally there was no else but I added it cause I dont understand why it was necessary
        //to run it twice, once above and again this
        else {
            foreach (CustomerAccountModel customer in TmpCustomerList) {
                if (customer.AccountName.ToLower().Contains(SearchText)) {
                    CustomerList.Add(new() { AccountName = customer.AccountName });
                }
            }
        }
    }

    [RelayCommand]
    private void CurrentCustomerPicked() {
        string customer = string.Empty;
        int customerID;

        string currentCustomerSQL = "SELECT * " +
            "FROM [tblCustomerAccounts] " +
            $"WHERE [CustomerAccountID] = ";

       //if think i need selected customer put in sql


        if (SelectedCustomer != null || (SearchText != null && SearchText != "")) {
            
            CustomerAccount = new();
            //this is constantly obj ref Not set to an instance of obj
            customer = SelectedCustomer.AccountName;
            customerID = SelectedCustomer.CustomerAccountID;
            currentCustomerSQL += $"{customerID}";
            DataTable dbTable = accessDB.FetchDBRecordRequest(currentCustomerSQL);
           
            CustomerAccount.tblCustomerAccountData(dbTable.Rows[0]);
            GetCustomerEquipmentList(CustomerAccount.CustomerAccountID);
           // MessageBox.Show("leaving equip list, going into service plan list");
            GetCustomerServicePlanList();
            //SearchText = "";
           // MessageBox.Show("leaving");
        }
    }

    private void GetCustomerList() {
        CustomerList.Clear();
        TmpCustomerList.Clear();

        accessDB.ViewArchived = AccessService.ViewCurrent;

        string customerListSQL =
            "SELECT AccountName, CustomerAccountID " +
            "FROM [tblCustomerAccounts] ";
           

        //true is archived only
        if (IsCheckedCustomerArchived == true) {
            customerListSQL += $"WHERE (tblCustomerAccounts.[Archive]) = -1 ";
            CustomerViewingby = "Viewing Archived";
        }
        //false is current
        else if (IsCheckedCustomerArchived == false) {
            customerListSQL += $"WHERE (tblCustomerAccounts.[Archive]) = 0 ";
            CustomerViewingby = "Viewing Current";
        }
        // show all
        else { CustomerViewingby = "Viewing All"; }

        customerListSQL += "ORDER BY AccountName";

        //"ORDER BY tblServicePlan.ServicePlanStatus, tblServicePlan.ServicePlanExpireDate, " +
        //              "tblServicePlan.ServicePlanNumber";

        DataTable dbTable = accessDB.FetchDBRecordRequest(customerListSQL);

        foreach (DataRow row in dbTable.Rows) {
            CustomerList.Add(new() { AccountName = row["AccountName"].ToString(), CustomerAccountID = (int)row["CustomerAccountID"] });
            TmpCustomerList.Add(new() { AccountName = row["AccountName"].ToString(), CustomerAccountID = (int)row["CustomerAccountID"] });
        }

        if (SelectedCustomer == null) {
            SelectedCustomer = new();
            SelectedCustomer = CustomerList[0];
            _customerAccountName = SelectedCustomer.AccountName;
        }

        CurrentCustomerPicked();
    }

    
}