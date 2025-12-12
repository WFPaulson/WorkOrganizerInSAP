using System.Windows.Controls.Primitives;

namespace ContractWork.MVVM.CustomerDetails;
public static class CustomerDetailsSQLStatements {

    //public static string CustomerAccountEquipmentList(int ID) {
    //    return "SELECT tblEquipment.EquipmentID, tblEquipment.CustomerAccountID_FK, tblEquipment.ServicePlanID_FK, " +
    //                    "tblEquipment.EquipmentSerial, tblEquipment.ModelID, tblEquipment.VersionID, tblEquipment.ServicePlanStatusLU_cbo, " +
    //                    "tblEquipment.PMMonthNumber, tblEquipment.PMMonth, tblEquipment.PMCompleted, tblEquipment.DeviceUnavailable, " +
    //                    "tblServicePlan.ServicePlanID, tblServicePlan.AccountName_cbo, tblServicePlan.ServicePlanStatus, tblServicePlan.ServicePlanNumber, " +
    //                    "tblServicePlan.ServicePlanExpireDate, tblServicePlan.POExpireDate, tblServicePlan.Expired, tblServicePlan.POExpired " +
    //                    "FROM(tblServicePlanStatus_LU RIGHT JOIN tblServicePlan ON tblServicePlanStatus_LU.ServicePlanStatus = tblServicePlan.ServicePlanStatus) " +
    //                    "INNER JOIN(tblEquipment INNER JOIN tblEquipmentVersion_LU ON tblEquipment.VersionID = tblEquipmentVersion_LU.VersionID) " +
    //                    "ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +
    //                    $"WHERE tblEquipment.CustomerAccountID_FK = {ID}";
    //}

    public static string CustomerAccountEquipmentList(int ID) {
        return "SELECT tblEquipment.EquipmentID, tblEquipment.CustomerAccountID_FK, tblEquipment.ServicePlanID_FK, " +
            "tblEquipment.EquipmentSerial, tblEquipment.ModelID, tblEquipment.VersionID, tblEquipment.ServicePlanStatusLU_cbo, " +
            "tblEquipment.PMMonthNumber, tblEquipment.PMMonth, tblEquipment.PMCompleted, tblEquipment.PMCompletedCheck, tblEquipment.DeviceUnavailable, " +
            "tblServicePlan.ServicePlanID, tblServicePlan.AccountName_cbo, tblServicePlan.ServicePlanStatus, " +
            "tblServicePlan.ServicePlanNumber, tblServicePlan.ServicePlanExpireDate, tblServicePlan.Expired " +
            "FROM tblServicePlan INNER JOIN tblEquipment ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +
            $"WHERE (tblEquipment.CustomerAccountID_FK)= {ID} ";
    }

        public static string CustomerServicePlanEquipmentList(int ID) {
        return "SELECT tblServicePlan.ServicePlanID, tblServicePlan.AccountName_cbo, tblServicePlan.ServicePlanNumber, " +
                        "tblEquipment.EquipmentSerial, tblEquipment.ModelID, tblEquipment.VersionID, tblEquipment.ServicePlanStatusLU_cbo, " +
                        "tblEquipment.PMCompleted, tblEquipment.PMMonth, tblEquipment.Archive " +
                        "FROM tblServicePlan INNER JOIN tblEquipment ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +
                        $"WHERE (tblServicePlan.ServicePlanID) = {ID} ";
    }

    public static string CustomerEquipmentDevice(int ID) {
        return "SELECT tblEquipment.EquipmentID, tblEquipment.CustomerAccountID_FK, tblServicePlan.AccountName_cbo, tblEquipment.EquipmentSerial, " +
            "tblEquipment.ABBAString, tblEquipment.ManufactureDate, tblEquipment.ServicePlanStatusLU_cbo, tblServicePlan.ServicePlanNumber," +
            "tblEquipment.ModelID, tblEquipment.VersionID " +
            "FROM(tblServicePlanStatus_LU RIGHT JOIN tblServicePlan ON tblServicePlanStatus_LU.ServicePlanStatus = tblServicePlan.ServicePlanStatus) " +
            "INNER JOIN(tblEquipment INNER JOIN tblEquipmentVersion_LU ON tblEquipment.VersionID = tblEquipmentVersion_LU.VersionID) " +
            "ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +
            $"WHERE tblEquipment.EquipmentID = {ID}";
    }

    public static string sqlEquipmentList(int ID) {
        return "SELECT [EquipmentSerial], [ServicePlanID_FK], [ServicePlanStatusLU_cbo] " +       //, [ServicePlanStatus], 
                "FROM [tblEquipment] " +
                $"WHERE [CustomerAccountID_FK] = {ID} ";
    }

    public static string sqlEquipmentListbyServicePlan(int ID) {
        return "SELECT [EquipmentSerial], [ServicePlanID_FK], [ServicePlanStatusLU_cbo] " +       //, [ServicePlanStatus], 
                "FROM [tblEquipment] " +
                $"WHERE [ServicePlanID_FK] = {ID} ";
    }

    public static string sqlServicePlanList(string _name) {
        return "SELECT [ServicePlanID], [ServicePlanNumber], [ServicePlanStatus], [ServicePlanExpireDate], [POExpireDate] " +       //, [ServicePlanStatus], 
                "FROM [tblServicePlan] " +
                $"WHERE [AccountName_cbo] = '{_name}'";
    }

    public static string getCustomerAccountIDs() {
        return "SELECT [CustomerAccountID], [AccountNameTXT_FK] " +
                "FROM [tblCustomerAccounts] ";
    }

    public static string getServicePlanIDs() {
        return "SELECT [ServicePlanID], [AccountName_cbo] " +
                "FROM [tblServicePlan] ";

    }
}
