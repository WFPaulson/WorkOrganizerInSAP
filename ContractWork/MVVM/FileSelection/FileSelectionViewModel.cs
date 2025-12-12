using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace ContractWork.MVVM.FileSelection;
public partial class FileSelectionViewModel : ObservableObject {

    #region Const statements
    private const string excelExt = "Excel files | *.xlsx; *.xlsm";
    private const string accessExt = "Access files | *.accdb;*.mdb;*.laccdb;*.adp;*.mda;*.accda;*.mde;*.accde;*.ade";
    #endregion

    #region Observable properties
    [ObservableProperty]
    private string _contractFileName = "blank";

    [ObservableProperty]
    private string _assetFileName = "blank";

    [ObservableProperty]
    private string _extendedAssetFileName = "blank";

    [ObservableProperty]
    private string _accessFileName = "blank";

    [ObservableProperty]
    private string _inventoryFileName = "blank";

    [ObservableProperty]
    private string _timeoutinseconds = "0";

    [ObservableProperty]
    private Visibility _countdownvisible = Visibility.Hidden;

    [ObservableProperty]
    private Visibility _nextvisible = Visibility.Hidden;

    [ObservableProperty]
    private int _clockBtnWidth = 35;

    #endregion

    DispatcherTimer timer;
    private int increment = 0;

   
    private NavigationService _navigationService { get; set; }
    public FileSelectionViewModel(NavigationService navigationService, int duration = 0){
        _navigationService = navigationService;
        
        ShowFileLocations();
        
        if (duration > 0) {
            Countdownvisible = Visibility.Visible;
            increment = duration;
            Timeoutinseconds = duration.ToString();
            StartTimer();
        }
    }

    private void StartTimer() {
        timer = new();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e) {
        increment--;
        //increment.ToString();
        Timeoutinseconds = increment.ToString();
        if (increment <= 0) { 
            timer.Stop(); 
            timer.Tick -= Timer_Tick;
            Countdownvisible = Visibility.Hidden;
            App.Current.Windows[1].Close(); 
        }
        //else if (ReturnStatus == "Change File Location") { timer.Stop(); this.Close(); }
    }

    private void ShowFileLocations() {
        ContractFileName = DashboardViewModel.fileLocatinDict["ContractsFilePath"];
        AssetFileName = DashboardViewModel.fileLocatinDict["AssetsFilePath"];
        ExtendedAssetFileName = DashboardViewModel.fileLocatinDict["ExtendedAssetsFilePath"];
        AccessFileName = DashboardViewModel.fileLocatinDict["AccessFilePath"];
        InventoryFileName = DashboardViewModel.fileLocatinDict["InventoryFilePath"];
       
    }
    

    private void UpdateFileLocations(string updatedFile, [CallerMemberName] string callingMethod = default) {
        string pathFolderLocation = string.Empty;
        string pathFileLocation = string.Empty;

        if (callingMethod.ToLower().Contains("contract")) {
            pathFolderLocation = "ContractsFolderPath";
            pathFileLocation = "ContractsFilePath";
        }
        else if (callingMethod.ToLower().Contains("extended")) {
            pathFolderLocation = "ExtendedAssetsFolderPath";
            pathFileLocation = "ExtendedAssetsFilePath";
        }
        else if (callingMethod.ToLower().Contains("asset")) {
            pathFolderLocation = "AssetsFolderPath";
            pathFileLocation = "AssetsFilePath";
        }
        else if (callingMethod.ToLower().Contains("access")) {
            pathFolderLocation = "AccessFolderPath";
            pathFileLocation = "AccessFilePath";
        }
        else if (callingMethod.ToLower().Contains("inventory")) {
            pathFolderLocation = "InventoryFolderPath";
            pathFileLocation = "InventoryFilePath";
        }

        string currDir = Path.GetDirectoryName(updatedFile);
        string fileLocation = updatedFile;

        DashboardViewModel.fileLocatinDict.updateDictValue(pathFolderLocation, currDir);
        DashboardViewModel.fileLocatinDict.updateDictValue(pathFileLocation, fileLocation);

        GL.UpdateJsonUserConfig(pathFolderLocation, currDir);
        GL.UpdateJsonUserConfig(pathFileLocation, fileLocation);

    }

    [RelayCommand]
    private void PauseClock() {
        if (Timeoutinseconds != "Paused") {
            timer.Stop();
            ClockBtnWidth = 65;
            Timeoutinseconds = "Paused";
            Nextvisible = Visibility.Visible;
        }
        else {
            Nextvisible = Visibility.Hidden;
            timer.Start();
            ClockBtnWidth = 35;
            Timeoutinseconds = increment.ToString();
        }
    }

    [RelayCommand]
    private void Next() {
        timer.Stop();
        App.Current.Windows[1].Close();
    }




    #region Access Methods

    [RelayCommand]
    private void PickContractsFile() {
        (ContractFileName, _) = GL.getFile(passedPath: DashboardViewModel.fileLocatinDict["ContractsFolderPath"], passedExt: excelExt);
        UpdateFileLocations(ContractFileName);
    }

    [RelayCommand]
    private void PickAssetsFile() {
        (AssetFileName, _) = GL.getFile(passedPath: DashboardViewModel.fileLocatinDict["AssetsFolderPath"], passedExt: excelExt);
        UpdateFileLocations(AssetFileName);
    }
    
    [RelayCommand]
    private void PickExtendedAssetsFile() {
        (ExtendedAssetFileName, _) = GL.getFile(passedPath: DashboardViewModel.fileLocatinDict["ExtendedAssetsFolderPath"], passedExt: excelExt);
        UpdateFileLocations(ExtendedAssetFileName);
    }

    [RelayCommand]
    private void PickAccessFile() {
        (AccessFileName, _) = GL.getFile(passedPath: DashboardViewModel.fileLocatinDict["AccessFolderPath"], passedExt: accessExt);
        UpdateFileLocations(AccessFileName);
    }
    
    [RelayCommand]
    private void PickInventoryFile() {
        (InventoryFileName, _) = GL.getFile(passedPath: DashboardViewModel.fileLocatinDict["InventoryFolderPath"], passedExt: accessExt);
        UpdateFileLocations(InventoryFileName);
    }

    #endregion
}
