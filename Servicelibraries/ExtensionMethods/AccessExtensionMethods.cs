using System.Windows.Controls.Primitives;
using GlobalLibraries;


public static class AccessExtensionMethods {

    //public static string UpdateDBField(this, string[] updateParameters) {
    //    return $"UPDATE [{updateParameters[0]}]" +
    //        $"SET [{updateParameters[1]}] = [{updateParameters[2]}] " +
    //        $"WHERE [{updateParameters[3]}] = [{updateParameters[4]}]  ";

    //}

    //public static string getCustomerAccountName(this int _mainAccountOrID) {  //int? _customerID = null, int? _mainAccountNumber = null, 
    //    AccessService dB = new AccessService();
    //    string sqlTemp;
    //    if (_mainAccountOrID == 0 || _mainAccountOrID == null) { return string.Empty; }

    //    if (_mainAccountOrID < 1000000) {
    //        sqlTemp =
    //            "SELECT [AccountName] " +
    //            "FROM [tblCustomerAccounts] " +
    //            $"WHERE [CustomerAccountID] = {_mainAccountOrID} ";
    //    }
    //    else {
    //        sqlTemp =
    //            "SELECT [AccountName] " +
    //            "FROM [tblCustomerAccounts] " +
    //            $"WHERE [MainAccount_FK] = {_mainAccountOrID} ";
    //    }

    //    DataTable dttemp = dB.FetchDBRecordRequest(fullstatement: sqlTemp);
    //    return dttemp.Rows[0]["AccountName"].ToString();
    //}

    public static int getMainAccountOrCustID(this int _mainAccountOrID) {
        AccessService dB = new();
        string sqlTemp;

        if (_mainAccountOrID < 1000000) {
            sqlTemp =
                "SELECT [MainAccount_PK] " +
                "FROM [tblCustomerAccounts] " +
                $"WHERE [CustomerAccountID] = {_mainAccountOrID} ";
        }
        else {
            sqlTemp =
                "SELECT [CustomerAccountID] " +
                "FROM [tblCustomerAccounts] " +
                $"WHERE [MainAccount_PK] = {_mainAccountOrID} ";
        }

        DataTable dttemp = dB.FetchDBRecordRequest(fullstatement: sqlTemp);

        if (dttemp == null || dttemp.Rows.Count <= 0) {
            return -1;

        }
        return _mainAccountOrID < 1000000
            ? (int)dttemp.Rows[0]["MainAccount_PK"]
            : (int)dttemp.Rows[0]["CustomerAccountID"];


    }

    public static int getMainAccountOrCustID(this double _mainAccountOrID) {
        AccessService dB = new();
        string sqlTemp;

        if (_mainAccountOrID < 200000) {
            sqlTemp =
                "SELECT [MainAccount_PK] " +
                "FROM [tblCustomerAccounts] " +
                $"WHERE [CustomerAccountID] = {_mainAccountOrID}";
        }
        else {
            sqlTemp =
                "SELECT [CustomerAccountID] " +
                "FROM [tblCustomerAccounts] " +
                $"WHERE [MainAccount_PK] = {_mainAccountOrID}";
        }

        DataTable dttemp = dB.FetchDBRecordRequest(fullstatement: sqlTemp);

        if (dttemp == null || dttemp.Rows.Count <= 0) {
            return -1;

        }
        return _mainAccountOrID < 200000
            ? (int)dttemp.Rows[0]["MainAccount_PK"]
            : (int)dttemp.Rows[0]["CustomerAccountID"];


    }

    public static int getShippingAccountNumberFromCustID(this int custID) {
        AccessService dB = new();

        string sqlGetMainAccount =
            "SELECT [MainAccount_PK] " +
            "FROM tblCustomerAccounts " +
            $"WHERE [CustomerAccountID] = {custID} ";

        DataTable dttemp = dB.FetchDBRecordRequest(fullstatement: sqlGetMainAccount);


        return (int)dttemp.Rows[0]["MainAccount_PK"];
    }

