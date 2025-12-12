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

    public static string GetAPMList() {

        return
        "SELECT " +
            "tblEquipment.CustomerAccountID_FK AS [ID], " +
            "tblCustomerAccounts.AccountName AS [Account Name], " +
            "tblEquipmentModel_LU.ModelType AS [Model], " +
            "Count(tblEquipment.EquipmentSerial) AS [Device Qty], " +
            "tblEquipment.PMMonth AS [PM Month], " +
            "tblPMMonth_LU.MonthNumber AS [Month Number], " +
            "tblCustomerAccounts.PMList AS [Add To List] " +
        "FROM " +
            "tblPMMonth_LU " +
            "INNER JOIN (" +
                "tblCustomerAccounts " +
                "INNER JOIN (" +
                    "tblEquipmentModel_LU " +
                    "INNER JOIN " +
                        "tblEquipment " +
                        "ON tblEquipmentModel_LU.ModelID = tblEquipment.ModelID" +
                ") " +
                "ON tblCustomerAccounts.CustomerAccountID = tblEquipment.CustomerAccountID_FK" +
            ") " +
            "ON tblPMMonth_LU.PMMonth = tblEquipment.PMMonth " +
        "GROUP BY " +
            "tblEquipment.CustomerAccountID_FK, " +
            "tblCustomerAccounts.AccountName, " +
            "tblEquipmentModel_LU.ModelType, " +
            "tblEquipment.PMMonth, " +
            "tblPMMonth_LU.MonthNumber, " +
            "tblCustomerAccounts.Archive, " +
            "tblEquipment.Archive, " +
            "tblCustomerAccounts.PMList, " +
            "tblEquipment.NoPMContract " +
        "HAVING (" +
            "((tblCustomerAccounts.Archive) <> True) " +
            "AND((tblEquipment.Archive) <> True) " +
            "AND ((tblEquipment.NoPMContract) <> True)) " +
        "ORDER BY " +
            "[tblPMMonth_LU.MonthNumber] DESC, [tblCustomerAccounts.AccountName] ASC ";

    }


    //"FROM " +
    //        "tblPMMonth_LU " +
    //        "INNER JOIN (" +
    //            "tblCustomerAccounts " +
    //            "INNER JOIN " +
    //                "tblEquipmentModel_LU INNER JOIN tblEquipment " +
    //                "ON tblEquipmentModel_LU.ModelID = tblEquipment.ModelID) " +
    //                "ON tblCustomerAccounts.CustomerAccountID = tblEquipment.CustomerAccountID_FK" +
    //        ") ON tblPMMonth_LU.PMMonth = tblEquipment.PMMonth " +

    //"FROM tblPMMonth_LU INNER JOIN(tblCustomerAccounts INNER JOIN (tblEquipmentModel_LU INNER JOIN tblEquipment " +
//      "ON tblEquipmentModel_LU.ModelID = tblEquipment.ModelID) " +
//      "ON tblCustomerAccounts.CustomerAccountID = tblEquipment.CustomerAccountID_FK) " +
//      "ON tblPMMonth_LU.PMMonth = tblEquipment.PMMonth ";


    //"First(tblEquipment.PMCompleted) AS [PM Completed], Last(tblEquipment.PMCompleted) AS [Oldest Completed], " +

    // Count
    //
    //"GROUP BY tblEquipment.CustomerAccountID_FK, tblCustomerAccounts.AccountName, tblEquipmentModel_LU.ModelType, " +
    //        "tblEquipment.PMMonth, tblPMMonth_LU.MonthNumber, tblCustomerAccounts.Archive, tblEquipment.Archive, " +
    //        "tblCustomerAccounts.PMList, tblEquipment.NoPMContract " +
    //        "HAVING(((tblCustomerAccounts.Archive) <> True) " +
    //        "AND((tblEquipment.Archive) <> True) " +
    //        "AND ((tblEquipment.NoPMContract) <> True)) " +

    //[tblPMMonth_LU.MonthNumber] DESC, 
    // "ON tblPMMonth_LU.PMMonth = tblEquipment.PMMonth " +


    //"WHERE(((tblCustomerAccounts.Archive) <> True) " +
    //        "AND((tblEquipment.Archive) <> True) " +
    //        "AND ((tblEquipment.NoPMContract) <> True)) " +

    //"AND ((tblCustomerAccounts.PMList) = True) " +

    //TODO: need to add check if contract has epired also

    public static string ShowPMList() {
        return
            "SELECT " +
                "tblCustomerAccounts.AccountName AS [Account Name], " +
                "tblCustomerAccounts.PMList AS [Add To List], " +
                "tblEquipment.CustomerAccountID_FK AS [ID], " +
                "tblEquipment.ServicePlanID_FK AS [Service Plan], " +
                "tblEquipment.PMMonth AS [PM Month], " +
                "tblEquipment.PMMonthNumber AS [PM Month Number], " +
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


        //return 
        //    "SELECT " +
        //        "tblCustomerAccounts.AccountName AS [Account Name], " +
        //        "tblCustomerAccounts.PMList AS [Add To List], " +
        //        "tblEquipment.CustomerAccountID_FK AS [ID], " +
        //        "tblEquipment.PMMonth AS [PM Month], " +
        //        "tblEquipment.PMMonthNumber AS [PM Month Number], " +
        //        "tblEquipmentModel_LU.ModelType AS [Model], " +
        //        "Count(tblEquipment.EquipmentSerial) AS [Device Qty] " +
        //    "FROM tblPMMonth_LU INNER JOIN(tblCustomerAccounts INNER JOIN (tblEquipmentModel_LU INNER JOIN tblEquipment " +
        //        "ON tblEquipmentModel_LU.ModelID = tblEquipment.ModelID) " +
        //        "ON tblCustomerAccounts.CustomerAccountID = tblEquipment.CustomerAccountID_FK) " +
        //        "ON tblPMMonth_LU.PMMonth = tblEquipment.PMMonth " +
        //    "GROUP BY " +
        //        "tblCustomerAccounts.AccountName, " +
        //        "tblCustomerAccounts.PMList, " +
        //        "tblCustomerAccounts.Archive, " +
        //        "tblEquipment.CustomerAccountID_FK, " +
        //        "tblEquipment.PMMonth, " +
        //        "tblEquipment.PMMonthNumber, " +
        //        "tblEquipment.NoPMContract, " +
        //        "tblEquipment.Archive, " +
        //        "tblEquipmentModel_LU.ModelType " +
        //    "HAVING(" +
        //        "((tblCustomerAccounts.Archive) <> True) " +
        //        "AND ((tblEquipment.Archive) <> True) " +
        //        "AND ((tblEquipment.NoPMContract) <> True) " +
        //        "AND ((tblCustomerAccounts.PMList) = True)" +
        //    ") " +
        //    "ORDER BY tblCustomerAccounts.AccountName ";
    }

}
//"tblPMMonth_LU.MonthNumber AS [Month Number], " +
