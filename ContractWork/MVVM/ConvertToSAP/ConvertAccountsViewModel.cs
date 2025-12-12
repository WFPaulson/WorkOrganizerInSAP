using Servicelibraries.ExtensionMethods.ExtensionMethods;

using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;

namespace ContractWork.MVVM.ConvertToSAP;

public partial class ConvertAccountsViewModel : ObservableObject {

    public ObservableCollection<CustomerAccountModel> ConvertAccounts { get; } = new();

    [ObservableProperty]
    private DataTable _sapaccounts;
    [ObservableProperty]
    private DataTable _dbaccounts;

    private CustomerAccountModel dbCustomerAccountModel;

    public string reviseAccountNumber { get; set; }
    public string originalAccount { get; set; }


    private NavigationService _navigationService { get; set; }
    public ConvertAccountsViewModel(NavigationService navigationService, DataTable sAPAccount, 
                                        CustomerAccountModel dBAccount, string copiedSAPAccountNumber)
    {
        _navigationService = navigationService;
        reviseAccountNumber = copiedSAPAccountNumber;
        Sapaccounts = sAPAccount.Copy();
        ConvertAccounts.Add(dBAccount);
        originalAccount = ConvertAccounts[0].OldMainAccount;

        UpdateAccount(reviseAccountNumber);
        RefreshAccount();
    }

    private void RefreshAccount() {
        MessageBoxResult result;
        AccessService db = new();
        DataTable dt = new();

        CustomerAccountModel dbCustomerAccountModel = new();
        string sqlRefresh = ConvertToSAPSQLStatements.RefreshDBSQL(reviseAccountNumber);
        dt = db.RefreshDB(sqlRefresh);
        if (!dt.dtISNullOrEmpty()){
            try{
                dbCustomerAccountModel.AccountName = dt.Rows[0]["AccountName"].ToString();
                dbCustomerAccountModel.MainAccountNumber = (int)dt.Rows[0]["MainAccount_PK"];
                dbCustomerAccountModel.BillingAccountNumber = (int)dt.Rows[0]["BillingAccount"];
                dbCustomerAccountModel.ShippingAccountNumber = (int)dt.Rows[0]["ShippingAccount"];
                dbCustomerAccountModel.OldMainAccountNumber = (int)dt.Rows[0]["OldMainAccount"];

                ConvertAccounts.Add(dbCustomerAccountModel);
            }
            catch (Exception ex){
                MessageBox.Show($"Did not refresh, error: {ex.Message}");
            }
        }
        else{
            MessageBox.Show("Nothing returned from Access");
            result = MessageBox.Show($"Is this customer New? ", "?New Customer?", 
                                        MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) { MessageBox.Show("Maybe the Account Name is not the " +
                                                                    "same in Access as Excel"); }
            else if (result == MessageBoxResult.Yes){
                //db.CreateNewCustomerAccount();
            }
        }
    }

    
    //what em I trying to do below originalaccount has value?  
    private void UpdateAccount(string? newAccount) {
        AccessService accessDB = new();
        string[] updateFields1, updateFields2, updateFields3;

        if (!string.IsNullOrEmpty(originalAccount)) { 
            originalAccount = ConvertAccounts[0].OldMainAccount;
            if (!string.IsNullOrEmpty(originalAccount)) { 
                originalAccount = newAccount; 
            }
        }
        

        updateFields1 = ["tblCustomerAccounts", "MainAccount_PK", newAccount, "OldMainAccount", originalAccount];       //ConvertAccounts[0].AcctMain];
        updateFields2 = ["tblCustomerAccounts", "ShippingAccount", newAccount, "OldMainAccount", originalAccount];         //ConvertAccounts[0].AcctMain];
        updateFields3 = ["tblAccountName_LU", "MainAccount_FK", newAccount, "OldMainAccount", originalAccount];         //ConvertAccounts[0].AcctMain];
        
        accessDB.AddToAccount(ConvertToSAPSQLStatements.UpdateDBFieldSQL(updateFields1), GL.FileAndFolderLocations["AccessFilePath"]);
        accessDB.AddToAccount(ConvertToSAPSQLStatements.UpdateDBFieldSQL(updateFields2), GL.FileAndFolderLocations["AccessFilePath"]);
        accessDB.AddToAccount(ConvertToSAPSQLStatements.UpdateDBFieldSQL(updateFields3), GL.FileAndFolderLocations["AccessFilePath"]);
    }

    [RelayCommand]
    private void UpdateAccountNumbers() {
        AccessService accessDB = new();
        string[] updateFields;

        string mainAccount = ConvertAccounts[1].MainAccount;
        string? billAccount = ConvertAccounts[1].BillingAccount;
        string? shipAccount = ConvertAccounts[1].ShippingAccount;
        
        updateFields = new[] { mainAccount, billAccount, shipAccount, originalAccount };

        accessDB.AddToAccount(ConvertToSAPSQLStatements.UpdateCustomerAccountNumbers(updateFields), 
            GL.FileAndFolderLocations["AccessFilePath"]);

    }
}
