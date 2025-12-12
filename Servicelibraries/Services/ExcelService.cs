using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.IO;


namespace Servicelibraries;
public partial class ExcelService : ObservableObject {

    #region Constant statements
    public const string excelExt = "Excel files | *.xlsx; *.xlsm";
    private const string xlConnString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=";
    private const string xlExtended = @"; Extended Properties = 'Excel 12.0 xml;";

    #endregion

    #region Property statements
    public OleDbConnection xlConn;
    public OleDbDataAdapter xlData;

    public static string ExcelFileLocation;

    public static bool? ViewCurrent = false;
    public static bool? ViewArchiveOnly = true;
    public static bool? ViewAll = false;


    #endregion

    #region Observable statements
    [ObservableProperty]
    private DataTable _xLDatatable;

    [ObservableProperty]
    private bool _headerRowStatus;

    [ObservableProperty]
    private string _connResult;

    [ObservableProperty]
    private string _sQLStatement = string.Empty;

    [ObservableProperty]
    private bool? _viewArchived = ViewCurrent;

    [ObservableProperty]
    private ObservableCollection<string> _filterBy;

    [ObservableProperty]
    private string _pickedFilterColumn;

    #endregion

    public bool OpenConn(string dbLocation, bool headerRow = false) {
        string xlHeaderRow = (headerRow) ? @"HDR=Yes'" : @"HDR=No'";

        if (HeaderRowStatus != headerRow && xlConn.State is ConnectionState.Open) CloseXL();
        if (xlConn.State != ConnectionState.Open) {
            try {
                xlConn.ConnectionString = xlConnString + dbLocation + xlExtended + xlHeaderRow;
                xlConn.Open();
                HeaderRowStatus = headerRow;
                return true;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        else {
            HeaderRowStatus = headerRow;
            return true;
        }
    }

    public (DataTable, bool) OpenExcelFile(string fullStatement, string excelFileName = "") {
        bool didItFail = true;
        ExcelFileLocation = excelFileName ?? ExcelFileLocation;
        SQLStatement = fullStatement;

        using (xlConn = new OleDbConnection()) {
            if (OpenConn(ExcelFileLocation, true)) {

                XLDatatable = new ();
                using (xlData = new OleDbDataAdapter(SQLStatement, xlConn)) {
                    try {
                        xlData.Fill(XLDatatable);
                        didItFail = false;
                    }
                    catch (OleDbException ex) {
                        MessageBox.Show($"All Error message: {ex.Message}");
                        //MessageBox.Show($"It looks like I could not find: {excelFileName}");
                        throw;
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"All Error message: {ex.Message}");
                    }
                }
            }
        }

        CloseXL();
        return (XLDatatable, didItFail);
    }

    public (DataTable, bool) RefreshSpreadSheet(string refreshStatement) {
        DataTable spreadSheet = new();
        bool didItPass = false;

        using ( xlConn = new OleDbConnection()) {
            if (OpenConn(ExcelFileLocation, true)) {
                using(xlData = new OleDbDataAdapter(refreshStatement, xlConn)) {
                    try {
                        xlData.Fill(spreadSheet);
                        didItPass = true;
                    }
                    catch (Exception ex) {

                        MessageBox.Show($"Error Message: {ex} ");
                        didItPass = false;
                    }
                    
                }
            }
            else didItPass=false;
        }

        return (spreadSheet, didItPass);
    }

    public void CloseXL() {
        if (xlConn != null) {
            if (xlConn.State == ConnectionState.Open) {
                try {
                    xlConn.Close();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                    ConnResult = "Failed";
                }
            }
        }
    }

    public DataTable CloseExcel() {
        XLDatatable.Clear();
        XLDatatable.Dispose();
        CloseXL();
        XLDatatable = new DataTable();
        return XLDatatable;
    }

    public void CreateWBandExportDatatable(DataTable exportDatatableToWB = null, string fName = null) {
 
        fName ??= "Paulson.xlsx";
        var filename = new FileInfo(@$"c:\JoesPMList\{fName} ");
        //DeleteIfExists(filename);

        using (var package = new ExcelPackage(filename)) {
            var sheet = package.Workbook.Worksheets.Add("");

            /***** Load from DataTable ***/
            // Import the DataTable using LoadFromDataTable
            sheet.Cells["A1"].LoadFromDataTable(exportDatatableToWB, true, TableStyles.Dark11);
            package.Save();
        }
    }

    private void DeleteIfExists(FileInfo filename) {
        if (filename.Exists) { filename.Delete(); }
    }
}
