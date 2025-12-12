namespace ContractWork.MVVM.ConvertToSAP;
public partial class ConvertToSAPViewModel : ObservableObject {

    [ObservableProperty]
    private DataTable _sAPAccountNumbers;
    [ObservableProperty]
    private DataTable _dBAccountNumbers;
    [ObservableProperty]
    private DataRowView _accountSelected;

    [ObservableProperty]
    private CustomerAccountModel _accountToSAP;

    private NavigationService _navigationService { get; set; }
    public ConvertToSAPViewModel(NavigationService navigationService, DataTable sapAccountNumbers, DataTable dbAccountNumbers) {
        _navigationService = navigationService;
        SAPAccountNumbers = sapAccountNumbers;
        DBAccountNumbers = dbAccountNumbers;
    }

    [RelayCommand]
    private void OpenPopupForSAPUpdate() {  //Get here by mouse double click
        MessageBoxResult result;
        AccessService db = new();
        DataTable sapAccountNumber = new();
        sapAccountNumber = SAPAccountNumbers.Clone();

        AccountToSAP = new();

        string accountName = AccountSelected.Row["Customer"].ToString();
        string accountNumber = AccountSelected.Row["Account"].ToString();
        accountNumber = accountNumber.Remove(0, 2);
        Clipboard.SetText(accountNumber);

        sapAccountNumber.ImportRow(AccountSelected.Row);

        try {
            foreach (DataRow row in DBAccountNumbers.Rows) {
                if (accountName == row["AccountName"].ToString()) {
                    AccountToSAP.AccountName = row["AccountName"].ToString();
                    AccountToSAP.MainAccountNumber = (int)row["MainAccount_PK"];
                    AccountToSAP.BillingAccountNumber = (int)row["BillingAccount"];
                    AccountToSAP.ShippingAccountNumber = (int)row["ShippingAccount"];
                    AccountToSAP.OldMainAccountNumber = (int)row["OldMainAccount"];
                }
            }
        }

        catch (Exception ex) {
            MessageBox.Show($"Did not find account: {accountName} error: {ex.Message}");
        }
        if (AccountToSAP.AccountName == null) {
            MessageBox.Show("Nothing returned from Access");
            result = MessageBox.Show($"Is this customer New? ", "?New Customer?",
                                        MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) {
                MessageBox.Show("Maybe the Account Name is not the same in Access as Excel");
                List<string> accessName = db.GetListOfNames(accountName);
                if (accessName.Count > 0) {
                    foreach (string dbName in accessName) {
                        result = MessageBox.Show($"Rename Account Name from {dbName} to {accountName}? ", "?Rename Customer?",
                                            MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if(result == MessageBoxResult.Yes) { 
                            string sqlRename1 = $"UPDATE [tblCustomerAccounts] " +
                                $"SET [AccountName] = '{accountName}' " +
                                $"WHERE [AccountName] = '{dbName}'  ";
                            string sqlRename2 = $"UPDATE [tblAccountName_LU] " +
                                $"SET [AccountName] = '{accountName}' " +
                                $"WHERE [AccountName] = '{dbName}'  ";

                            db.AddToAccount(sqlRename1, GL.FileAndFolderLocations["AccessFilePath"]);
                            db.AddToAccount(sqlRename2, GL.FileAndFolderLocations["AccessFilePath"]);
                            MessageBox.Show("Update should be complete, need to refresh");


                            return;
                        }
                    }
                }

                //MessageBox.Show($"Account Name selected: {accountName}, Access Name to Change");

                return;
            }
            else if (result == MessageBoxResult.Yes) {
                MessageBox.Show($"Going to attempt to create new Account for {accountName}");
                try {
                    //db.CreateNewCustomerAccount();
                    string sqlCustAcct, sqlLookUp;
                    (sqlCustAcct, sqlLookUp) = ConvertToSAPSQLStatements.AddNewCustomerAccount(accountName, accountNumber);
                    db.AddToAccount(sqlCustAcct);
                    db.AddToAccount(sqlLookUp);
                }
                catch (Exception ex) {
                    MessageBox.Show($"Attempt to create new Account for {accountName} - FAILED ");

                }

            }
        }


         ConvertAccountsWindow updateAccounts = new() {
            DataContext = _navigationService.CurrentViewModel = new ConvertAccountsViewModel(_navigationService,
                                                                        sapAccountNumber, AccountToSAP, accountNumber)
        };
        updateAccounts.ShowDialog();

        DBAccountNumbers = db.RefreshDB(ConvertToSAPSQLStatements.RefreshDBSQL());
        
    }
}
