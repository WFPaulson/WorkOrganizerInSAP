using System.Threading.Tasks;
using ContractWork.csproj.ExtensionMethods;
using ContractWork.MVVM.PMandInventory;
using ContractWork.MVVM.TaskManager;
using Clipboard = System.Windows.Clipboard;

namespace ContractWork.MVVM.Dashboard;

public partial class DashboardViewModel : ObservableObject {
    AccessService accessDB;
    
    public static Dictionary<string, string> fileLocatinDict = new();

    #region Observable Property Statements
    [ObservableProperty]
    private int _currentProgress;

    [ObservableProperty]
    private Visibility _progressBarVisibility = Visibility.Hidden;

    [ObservableProperty]
    private ObservableCollection<string> _combolist;

    [ObservableProperty]
    private string _notesNonSpecific;

    //[ObservableProperty]
    //private int _cmbWidthForText = 240;

    [ObservableProperty]
    private string _selectedUAReason = $"Unavailable for service, will honor PM if customer calls back to schedule.";    //"...Puck an item";
    //partial void OnSelectedUAReasonChanged(string value) {
    //    CmbWidthForText = value.Length + 300;
    //}
    
    


    #endregion

    #region Property Statements
    //public static string AccessFileLocation { get; set; }
    //public static string AccessFolderLocation { get; set; }

    //public static string ExcelFileLocation { get; set; }
    //public static string ExcelFolderLocation { get; set; }

    private bool PMInFocus = false;
    private bool ServiceNotesInFocus = false;
    public bool SomeTextIsFocused { get; set; }

    public int PMTextInt { get; set; } = 1;
    public string PMText { get; set; } = "A034, A036 User Test Fails";

    public int ServiceNotesInt { get; set; } = 2;
    public string ServiceNotesText { get; set; } = "A PM PIP was completed per service manual instructions." + GL.nl +
        "Worn items were inspected and replaced as needed. " + GL.nl +
        "Device passed inspection, calibrations confirmed, software updated (if required)." + GL.nl +
        "A new calibration sticker was applied, and device was returned to customer for deployment as needed.";

    public string ServiceNotesUAText { get; set; } = $"Unavailable for service, will honor PM if customer calls back to schedule.";

    //public string SelectedUAReason {
    //    get { return SelectedUAReason; }
    //    set {
    //        CmbWidthForText = 160;
    //    }
    //}


    #endregion


    public NavigationService _navigationService { get; set; }

    public DashboardViewModel(NavigationService navigationService) {
        _navigationService = navigationService;
        accessDB = new();

        RunInitialSetup();

    }

    private void RunInitialSetup() {
        int status = 0;

        GetUAComboList();
 
        CheckFileLocations();

        DisplayFileLocations();

        UpdateFileLocations();
        
        GetABBAStringLists();

        ImportNotesNonSpecific();


    }

    private void GetUAComboList() {
        Combolist = new ObservableCollection<string> {
            "Unavailable for service, will honor PM if customer calls back to schedule.",
            "PM - Duplicate"
        };
    }

    private void ImportNotesNonSpecific() {
        string sqlNoContractNotes =
            "SELECT [Notes] " +
            "FROM [tblServicePlan] " +
            "WHERE [ServicePlanID] = 221";
        DataTable dt = accessDB.FetchDBRecordRequest(sqlNoContractNotes);
        NotesNonSpecific = dt.Rows[0]["Notes"].ToString();

    }

    [RelayCommand]
    private void LostFocusCompareValue(string notesToWrite) {
        AccessService db = new();
        string sqlUpdateNotes =
            "UPDATE [tblServicePlan] " +
            $"SET [Notes] = '{notesToWrite}' " +
            $"WHERE [ServicePlanID] = 221";

        //AccessService.UpdateCustomerAccountDetails<NewTaskContent>(note, this, notesParams);
        db.AddToAccount(SQLInsert: sqlUpdateNotes);
    }


    [RelayCommand]
    private void ChangeFileLocation() {

        FileSelectionWindow SelectFiles = new() {
            DataContext = _navigationService.CurrentViewModel = new FileSelectionViewModel(_navigationService)
        };
        SelectFiles.ShowDialog();

        UpdateFileLocations();
    }
    private void UpdateFileLocations() {
        AccessService.AccessFileLocation = fileLocatinDict["AccessFilePath"];
        ExcelService.ExcelFileLocation = fileLocatinDict["ExtendedAssetsFilePath"];
        //InventoryFile.AccessFileLocation = InventoryFileName;
        //excelWB.SetFilePath(ContractFileName);
        //excelWB.SetFilePath(AssetFileName);


    }