    //public static string GetServicePlanNumber(this int servicePlanID) {
    //    AccessService db = new  ();
    //    //string customerName = string.Empty;

    //    if (servicePlanID == 0 || servicePlanID == null) { return string.Empty; }

    //    string sqltemp =
    //        $"SELECT [ServicePlanNumber] FROM [tblServicePlan] WHERE [ServicePlanID] = {servicePlanID} ";

    //    DataTable dttemp = db.FetchDBRecordRequest(fullstatement: sqltemp);

    //    //DataRow accountrow = dttemp.Rows[0];
    //    //return customerName = accountrow["ServicePlanNumber"].ToString();
    //    return dttemp.Rows[0]["ServicePlanNumber"].ToString();

    //}

    //public static string GetServicePlanNumber(this string deviceSerialNumber) {
    //    AccessService db = new AccessService();
    //    //string customerName = string.Empty;

    //    //if (servicePlanID == 0 || servicePlanID == null) { return string.Empty; }

    //    string sqltemp =
    //        "SELECT tblServicePlan.ServicePlanNumber " +
    //        "FROM tblServicePlan INNER JOIN tblEquipment " +
    //        "ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +
    //        $"WHERE tblEquipment.EquipmentSerial = '{deviceSerialNumber}'";

    //    DataTable dttemp = db.FetchDBRecordRequest(fullstatement: sqltemp);

    //    //DataRow accountrow = dttemp.Rows[0];
    //    //return customerName = accountrow["ServicePlanNumber"].ToString();
    //    return dttemp.Rows[0]["ServicePlanNumber"].ToString();


    //}

    public static int GetServicePlanID(this string servicePlanNumber) {
        AccessService dB = new();

        string sqlStatus =
            "SELECT ServicePlanID " +
            "FROM tblServicePlan " +
            $"WHERE ServicePlanNumber = '{servicePlanNumber}'";

        //else {
        //    MessageBox.Show("No Service Plan found");
        //    return null;
        //}

        DataTable dt = dB.FetchDBRecordRequest(fullstatement: sqlStatus);
        if (dt.Rows.Count < 1) {
            return 0;
        }
        else return (int)dt.Rows[0]["ServicePlanID"];
    }

    public static string getServicePlanInfo(this int servicePlanID) {
        AccessService db = new();
        DataTable dttemp = new();
        string customerName = string.Empty;

        string sqltemp =
            $"SELECT [ServicePlanNumber] FROM [tblServicePlan] WHERE [ServicePlanID] = {servicePlanID} ";

        dttemp = db.FetchDBRecordRequest(sqltemp);

        DataRow accountrow = dttemp.Rows[0];
        return customerName = accountrow["ServicePlanNumber"].ToString();
    }

    public static int getServicePlanInfo(this string servicePlanNumber) {
        AccessService db = new();
        DataTable dttemp = new();
        int servicePlanID = 0;

        string sqltemp =
            $"SELECT [ServicePlanID] FROM [tblServicePlan] WHERE [ServicePlanNumber] = '{servicePlanNumber}' ";

        dttemp = db.FetchDBRecordRequest(sqltemp);

        if (dttemp.Rows.Count <= 0) {
            return 0;
        }

        DataRow accountrow = dttemp.Rows[0];
        return servicePlanID = (int)accountrow["ServicePlanID"];
    }

    public static string GetServicePlanStatus<t>(this t servicePlanNumberOrID) {
        AccessService dB = new AccessService();
        string sqlStatus;

        var servicePlanType = servicePlanNumberOrID.GetType();

        if (servicePlanType == typeof(string)) {
            sqlStatus =
            "SELECT ServicePlanStatus " +
            "FROM tblServicePlan " +
            $"WHERE ServicePlanNumber = '{servicePlanNumberOrID}'";
        }
        else if (servicePlanType == typeof(int)) {
            sqlStatus =
            "SELECT ServicePlanStatus " +
            "FROM tblServicePlan " +
            $"WHERE ServicePlanID = {servicePlanNumberOrID}";
        }
        else {
            MessageBox.Show("No Service Plan found");
            return null;
        }

        DataTable dt = dB.FetchDBRecordRequest(fullstatement: sqlStatus);
        return dt.Rows[0]["ServicePlanStatus"].ToString();
    }

