using System.Windows.Navigation;

namespace ContractWork.MVVM.ContractAndAssets;
public static class ContractAndAssetsSQLStatements {

    #region Access SQL
    public static string OpenAccessFileSQL() {
        return "SELECT tblCustomerAccounts.MainAccount_PK, tblServicePlan.ServicePlanID, tblServicePlan.AccountName_cbo, " +
            "tblServicePlan.ServicePlanNumber, tblServicePlan.ServicePlanStatus, tblServicePlan.ServicePlanStartDate, " +
            "tblServicePlan.ServicePlanExpireDate, tblServicePlan.Expired, tblServicePlan.POExpireDate, " +
            "tblServicePlan.POExpired, tblServicePlan.Archive, tblServicePlan.InWork, tblServicePlan.Notes " +
            "FROM tblServicePlan INNER JOIN (tblCustomerAccounts INNER JOIN tblEquipment " +
            "ON tblCustomerAccounts.CustomerAccountID = tblEquipment.CustomerAccountID_FK) " +
            "ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +
            "WHERE(((tblServicePlan.ServicePlanNumber) <>'xxxxxxx') AND((tblServicePlan.Archive) = False))";
    }

    public static string OpenFullAccessSQL() {

        return "SELECT * " +
           "FROM tblCustomerAccounts " +
           "WHERE((tblCustomerAccounts.Archived) <> 'FALSE')";
    }

    public static string OpenDBForAccountConversionSQL() {
        return "SELECT [AccountName], [MainAccount_PK], [BillingAccount], " +
            "[ShippingAccount] " +      //, [OldMainAccount]
            "FROM tblCustomerAccounts " +
            "ORDER BY [AccountName]";
    }



    public static string ReduceDBSQL() {
        return "SELECT [AccountName], [MainAccount_PK], [BillingAccount], " +
            "[ShippingAccount] " +      //, [OldMainAccount]
            "FROM tblCustomerAccounts " +
            "WHERE LEFT(MainAccount_PK, 1) <> 2 " +
            "ORDER BY [AccountName]";

    }

    public static string OpenInventoryFileSQL() {
        return "SELECT * " +
            "FROM tblInventoryDataFromExcel " +
            "ORDER BY CatalogNumber";

    }

    #endregion


    #region Excel SQL
    public static string OpenExcelFileSQL() {
        return "SELECT * " +
            "FROM [Edited$] " +
            "ORDER BY Status DESC, 'Customer', 'Cvg End'";        //Customer
    }   //"WHERE [Archive] = False " +

    public static string OpenAssetsExtendedFileSQL() {
        return "SELECT * " +
            "FROM [Edited$] " +
            "ORDER BY Status DESC, 'Ship To Name', 'Cvg End'";
    }

    public static string OpenExcelFileWithDollarValueSQL() {
        return "SELECT * " +
            "FROM [Edited$] " +
            "WHERE [Net Value] <> 0 AND [Contract] <> '' " +
            "ORDER BY Status DESC, 'Customer', 'Cvg End' ";
    }   //"WHERE [Archive] = False " +

    public static string OpenXLAccountForConvertingSQL() {
        return "SELECT DISTINCT [Account], [Customer] " +
            "FROM [Edited$] " +
            "ORDER BY Customer";
    }

    public static string AssetsWithContractOnlyExcelFileSQL() {

        //return "SELECT * " +
        //    "FROM [Edited$] " +
        //    @"WHERE [Contract] <> null";

        //"ORDER BY [Customer] " +
        //WHERE(((Contract)Is Not Null));

        //return "select* from[Edited$] where[Contract] IS NULL";
        return "select * from [Edited$] where Contract > 0";


    }
    //[Ship To], [Bill To], [Contract Material], 
    public static string EquipmentNotExpiredContract () {
        return "SELECT DISTINCT [Account], [Customer], [Serial], [Product], [Contract], " +
            "[Cvg Start], [Cvg End], [Status], [Tech], [Sales Rep] " +     
            "FROM [Edited$] " +
            "WHERE [Contract] IS NOT NULL " +
            "AND [Serial] <> '(1 unit)' " +
            "AND [Serial] <> '(HSS)' " +
            "AND [Status] = 'Active' " +
            "OR [Status] = 'Future'";
    }

    public static string EquipmentExpiredContract() {
        return "SELECT DISTINCT [Contract], [Ship To], [Bill To], [Contract Material], [Covered Serial], [Status] " +       //[Model],  //[Sold], , [Bill To], [Tech], [Sales Rep] " + //, //[Bill To], , [Contract] " +, [Tech], [Sales Rep]
            "FROM [Edited$] " +
            "WHERE [Contract] IS NOT NULL " +
            "AND [Covered Serial] <> '(1 unit)' " +
            "AND [Covered Serial] <> '(HSS)' " +
            "AND [Status] = 'Expired'";
        
            //"AND [Covered Serial] <> '(1 unit)' " +
            //"AND [Status] = 'Expired'";

        //"AND [Covered Serial] <> '(HSS)' " +

    }

    //change this to a t parameter to use either accountname or accountnumber, if both strings maybe number be int??
    public static string GetCustomersContractList(string AccountNameOrNumber) {

        if (double.TryParse(AccountNameOrNumber, out _)) {
            return "SELECT * " +
            "FROM [Edited$] " +
            $"WHERE Account = {AccountNameOrNumber} " +
            "ORDER BY Status, 'CVG End'";

        }
        else {
            return "SELECT * " +
            "FROM [Edited$] " +
            $"WHERE Customer = '{AccountNameOrNumber}' " +
            "ORDER BY Status, 'CVG End'";
        }
    }

    public static string GetJoesPMListSQL() {
        return "SELECT * " +
            "FROM [Report$] " +
            "ORDER BY Account";
    }


    #endregion

}