    private void CheckFileLocations() {
        fileLocatinDict = GL.SetUserConfig("UserConfig.json", CreateDefaultUserConfig);
    }
    private static string CreateDefaultUserConfig() {
        string defConfig = "defConfig";
        UserConfigModel userDefaultConfig = new(defConfig);

        string classToJson = userDefaultConfig.toJson(Formatting.Indented);
        return classToJson;
    }

    private void DisplayFileLocations() {

        UpdateFileLocations();
       
    }

    private void GetABBAStringLists() {
        GV.LP20ABBAList = new();
        GV.LP15ABBAList = new();
        GV.LPCR2ABBAList = new();

        string sqlABBAlist =
            "SELECT DISTINCT ABBAString " +
            "FROM tblEquipment ";

        string sqlNew = sqlABBAlist + "WHERE(((ABBAString)Like '%20%'))";
        DataTable dt = accessDB.FetchDBRecordRequest(fullstatement: sqlNew);

        foreach (DataRow Name in dt.Rows) {
            if (string.IsNullOrEmpty(Name["ABBAString"].ToString())) { continue; }
            GV.LP20ABBAList.Add(Name["ABBAString"].ToString());
        }

        //sqlNew = string.Empty;
        sqlNew = sqlABBAlist + "WHERE(((ABBAString)Like '%15%'))";
        dt = accessDB.FetchDBRecordRequest(fullstatement: sqlNew);
        foreach (DataRow Name in dt.Rows) {
            if (string.IsNullOrEmpty(Name["ABBAString"].ToString())) { continue; }
            GV.LP15ABBAList.Add(Name["ABBAString"].ToString());
        }

        //sqlNew = string.Empty;
        sqlNew = sqlABBAlist + "WHERE(((ABBAString)Like '%CR2%'))";
        dt = accessDB.FetchDBRecordRequest(fullstatement: sqlNew);
        foreach (DataRow Name in dt.Rows) {
            if (string.IsNullOrEmpty(Name["ABBAString"].ToString())) { continue; }
            GV.LPCR2ABBAList.Add(Name["ABBAString"].ToString());
        }

    }


    [RelayCommand]
    private void ServicePlanMaintenance() {
        
        ContractAndAssetsWindow ServicePlanMaintenance = new() {
            DataContext = _navigationService.CurrentViewModel = new ContractAndAssetsViewModel(_navigationService)
        };
        ServicePlanMaintenance.ShowDialog();
        
        UpdateFileLocations();
    }

    [RelayCommand]
    private void OpenCustomerAccountView() {
        
        accessDB = new();

        CustomerAccountSelectionWindow AccountsWindow = new CustomerAccountSelectionWindow() {
            DataContext = _navigationService.CurrentViewModel = new 
                            CustomerAccountSelectionViewModel(_navigationService, null)
        };
        AccountsWindow.Show();
    }


    [RelayCommand]
    private void ContractAndWarranty() {

        ContractsAndWarrantyWindow ContractsAndWarranty = new ContractsAndWarrantyWindow() {
            DataContext = _navigationService.CurrentViewModel = new ContractsAndWarrantyViewModel(_navigationService)
        };
        //need to reset to "" change from "Failed" after the first Failed

        if (GV._popupClosedByX == "Failed") {
            ContractsAndWarranty.Close();
        }
        else ContractsAndWarranty.Show();

    }

    [RelayCommand]
    private void TaskManager()
    {
        TaskManagerWindow TaskManager = new TaskManagerWindow()
        {
            DataContext = _navigationService.CurrentViewModel = new TaskManagerViewModel(_navigationService)
        };

        TaskManager.Show();

    }

    [RelayCommand]
    private void Inventory(){
        InventoryWindow Inventory = new InventoryWindow()
        {
            DataContext = _navigationService.CurrentViewModel = new InventoryViewModel(_navigationService)
        };

        Inventory.Show();

    }

    [RelayCommand]
    private void CustomerPMSelect() {
        ProgressBarVisibility = Visibility.Visible;

        PMSchedulingWindow CustomerPMSelect = new () {
            DataContext = _navigationService.CurrentViewModel = new PMSchedulingViewModel(_navigationService)
        };
        CustomerPMSelect.Show();

        ProgressBarVisibility = Visibility.Hidden;
    }


    #region Relay Commands

