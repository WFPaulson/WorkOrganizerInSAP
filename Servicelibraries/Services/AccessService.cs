using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using Microsoft.VisualBasic;

namespace Servicelibraries;
public partial class AccessService : ObservableObject {

    #region Constant statements
    public const string accessExt = "Access files | *.accdb;*.mdb;*.laccdb;*.adp;*.mda;*.accda;*.mde;*.accde;*.ade";
    private const string dbConnString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=";
    private const string dbSecurity = @";Persist Security Info=False;";

    public static bool? ViewCurrent = false;
    public static bool? ViewArchivedOnly = true;
    public static bool? ViewAll = null;

    public static string AccessFileLocation = string.Empty;


    #endregion

    #region Property statements
    public OleDbConnection dbConn;
    public OleDbDataAdapter dbData;
    public OleDbCommand dbCommand;
    #endregion

    #region Observable statements
    [ObservableProperty]
    private string _connResult;

    [ObservableProperty]
    private string _sQLStatement;

    [ObservableProperty]
    private ObservableCollection<string> _tblList;

    [ObservableProperty]
    private ObservableCollection<string> _tblColList;

    [ObservableProperty]
    private DataTable _dBDatatable;

    //[ObservableProperty]
    //private static string _fileLocation;
    //[ObservableProperty]
    //private static string _inventoryFileLocation;  //private static string _accessFileLocation;


    [ObservableProperty]
    private bool? _viewArchived = ViewCurrent;


    #endregion



    public bool OpenConn() {
        //DbLocation = AccessFileLocation;
        //string location = DbLocation ?? G        .AccessFileLocation;
        if (string.IsNullOrEmpty(AccessFileLocation)) {
            AccessFileLocation = "nothing";
        }
        if (dbConn.State != ConnectionState.Open) {
            try {
                dbConn.ConnectionString = dbConnString + AccessFileLocation + dbSecurity; // need to add filelocation
                dbConn.Open();
                ConnResult = "Success";
                return true;
            }
            catch (Exception ex) {
                MessageBox.Show(AccessFileLocation);
                MessageBox.Show(ex.Message);
                ConnResult = "Failed";
                return false;
            }
        }
        else return true;
    }

