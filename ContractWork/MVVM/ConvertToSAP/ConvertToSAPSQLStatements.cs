using System.Data.SqlTypes;
using System.Windows.Navigation;

namespace ContractWork.MVVM.ConvertToSAP;
public static class ConvertToSAPSQLStatements {

    public static string UpdateDBFieldSQL(string[] updateParameters) {
        return $"UPDATE [{updateParameters[0]}] " +
            $"SET [{updateParameters[1]}] = {updateParameters[2]} " +
            $"WHERE [{updateParameters[3]}] = {updateParameters[4]}  ";
    }

    public static string RefreshDBSQL(string mainAccountNumber = null) {
        string sqlString =  "SELECT [AccountName], [MainAccount_PK], " +
            "[BillingAccount], [ShippingAccount], [OldMainAccount] " +
            "FROM tblCustomerAccounts ";
        if (mainAccountNumber != null) {
            sqlString += $"WHERE [MainAccount_PK] = {mainAccountNumber} ";
        }
        sqlString += "ORDER BY [AccountName]";
        return sqlString;    
    }

    public static string UpdateCustomerAccountNumbers(string[] updateFields) {
        return $"UPDATE [tblCustomerAccounts] " +
            $"Set [MainAccount_PK] = {updateFields[0]}, [BillingAccount] = {updateFields[1]}, [ShippingAccount] = {updateFields[2]} " +
            $"WHERE [OldMainAccount] = {updateFields[3]}";
    }

    public static (string custAcct, string lookUp) AddNewCustomerAccount(string customerAccountName, string customerAccountNumber) {
        string newAccount = "INSERT INTO [tblCustomerAccounts]([AccountName], [MainAccount_PK], [OldMainAccount]) " +
                            $"VALUES ('{customerAccountName}', '{customerAccountNumber}', {customerAccountNumber}) ";

        string newAccount_LU = "INSERT INTO [tblAccountName_LU]([AccountName], [MainAccount_FK], [OldMainAccount]) " +
                            $"VALUES ('{customerAccountName}', '{customerAccountNumber}', {customerAccountNumber}) ";


        return (newAccount, newAccount_LU);
    }
}
