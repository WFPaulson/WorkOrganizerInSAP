namespace ContractWork.MVVM.PMandInventory;
public static class PMandInventorySQLStatements {

    public static string GetCustomerAccountData(int ID) {
        return "SELECT * " +
        "FROM tblCustomerAccounts " +
        $"WHERE  CustomerAccountID = {ID} ";
    }

    public static string GetFullPMList() {
        return 
            "SELECT " +
                "tblCustomerAccounts.AccountName AS [Account Name], " +
                "tblCustomerAccounts.PMList AS [Add To List], " +
                "tblEquipment.CustomerAccountID_FK AS [ID], " +
                "tblEquipment.ServicePlanID_FK AS [Service Plan], " +
                "tblEquipment.PMMonth AS [PM Month], " +
                "tblEquipment.PMMonthNumber AS [PM Month Number], " +
                "tblEquipment.ServicePlanStatusLU_cbo AS [Service Plan Status], " +
                "tblEquipmentModel_LU.ModelType AS [Model], " +
                "Count(tblEquipment.EquipmentSerial) AS [Device Qty] " +
            "FROM " +
                "tblCustomerAccounts " +
                "INNER JOIN (" +
                    "tblEquipmentModel_LU " +
                    "INNER JOIN tblEquipment ON tblEquipmentModel_LU.ModelID = tblEquipment.ModelID" +
                ") ON tblCustomerAccounts.CustomerAccountID = tblEquipment.CustomerAccountID_FK " +
            "GROUP BY " +
                "tblCustomerAccounts.AccountName, " +
                "tblCustomerAccounts.PMList, " +
                "tblCustomerAccounts.Archive, " +
                "tblEquipment.CustomerAccountID_FK, " +
                "tblEquipment.ServicePlanID_FK, " +
                "tblEquipment.PMMonth, " +
                "tblEquipment.PMMonthNumber, " +
                "tblEquipment.ServicePlanStatusLU_cbo, " +
                "tblEquipment.Archive, " +
                "tblEquipment.NoPMContract, " +
                "tblEquipmentModel_LU.ModelType " +
            "HAVING (" +
                "((tblCustomerAccounts.Archive) <> TRUE) " +
                "AND ((tblEquipment.Archive) <> TRUE) " +
                "AND ((tblEquipment.NoPMContract) <> TRUE)) " +
            "ORDER BY " +
                "[tblCustomerAccounts.AccountName] ASC ";
    }

    public static string GetRefreshListFirstHalf() {
        return
        "SELECT " +
            "tblCustomerAccounts.AccountName AS [Account Name], " +
            "tblCustomerAccounts.PMList AS [Add To List], " +
            "tblEquipment.CustomerAccountID_FK AS [ID], " +
            "tblEquipment.ServicePlanID_FK AS [Service Plan], " +
            "tblEquipment.PMMonth AS [PM Month], " +
            "tblEquipment.PMMonthNumber AS [PM Month Number], " +
            "tblEquipment.ServicePlanStatusLU_cbo AS [Service Plan Status], " +
            "tblEquipmentModel_LU.ModelType AS [Model], " +
            "Count(tblEquipment.EquipmentSerial) AS [Device Qty] " +
        "FROM " +
            "tblCustomerAccounts " +
                "INNER JOIN (" +
                    "tblEquipmentModel_LU " +
                    "INNER JOIN " +
                        "tblEquipment " +
                        "ON tblEquipmentModel_LU.ModelID = tblEquipment.ModelID" +
                ") " +
                "ON tblCustomerAccounts.CustomerAccountID = tblEquipment.CustomerAccountID_FK ";
    }
       
    public static string GetRefreshListSecondHalf() {
        return
        "GROUP BY " +
            "tblCustomerAccounts.AccountName, " +
            "tblCustomerAccounts.PMList, " +
            "tblCustomerAccounts.Archive, " +
            "tblEquipment.CustomerAccountID_FK, " +
            "tblEquipment.ServicePlanID_FK, " +
            "tblEquipment.PMMonth, " +
            "tblEquipment.PMMonthNumber, " +
            "tblEquipment.ServicePlanStatusLU_cbo, " +
            "tblEquipment.Archive, " +
            "tblEquipment.NoPMContract, " +
            "tblEquipmentModel_LU.ModelType " +
        "HAVING (" +
            "((tblCustomerAccounts.Archive) <> True) " +
            "AND((tblEquipment.Archive) <> True) " +
            "AND ((tblEquipment.NoPMContract) <> True)) " +
        "ORDER BY " +
            "[tblCustomerAccounts.AccountName] ASC ";
    }

    public static string ShowPMList() {
        return
             "SELECT " +
                "tblCustomerAccounts.AccountName AS [Account Name], " +
                "tblCustomerAccounts.PMList AS [Add To List], " +
                "tblEquipment.CustomerAccountID_FK AS [ID], " +
                "tblEquipment.ServicePlanID_FK AS [Service Plan], " +
                "tblEquipment.PMMonth AS [PM Month], " +
                "tblEquipment.PMMonthNumber AS [PM Month Number], " +
                "tblEquipment.ServicePlanStatusLU_cbo AS [Service Plan Status], " +
                "tblEquipmentModel_LU.ModelType AS [Model], " +
                "Count(tblEquipment.EquipmentSerial) AS [Device Qty] " +
            "FROM " +
                "tblCustomerAccounts " +
                "INNER JOIN (" +
                    "tblEquipmentModel_LU " +
                    "INNER JOIN tblEquipment ON tblEquipmentModel_LU.ModelID = tblEquipment.ModelID" +
                ") ON tblCustomerAccounts.CustomerAccountID = tblEquipment.CustomerAccountID_FK " +
            "GROUP BY " +
                "tblCustomerAccounts.AccountName, " +
                "tblCustomerAccounts.PMList, " +
                "tblCustomerAccounts.Archive, " +
                "tblEquipment.CustomerAccountID_FK, " +
                "tblEquipment.ServicePlanID_FK, " +
                "tblEquipment.PMMonth, " +
                "tblEquipment.PMMonthNumber, " +
                "tblEquipment.ServicePlanStatusLU_cbo, " +
                "tblEquipment.Archive, " +
                "tblEquipment.NoPMContract, " +
                "tblEquipmentModel_LU.ModelType " +
            "HAVING (" +
                "((tblCustomerAccounts.Archive) <> TRUE) " +
                "AND ((tblEquipment.Archive) <> TRUE) " +
                "AND ((tblEquipment.NoPMContract) <> TRUE)) " +
                "AND ((tblCustomerAccounts.PMList) = True)" +
            "ORDER BY " +
                "[tblCustomerAccounts.AccountName] ASC ";
    }
}