    public (DataTable, string) OpenAccessFile(string fullStatement, string fileLocation = "", string defaultSelection = "noDefault") {
        string DidDBFail = "Failed";
        SQLStatement = fullStatement;
        //AccessFileLocation = fileLocation;
        AccessFileLocation = fileLocation ?? AccessFileLocation;

        using (dbConn = new()) {
            getTableNameList(defaultSelection);
            if (TblList.ocIsNullorEmpty()) {
                DBDatatable = null;
                DidDBFail = "Failed";
                return (DBDatatable, DidDBFail);
            }

            if (OpenConn()) {
                DidDBFail = "Success";
                DBDatatable = new();
                try {
                    using (dbData = new(
                        SQLStatement,
                        dbConn)) {
                        dbData.Fill(DBDatatable);
                        getTblColumnNameList();
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show($"Error issue: {ex.Message}");
                }
            }
            else DidDBFail = "Failed";
        }

        CloseDB();
        return (DBDatatable, DidDBFail);
    }

    public void getTableNameList(string defaultSelection = "nodefault") {
        DataTable usertables = new();
        string item;
        TblList = new();
        this.OpenConn();

        usertables = this.dbConn.GetSchema("Tables");
        foreach (DataRow row in usertables.Rows) {
            item = row["Table_Name"].ToString();
            if (item.StartsWith("tbl")) { TblList.Add(item); }
        }
    }

    private ObservableCollection<string> getTblColumnNameList() {
        TblColList = new();
        TblColList.Clear();
        foreach (DataColumn col in DBDatatable.Columns) {
            TblColList.Add(col.ColumnName);
        }
        return TblColList;
    }

    public DataTable RefreshDB(string SQLString) {
        DataTable dt = new();
        using (dbConn = new()) {
            if (OpenConn()) {
                using (dbData = new OleDbDataAdapter(SQLString, dbConn)) {
                    dbData.Fill(dt);
                }
            }
            else MessageBox.Show("Failed Refresh DB ");
        }
        return dt;
    }

    public DataTable CloseAccess() {
        DBDatatable.Clear();
        DBDatatable.Columns.Clear();
        DBDatatable.Dispose();
        CloseDB();
        DBDatatable = new DataTable();
        return DBDatatable;

    }


    public void CloseDB() {
        if (dbConn != null) {
            if (dbConn.State == ConnectionState.Open) {
                try {
                    dbConn.Close();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                    ConnResult = "Failed";
                }
            }
        }
    }


    public bool AddToAccount(string SQLInsert = null, string fileLocation = null, string bypassMessage = "No",
                             params SqlParameter[] parameters) {
        AccessFileLocation = fileLocation ?? AccessFileLocation;
        //fileLocation ??= AccessFileLocation;

        try {
            using (dbConn = new OleDbConnection()) {
                if (OpenConn()) {
                    using (OleDbCommand xlCommand = new OleDbCommand()) {
                        xlCommand.Connection = dbConn;
                        xlCommand.CommandText = SQLInsert;

                        foreach (SqlParameter item in parameters) {
                            xlCommand.Parameters.Add(item.ParameterName, item.Value);
                        }

                        xlCommand.ExecuteNonQuery();
                    }
                }
                else MessageBox.Show("Failed Update XLConn");
                dbConn.Close();
            }
        }
        catch (Exception ex) {
            if (bypassMessage == "No") {
                MessageBox.Show($"The issue is: {ex.Message}");
            }
            return false;
        }
        return true;
    }


    public DataTable FetchDBRecordRequest(string fullstatement) {
        string tmpSQL = string.Empty;
        DBDatatable = new();

        try {
            using (dbConn = new()) {
                if (OpenConn()) {

                    using (dbData = new OleDbDataAdapter(fullstatement, dbConn)) {
                        dbData.Fill(DBDatatable);
                    }
                }
                else MessageBox.Show("Failed RefreshDB");
            }
        }
        catch (Exception ex) {
            MessageBox.Show($"This error is caused by: {ex.Message}");
        }


        return DBDatatable;
    }


    public void CreateNewCustomerAccount(string? accountName = null, string? accountNumber = null) {

    }

    public List<string> GetListOfNames(string? accountName) {
        List<string> list = new();
        string containPartial = accountName.Substring(0, accountName.IndexOf(" "));
        string sqlListOfNames = "SELECT * " +
            "FROM [tblCustomerAccounts] " +
            $"WHERE [AccountName] LIKE '%{containPartial}%'";
        DataTable dt = RefreshDB(sqlListOfNames);

        foreach (DataRow row in dt.Rows) {
            list.Add(row["AccountName"].ToString());
        }

        return list;
    }


    //public void UpdateDB<TCustomer>(string fieldChange, TCustomer Customer, string[] updateParameters,
    //        [CallerArgumentExpression("fieldChange")] string nameoffieldChange = null) {

    //    AccessService accessDB = new();
    //    DataTable dt = new();
    //    string updatePMDate = string.Empty;
    //    string getID = string.Empty;

    //    string _nameOfField = nameoffieldChange;

    //    var customerProperty = Customer.GetType().GetProperty(updateParameters[2]).GetValue(Customer);

    //    switch (_nameOfField) {
    //        case "Notes":
    //        case "_pmdue":
    //        case "_pmMonthName":
    //        case "ServicePlanNumber":
    //            //if (!string.IsNullOrEmpty("Notes") || !string.IsNullOrEmpty("_pmdue")) {
    //            updatePMDate =
    //                $"UPDATE [{updateParameters[0]}] " +
    //                $"SET [{updateParameters[1]}] = '{fieldChange}' " +
    //                $"WHERE [{updateParameters[2]}] = {customerProperty}";
    //            // }

    //            break;

    //        case "PMCompleted":
    //            DateTime parsedDate;
    //            string setStatement = string.Empty;

    //            if (fieldChange != string.Empty) {
    //                parsedDate = DateTime.Parse(fieldChange);
    //                setStatement = $"SET [{updateParameters[1]}] = '{parsedDate}' ";
    //            }
    //            else { setStatement = $"SET [{updateParameters[1]}] = null "; }

    //            updatePMDate =
    //            $"UPDATE [{updateParameters[0]}] " +
    //            setStatement +
    //            $"WHERE [{updateParameters[2]}] = {customerProperty}";

    //            break;

    //        case "ServicePlanStatus":
    //            string SetStatement = $"SET [{updateParameters[1]}] = '{fieldChange}' ";

    //            if (updateParameters[0] == "tblServicePlan") {
    //                if (fieldChange == "Expired") {
    //                    SetStatement = $"SET [{updateParameters[1]}] = '{fieldChange}', [Expired] = true ";

    //                }
    //            }

    //            updatePMDate =
    //                $"UPDATE [{updateParameters[0]}] " +
    //                SetStatement +
    //                $"WHERE [{updateParameters[2]}] = {customerProperty}";
    //            break;
    //    }

    //    if (!string.IsNullOrEmpty(updatePMDate)) {
    //        accessDB.AddToAccount(SQLInsert: updatePMDate);
    //    }
    //}

    public static void UpdateCustomerAccountDetails<TCustomer>(int fieldChange, TCustomer Customer, string[] updateParameters,
            [CallerArgumentExpression("fieldChange")] string nameoffieldChange = null) {

        AccessService accessDB = new();
        DataTable dt = new DataTable();
        string updatePMDate = string.Empty;
        string getID = string.Empty;

        string _nameOfField = nameoffieldChange;

        var customerProperty = Customer.GetType().GetProperty(updateParameters[2]).GetValue(Customer);

        switch (_nameOfField) {
            case "_pmMonthNumber":
            case "ModelID":
            case "VersionID":
            case "ServicePlanID":
                //if (!string.IsNullOrEmpty("_pmMonthNumber")) {
                updatePMDate =
                    $"UPDATE [{updateParameters[0]}] " +
                    $"SET [{updateParameters[1]}] = {fieldChange} " +
                    $"WHERE [{updateParameters[2]}] = {customerProperty}";
                //}

                break;
        }

        if (!string.IsNullOrEmpty(updatePMDate)) {
            accessDB.AddToAccount(SQLInsert: updatePMDate);
        }

    }

    public static void UpdateCustomerAccountDetails<TCustomer>(string fieldChange, TCustomer Customer, string[] updateParameters,
            [CallerArgumentExpression("fieldChange")] string nameoffieldChange = null) {

        AccessService accessDB = new();
        DataTable dt = new DataTable();
        string updatePMDate = string.Empty;
        string getID = string.Empty;

        string _nameOfField = nameoffieldChange;

        var customerProperty = Customer.GetType().GetProperty(updateParameters[2]).GetValue(Customer);


        switch (_nameOfField) {
            case "Notes":
                updatePMDate = null;

                string updateNotes =
               $"UPDATE [{updateParameters[0]}] " +
               $"SET [{updateParameters[1]}] = @notes " +
               $"WHERE [{updateParameters[2]}] = {customerProperty}";

                SqlParameter notesParameter = new SqlParameter("@notes", fieldChange);

                accessDB.AddToAccount(SQLInsert: updateNotes, parameters: notesParameter);
                break;

            case "_pmdue":
            case "PmMonthName":
            case "ServicePlanNumber":
                //if (!string.IsNullOrEmpty("Notes") || !string.IsNullOrEmpty("_pmdue")) {
                updatePMDate =
                    $"UPDATE [{updateParameters[0]}] " +
                    $"SET [{updateParameters[1]}] = '{fieldChange}' " +
                    $"WHERE [{updateParameters[2]}] = {customerProperty}";
                // }

                break;

            case "PMCompleted":
                DateTime parsedDate;
                string setStatement = string.Empty;

                if (fieldChange != string.Empty) {
                    parsedDate = DateTime.Parse(fieldChange);
                    setStatement = $"SET [{updateParameters[1]}] = '{parsedDate}' ";
                }
                else { setStatement = $"SET [{updateParameters[1]}] = null "; }

                updatePMDate =
                $"UPDATE [{updateParameters[0]}] " +
                setStatement +
                $"WHERE [{updateParameters[2]}] = {customerProperty}";

                break;

            case "ServicePlanStatus":
                string SetStatement = $"SET [{updateParameters[1]}] = '{fieldChange}' ";

                if (updateParameters[0] == "tblServicePlan") {
                    if (fieldChange == "Expired") {
                        SetStatement = $"SET [{updateParameters[1]}] = '{fieldChange}', [Expired] = true ";

                    }
                }

                updatePMDate =
                    $"UPDATE [{updateParameters[0]}] " +
                    SetStatement +
                    $"WHERE [{updateParameters[2]}] = {customerProperty}";
                break;
        }

        if (!string.IsNullOrEmpty(updatePMDate)) {
            accessDB.AddToAccount(SQLInsert: updatePMDate);
        }

    }

    public static void UpdateCustomerAccountDetails<TCustomer>(bool fieldChange, TCustomer Customer, string[] updateParameters,
            [CallerArgumentExpression("fieldChange")] string nameoffieldChange = null) {
        AccessService accessDB = new();
        DataTable dt = new DataTable();
        string updatePMDate = string.Empty;
        string getID = string.Empty;

        string _nameOfField = nameoffieldChange;

        var customerProperty = Customer.GetType().GetProperty(updateParameters[2]).GetValue(Customer);


        switch (_nameOfField) {
            case "DeviceUnavailable":

                updatePMDate =
                   $"UPDATE [{updateParameters[0]}] " +
                   $"SET [{updateParameters[1]}] = {fieldChange} " +
                   $"WHERE [{updateParameters[2]}] = {customerProperty}";
                // }

                break;

        }
        if (!string.IsNullOrEmpty(updatePMDate)) {
            accessDB.AddToAccount(SQLInsert: updatePMDate);
        }


    }


    public void SetFilePath(string fileName, string filePath) {

    }
}