    //public static string CheckServicePlanStatus(this int servicePlanID) {
    //    AccessService accessDB = new AccessService();
    //    DataTable dt = new DataTable();
    //    string status = string.Empty;
    //    bool answer = true;

    //    string sqlServicePlan =
    //        "SELECT [ServicePlanID], [ServicePlanNumber], [ServicePlanStatus], [ServicePlanStartDate], " +
    //        "[ServicePlanExpireDate], [POExpireDate], [Expired], [POExpired]" +
    //        "FROM [tblServicePlan] " +
    //        $"WHERE [ServicePlanID] = {servicePlanID}";

    //    string sqlUpdateEquipmentStatusPlan =
    //        "UPDATE tblServicePlan " +
    //        "INNER JOIN [tblEquipment] ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +          //tblEquipment.ServicePlanID_FK " + {servicePlanID}
    //        "SET tblEquipment.ServicePlanStatusLU_cbo = [tblServicePlan].[ServicePlanStatus] " +
    //        $"WHERE tblServicePlan.ServicePlanID = {servicePlanID}";

    //    dt = accessDB.FetchDBRecordRequest(fullstatement: sqlServicePlan);

    //    if (dt.Rows[0]["POExpireDate"] != DBNull.Value) {
    //        answer = (DateTime)dt.Rows[0]["POExpireDate"] > DateTime.Now;
    //    }

    //    if (dt.Rows[0]["ServicePlanStatus"].ToString() == "Active"
    //        || dt.Rows[0]["ServicePlanStatus"].ToString() == "Current") {

    //        if (((DateTime)dt.Rows[0]["ServicePlanStartDate"] < DateTime.Now) &&
    //            ((DateTime)dt.Rows[0]["ServicePlanExpireDate"] > DateTime.Now) &&
    //            (answer == true)) { }

    //        else if (((DateTime)dt.Rows[0]["ServicePlanExpireDate"] < DateTime.Now) || (answer == false)) {

    //            string message = $"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} ";

    //            if (!answer) { message += nl + "PO has expired "; }
    //            if ((DateTime)dt.Rows[0]["ServicePlanExpireDate"] < DateTime.Now) {
    //                message += nl + "Contract has expired";
    //            }
    //            status = "Expired";
    //        }
    //        else MessageBox.Show($"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} is NOT Current or Expired??? ");
    //    }

    //    else if (dt.Rows[0]["ServicePlanStatus"].ToString() == "Expired") {
    //        if ((DateTime)dt.Rows[0]["ServicePlanExpireDate"] < DateTime.Now) {
    //            accessDB.AddToAccount(SQLInsert: sqlUpdateEquipmentStatusPlan);
    //            status = "Expired";
    //        }
    //        else if (answer == false) {
    //            MessageBox.Show($"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} PO has expired ");
    //        }
    //        else MessageBox.Show($"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} has NOT expired??? ");
    //    }

    //    else if (dt.Rows[0]["ServicePlanStatus"].ToString() == "Future Contract") {
    //        if ((DateTime)dt.Rows[0]["ServicePlanStartDate"] > DateTime.Now) { status = "Future Contract"; }
    //        else if (((DateTime)dt.Rows[0]["ServicePlanStartDate"] < DateTime.Now) && ((DateTime)dt.Rows[0]["ServicePlanExpireDate"] > DateTime.Now)) {
    //            MessageBox.Show($"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} is NOT a Future Contract, but should be Current {nl} " +
    //            "Changing status to Current, you will need to Refresh this customer for updated data");
    //            status = "Current";
    //            accessDB.AddToAccount(SQLInsert: sqlUpdateEquipmentStatusPlan);
    //        }
    //        else MessageBox.Show($"Service Plan Number: {dt.Rows[0]["ServicePlanNumber"].ToString()} this is NOT Future or Current, Maybe expired??? ");
    //    }

