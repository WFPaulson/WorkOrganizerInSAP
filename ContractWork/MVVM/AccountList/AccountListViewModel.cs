using System.Diagnostics.Contracts;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using ContractWork.MVVM.Quoting;

namespace ContractWork.MVVM.AccountList; 

public partial class AccountListViewModel : ObservableObject {

    #region Observable statements
    [ObservableProperty]
    private DataTable _accountContractList;

    [ObservableProperty]
    private DataTable _accountAssets;

    [ObservableProperty]
    private string _accountName;

    [ObservableProperty]
    private DataRowView _selectedContract;

    #endregion


    public NavigationService _navigationService { get; set; }

    public AccountListViewModel(NavigationService navigationService, DataTable customerAccount, string customerName, 
                                                            DataTable customerAssets) {
        _navigationService = navigationService;
        AccountContractList = customerAccount.Copy(); //Datagrid list
        AccountAssets = customerAssets.Copy();
        AccountName = customerName;

    }

    [RelayCommand]
    private void ContractAndAssetList() {
        ExcelService excelWB = new();

        DataTable selectedContractDevices = new();
        selectedContractDevices.Clear();
        selectedContractDevices = AccountAssets.Clone();

        DataTable accountDevicesNotOnAnyContract = new();
        accountDevicesNotOnAnyContract.Clear();
        accountDevicesNotOnAnyContract = AccountAssets.Clone();

        DataTable expiredContracts = new();
        expiredContracts.Clear();
        expiredContracts = AccountAssets.Clone();

        Dictionary<string, string> tmpContractHolder = new();

        string custContract = string.Empty;
        string custAccount = string.Empty;
        string custName = string.Empty;
        string custSalesRep = string.Empty;
        string custStatus = string.Empty;

        string contractNumber = SelectedContract.Row["Contract"].ToString();
        string contractAccount = SelectedContract.Row["Account"].ToString();
        string contractName = SelectedContract.Row["Customer"].ToString();
        string contractSalesRep = SelectedContract.Row["Sales Rep"].ToString(); ;

        string backupLocation = ExcelService.ExcelFileLocation;
        ExcelService.ExcelFileLocation = DashboardViewModel.fileLocatinDict["AssetsFilePath"];

        string sqlExcelAssets =
            "SELECT * " +
            "FROM [Edited$] " +
            $"WHERE [Sales Rep] = '{contractSalesRep}' " +
            $"AND [Customer] = '{contractName}' ";
         
        (DataTable excelAssets, _) = excelWB.RefreshSpreadSheet(sqlExcelAssets);

        foreach (DataRow dtrow in AccountAssets.Rows) {
            custContract = dtrow["Contract"].ToString();
            custAccount = dtrow["Account"].ToString();
            custName = dtrow["Customer"].ToString();
            custSalesRep = dtrow["Sales Rep"].ToString();
            custStatus = dtrow["Status"].ToString();

            if (custContract == contractNumber || custAccount == contractAccount 
                             || custName == contractName || tmpContractHolder.ContainsKey(custContract)) {
                if (custContract != "" && custSalesRep == contractSalesRep) {
                    if (custStatus == "Expired") expiredContracts.ImportRow(dtrow);
                    else selectedContractDevices.ImportRow(dtrow);
                    try {
                        tmpContractHolder.Add(custContract, custContract);
                    }
                    catch (Exception) {
                        
                    }
                    
                }
                else if (custSalesRep == contractSalesRep) { accountDevicesNotOnAnyContract.ImportRow(dtrow); } 
            }
        }

        ExcelService.ExcelFileLocation = backupLocation;

        CustomerDeviceListWindow customerDeviceList = new() {
            DataContext = _navigationService.CurrentViewModel = new CustomerDeviceListViewModel(_navigationService,
                                                                selectedContractDevices, expiredContracts, accountDevicesNotOnAnyContract, contractName)
        };
        customerDeviceList.ShowDialog();

        // CustomerContractSerialNumbers contractSerialNumbers = new (selectedContractDevices, 
        //                                                      expiredContracts, accountDevicesNotOnAnyContract, contractName);

        // if (contractSerialNumbers.ShowDialog() == true) {

        //CreateQuote(selectedContractDevices, expiredContracts, accountDevicesNotOnAnyContract, contractName);
        // }
    }

    private void CreateQuote(DataTable onContract, DataTable expireContract, DataTable noContract, string contractNumber) {
        //MessageBox.Show("hit quote");

        CustomerDeviceListWindow customerDeviceList = new() {
            DataContext = _navigationService.CurrentViewModel = new CustomerDeviceListViewModel(_navigationService,
                                                                onContract, expireContract, noContract, contractNumber)
        };
        customerDeviceList.ShowDialog();
    }

}