    #region Commented Methods
    //public DataTable FetchDBRecordRequest(string selection = null, string tblName = null, string filterColName = null,
    //                                            string strfilterItem = null, int intfilterItem = 0, string fullstatement = null,
    //                                            string dblocation = null) {
    //    string tmpSQL = string.Empty;
    //    DBDatatable = new DataTable();

    //    if (fullstatement == null) {

    //        //? if (selection != "*") { selection = "[" + selection + "]"; }

    //        tmpSQL = $"SELECT {selection} " +
    //                $"FROM [{tblName}] ";

    //        if (filterColName != null) {
    //            if (strfilterItem != null) { tmpSQL += $"WHERE [{filterColName}] = '{strfilterItem}' "; }
    //            else if (intfilterItem != 0) { tmpSQL += $"WHERE [{filterColName}] = {intfilterItem} "; }
    //        }

    //        if (selection == "*") {
    //            GetOrderByList(tblName);
    //            tmpSQL += $"ORDER BY {orderBy}";
    //        }
    //        else { tmpSQL += $"ORDER BY {selection}"; }

    //    }
    //    else tmpSQL = fullstatement;
    //    try {
    //        using (dbConn = new OleDbConnection()) {
    //            if (OpenConn()) {

    //                using (dbData = new OleDbDataAdapter(tmpSQL, dbConn)) {
    //                    dbData.Fill(DBDatatable);
    //                }
    //            }
    //            else MessageBox.Show("Failed RefreshDB");
    //        }
    //    }
    //    catch (Exception ex) {
    //        MessageBox.Show($"This error is caused by: {ex.Message}");
    //    }


    //    return DBDatatable;
    //}

    #endregion



//public class AccessFileLocation() {
//    public string path { get; set; }
//    public string filename { get; set; }

//    public AccessFileLocation() {

//    }

//}