    //    else if (dt.Rows[0]["ServicePlanStatus"].ToString() == "No Contract") { status = "No Contract"; }

    //    else if (dt.Rows[0]["ServicePlanStatus"].ToString() == "Archive") { status = "Archive"; }

    //    return status;
    //}

    //public static bool HasEquipment(this string servicePlanNumber) {
    //    AccessService db = new AccessService();


    //    //mismatch in query expression error from following statement
    //    string checkServicePlanForEquipment =
    //        "SELECT tblServicePlan.ServicePlanNumber, tblEquipment.EquipmentSerial " +
    //        "FROM tblServicePlan INNER JOIN tblEquipment " +
    //        "ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +
    //        $"WHERE tblServicePlan.ServicePlanNumber = '{servicePlanNumber}'";

    //    DataTable dt = db.FetchDBRecordRequest(fullstatement: checkServicePlanForEquipment);

    //    if (dt == null || dt.Rows.Count <= 0) { return false; }
    //    else { return true; }
    //}

    //

    //public static bool DoesEquipmentExist(this string serialNumber) {
    //    AccessService db = new AccessService();

    //    string sql =
    //        "SELECT * " +
    //        "FROM tblEquipment " +
    //        $"WHERE EquipmentSerial = '{serialNumber}'";
    //    DataTable dt = db.FetchDBRecordRequest(fullstatement: sql);
    //    if (dt.Rows.Count > 0) { return true; }
    //    else return false;
    //}

    //public static bool isAccountArchived(this int _mainAccount) {
    //    AccessService accessDB = new AccessService();
    //    string sqlIsAccountArchived =
    //        "SELECT * " +
    //        "FROM tblCustomerAccounts " +
    //        $"WHERE MainAccount_PK = {_mainAccount}";

    //    DataTable dte = accessDB.FetchDBRecordRequest(fullstatement: sqlIsAccountArchived);

    //    return Convert.ToBoolean(dte.Rows[0]["Archive"]);
    //}

    public static bool isContractArchived<t>(this t servicePlanNumberOrID) {

        AccessService accessDB = new AccessService();
        string sqlCheckIfArchivedFirst =
            "SELECT * " +
            "FROM tblServicePlan ";
        string addWhereStatememt = string.Empty;

        var servicePlanType = servicePlanNumberOrID.GetType();

        if (servicePlanType == typeof(string)) {
            addWhereStatememt = $"WHERE ServicePlanNumber = '{servicePlanNumberOrID}'";
        }
        else if (servicePlanType == typeof(int)) {

            addWhereStatememt = $"WHERE ServicePlanID = {servicePlanNumberOrID}";
        }
        else {
            MessageBox.Show("The info sent was not an int or string, going to exit");
            return false;
        }
        sqlCheckIfArchivedFirst += addWhereStatememt;

        DataTable dte = accessDB.FetchDBRecordRequest(fullstatement: sqlCheckIfArchivedFirst);

        return Convert.ToBoolean(dte.Rows[0]["Archive"]);

    }

    public static bool isAccountArchived(this int _mainAccount) {
        AccessService accessDB = new();
        string sqlIsAccountArchived =
            "SELECT * " +
            "FROM tblCustomerAccounts " +
            $"WHERE MainAccount_PK = {_mainAccount}";

        DataTable dte = accessDB.FetchDBRecordRequest(fullstatement: sqlIsAccountArchived);

        return Convert.ToBoolean(dte.Rows[0]["Archive"]);
    }

    public static bool ArchiveServicePlan(this string servicePlanNumber) {
        AccessService accessDB = new();
        string sqlArchiveContract =
            $"UPDATE [tblServicePlan] " +
            $"SET [Archive] = True " +
            $"WHERE [ServicePlanNumber] = '{servicePlanNumber}'";
        accessDB.AddToAccount(SQLInsert: sqlArchiveContract);
        return true;
    }

