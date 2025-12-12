namespace ContractWork.MVVM.Popups;


public partial class FileSelectionPopup : Window
{

    public Dictionary<string, string> popupFileLocationDict { get; set; }

    public FileSelectionPopup(Dictionary<string,string> fileLocationTmpDict)
    {
        InitializeComponent();
        popupFileLocationDict = new();
        ContractsFile.txtbxText = fileLocationTmpDict["ContractsFilePath"];
        AssetsFile.txtbxText = fileLocationTmpDict["AssetsFilePath"];
        //ContractsFile.txtbxText = fileLocationTmpDict["ContractsFilePath"];
        //ContractsFile.txtbxText = fileLocationTmpDict["ContractsFilePath"];
    }

    private void DragMe(object sender, MouseButtonEventArgs e) {
        try {
            DragMove();
        }
        catch (Exception) {

        }
    }

    private void btnMinimize_Click(object sender, RoutedEventArgs e) {
        Window window = Window.GetWindow(this);
        window.WindowState = WindowState.Minimized;
    }

    private void btnMaximize_Click(object sender, RoutedEventArgs e) {
        Window window = Window.GetWindow(this);
        if (window.WindowState == WindowState.Maximized) {
            window.WindowState = WindowState.Normal;
        }
        else { window.WindowState = WindowState.Maximized; }
    }

    private void btnExit_Click(object sender, RoutedEventArgs e) {
        Application.Current.Shutdown();
    }


}