    [RelayCommand]
    private void MouseLeave(string textToCopy) {
        //if (textToCopy == "PMText") {
        //    if (PMInFocus) { return; }
        //    PmTextBoxBackground = Brushes.White;
        //    PmTextBoxForeground = Brushes.Black;
        //    PmTextBoxBorderBrush = Brushes.Black;
        //}

        //else if (textToCopy == "ServiceNotes") {
        //    if (ServiceNotesInFocus) { return; }
        //    ServiceNotesBackground = Brushes.White;
        //    ServiceNotesForeground = Brushes.Black;
        //    ServiceNotesBorderBrush = Brushes.Black;
        //}
    }

    [RelayCommand]
    private void MouseEnter(string textToCopy) {
        //if (textToCopy == "PMText") {
        //    if (ServiceNotesInFocus) { return; }
        //    PmTextBoxBackground = Brushes.Black;
        //    PmTextBoxForeground = Brushes.White;
        //    PmTextBoxBorderBrush = Brushes.White;
        //}

        //else if (textToCopy == "ServiceNotes") {
        //    if(PMInFocus) { return; }
        //    ServiceNotesBackground = Brushes.Black;
        //    ServiceNotesForeground = Brushes.White;
        //    ServiceNotesBorderBrush = Brushes.White;
        //}
    }

    [RelayCommand]
    private void TextBoxLostFocus(string textToCopy) {
        //if (textToCopy == "PMText") {
        //    PmTextBoxBackground = Brushes.White;
        //    PmTextBoxForeground = Brushes.Black;
        //    PmTextBoxBorderBrush = Brushes.Black;
        //    PMInFocus = false;
        //}

        //else if (textToCopy == "ServiceNotes") {
        //    ServiceNotesBackground = Brushes.White;
        //    ServiceNotesForeground = Brushes.Black;
        //    ServiceNotesBorderBrush = Brushes.Black;
        //    ServiceNotesInFocus = false;
        //}
    }

    [RelayCommand]
    private void TextBoxGotFocus(string textToCopy) {
        //if (textToCopy == "PMText") {
        //    PmTextBoxBackground = Brushes.Black;
        //    PmTextBoxForeground = Brushes.White;
        //    PmTextBoxBorderBrush = Brushes.White;
        //    Clipboard.SetText(PMText);
        //    PMInFocus = true;
        //    TextBoxLostFocus("ServiceNotes");
        //}

        //else if (textToCopy == "ServiceNotes") {
        //    ServiceNotesBackground = Brushes.Black;
        //    ServiceNotesForeground = Brushes.White;
        //    ServiceNotesBorderBrush = Brushes.White;
        //    Clipboard.SetText(ServiceNotesText);
        //    ServiceNotesInFocus = true;
        //    TextBoxLostFocus("PMText");
        //}
            
            
    }

    [RelayCommand]
    private void CopyToClipboard(string textToCopy) {
        if (textToCopy == "PMText") {
            Clipboard.SetText(PMText);
            //PmTextBoxBackground = Brushes.Black;
            //PmTextBoxForeground = Brushes.White;
            //PmTextBoxBorderBrush = Brushes.White;
            //PMInFocus = true;

            //ServiceNotesBackground = Brushes.White;
            //ServiceNotesForeground = Brushes.Black;
            //ServiceNotesBorderBrush = Brushes.Black;
        }

        else if (textToCopy == "ServiceNotes") {
            Clipboard.SetText(ServiceNotesText);
            //PmTextBoxBackground = Brushes.White;
            //PmTextBoxForeground = Brushes.Black;
            //PmTextBoxBorderBrush = Brushes.Black;
            //ServiceNotesInFocus = true;

            //ServiceNotesBackground = Brushes.Black;
            //ServiceNotesForeground = Brushes.White;
            //ServiceNotesBorderBrush = Brushes.White;
        }

        else if (textToCopy == "ServiceNotesUAText") {
            if (SelectedUAReason != null) {
                Clipboard.SetText(SelectedUAReason);

            }
            else {
                MessageBox.Show("No text to copy");
            }
        }

        else {
            MessageBox.Show("No text to copy");
        }

    }

    [RelayCommand]
    private void CustomerAccountInfo() {
        MessageBox.Show("got to CustomerAccountInfoCommand message");
    }

    [RelayCommand]
    private void ServicePlan() {
        MessageBox.Show("got to ServicePlanCommand message");
    }

    [RelayCommand]
    private void ServicePlanImportInfo(object obj) {
        
    }

    #endregion

}