    public static bool DoesThisServicePlanExist<T>(this T servicePlan) {
        AccessService accessDB = new();
        string sqlstatement = string.Empty;

        var servicePlanType = servicePlan.GetType();

        if (servicePlanType == typeof(string)) {
            sqlstatement =
                "SELECT ServicePlanNumber " +
                "FROM tblServicePlan " +
                $"WHERE ServicePlanNumber = '{servicePlan}'";
        }
        else if (servicePlanType == typeof(int)) {
            sqlstatement =
                "SELECT ServicePlanID " +
                "FROM tblServicePlan " +
                $"WHERE ServicePlanID = {servicePlan}";
        }
        else { MessageBox.Show("its neither Service plan number or ID, something when wrong???"); }

        DataTable dt = accessDB.FetchDBRecordRequest(fullstatement: sqlstatement);

        if (dt == null || dt.Rows.Count <= 0) { return false; }
        else { return true; }
    }

    public static bool DoesThisAccountExist(this double account, string accountType = "") {
        AccessService accessDB = new();
        string sqlstatement = string.Empty;

        switch (accountType) {
            case "Billing":
                sqlstatement =
                   "SELECT BillingAccount " +
                   "FROM tblCustomerAccounts " +
                   $"WHERE BillingAccount = {account}";
                break;

            case "Shipping":
                sqlstatement =
                   "SELECT ShippingAccount " +
                   "FROM tblCustomerAccounts " +
                   $"WHERE ShippingAccount = {account}";
                break;

            case "Main":
            default:
                sqlstatement =
                    "SELECT MainAccount_PK " +
                    "FROM tblCustomerAccounts " +
                    $"WHERE MainAccount_PK = {account}";
                break;
        }



        DataTable dt = accessDB.FetchDBRecordRequest(sqlstatement);

        if (dt == null || dt.Rows.Count <= 0) { return false; }
        else { return true; }

    }

    public static bool DoesThisSerialNumberExist(this string serialNumber) {
        AccessService accessDB = new();
        string sqlSerialNumber =
            "SELECT [EquipmentSerial] " +
            "FROM [tblEquipment] " +
            $"WHERE [EquipmentSerial] = '{serialNumber}'";

        DataTable dt = accessDB.FetchDBRecordRequest(sqlSerialNumber);

        if (dt == null || dt.Rows.Count <= 0) { return false; }     //
        else return true;

    }

    public static bool DoesSerialNumberHaveServicePlanAndStatus(this string serialNumber, out string? Status) {
        AccessService accessDB = new();

        string sqlQuery =
            "SELECT [ServicePlanID_FK], [ServicePlanStatusLU_cbo] " +
            "FROM [tblEquipment] " +
            $"WHERE [EquipmentSerial] = '{serialNumber}'";

        DataTable dt = accessDB.FetchDBRecordRequest(sqlQuery);

        if (dt == null || dt.Rows.Count <= 0) { Status = null; return false; }     //
        else { Status = dt.Rows[0]["ServicePlanStatusLU_cbo"].ToString(); return true; }   //   
        //drRow["Covered Serial"]

    }

    public static bool ContractStatus(this int customerID, int mdlID, int planID, out string? Status) {
        AccessService accessDB = new();

        string sqlQuery =
            "SELECT DISTINCT [ServicePlanStatusLU_cbo] " +
            "FROM [tblEquipment] " +
            $"WHERE [CustomerAccountID_FK] = {customerID} " +
            "AND [tblEquipment.Archive] <> True " +
            $"AND [tblEquipment.ModelID] = {mdlID} " +
            $"AND [tblEquipment.ServicePlanID_FK] = {planID}";

        DataTable dt = accessDB.FetchDBRecordRequest(sqlQuery);

        if (dt == null || dt.Rows.Count <= 0) { Status = null; return false; }     //
        else { Status = dt.Rows[0]["ServicePlanStatusLU_cbo"].ToString(); return true; }   //   
        //drRow["Covered Serial"]

    }

    public static bool DoesCompositePrimaryKeyExist(string? primaryKey, string? secondaryKey) {
        AccessService accessDB = new();
        string sqlPrimarykey =
            "SELECT * " +
            "FROM tblCustomerAccounts " +
            $"WHERE MainAccount_PK = {primaryKey} " +
            $"AND BillingAccount = {secondaryKey}";

        DataTable dt = accessDB.FetchDBRecordRequest(sqlPrimarykey);

        if (dt == null || dt.Rows.Count <= 0) { return false; }
        else { return true; }
    }

    //public static string GetCustomerAccountNameFromID(this double accountID) {
    //    AccessService accessDB = new();

    //    string sqlGetName =
    //        "SELECT [AccountName] " +
    //        "FROM [tblCustomerAccounts] " +
    //        $"WHERE ["

    //    return acctName;
    //}

    public static int ModelToID(this string modelType) {
        int modelID; // = GlobalVariables._reverseVersionLookUP[modelType];


        return modelID = GlobalVariables._reversemodelLookUP[modelType]; ;
    }

    public static bool AddNewAccount(this DataRow dr) {
        AccessService accessDB = new();
        string sqlAccount =
            "INSERT INTO [tblCustomerAccounts] ([AccountName], [MainAccount_PK], [ProCareRepLU_cbo], [SalesRepLU_cbo]) " +
            $"VALUES ('{dr["Customer"]}', {dr["Account"]}, '{dr["Tech"]}', '{dr["Sales Rep"]}' )";

        try {
            accessDB.AddToAccount(sqlAccount);
        }
        catch (Exception) {
            //return false;
            throw;
        }

        return true;
    }

    public static bool AddNewContract(this DataRow dr) {
        AccessService accessDB = new();
        string sqlContract =
           "INSERT INTO [tblServicePlan] ([ShipToAccountNumber], [AccountName_cbo], [ServicePlanNumber], " +
           "[ServicePlanStatus], [ServicePlanStartDate], [ServicePlanExpireDate]) " +
           $"VALUES ({dr["Account"]}, '{dr["Customer"]}', '{dr["Contract"]}', '{dr["Status"]}', {dr["Cvg Start"]}, {dr["Cvg End"]})";

        try {
            accessDB.AddToAccount(sqlContract);
        }
        catch (Exception) {
            //return false;
            throw;
        }
        
        return true;
    }

    public static bool AddNewSerialNumber(this DataRow dr) {
        AccessService accessDB = new();

        double customerAccountNumber = (double)dr["Account"];
        int mainCustID = customerAccountNumber.getMainAccountOrCustID();

        string sqlSerialNumber =
            "INSERT INTO [tblEquipment] ([CustomerAccountID_FK], [ServicePlanID_FK], [EquipmentSerial], [ServicePlanStatusLU_cbo], [ModelDescription]) " +
            $"VALUES ({mainCustID}, {dr["Contract"].ToString().GetServicePlanID()}, '{dr["Serial"]}', '{dr["Status"]}', '{dr["product"]}')";

        try {
            accessDB.AddToAccount(sqlSerialNumber);
        }
        catch (Exception) {
            //return false;
            throw;
        }
        
        return true;
    }
    public static bool CopytoClipboard(this string number) {
        Clipboard.SetText(number);
        return true;
    }

    //public static bool AddContractToDatabase(this string contract, string sqlInsert) {
    //    AccessService accessDB = new();
    //    //string sqlInsert =
    //    //   "INSERT INTO [tblServicePlan] ([ServicePlanNumber]) " +
    //    //   $"VALUES ('{contract}')";


    //    //sqlInsert =
    //    //            "INSERT INTO [tblCustomerAccounts]([AccountName], [MainAccount_PK], " +
    //    //            "[ProCareRepLU_cbo], [SalesRepLU_cbo]) " +
    //    //            $"VALUES ('{drRow["Customer"]}', {drRow["Account"]}, " +
    //    //            $"'{drRow["Tech"]}', '{drRow["Sales Rep"]}')";

    //    accessDB.AddToAccount(sqlInsert);

    //    return true;

    //}
}
