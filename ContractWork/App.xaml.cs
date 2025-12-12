using ContractWork.MVVM.FileSelection;
using Microsoft.Extensions.Configuration;
using Forms = System.Windows.Forms;

//using System.Windows.Forms;
using System.Drawing; // For Icon

// ... in your main window or a dedicated class


namespace ContractWork; 

public partial class App : Application {
 
    public static IConfiguration Config { get; private set; }

    public App()
    {
        Config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
    }

    protected override void OnStartup(StartupEventArgs e) {

        NavigationService navigationService = new NavigationService();

        DashboardWindow window = new() {
            DataContext = navigationService.CurrentViewModel = new DashboardViewModel(navigationService)         
        };
        window.Show();

        Forms.NotifyIcon notify = new System.Windows.Forms.NotifyIcon();

        base.OnStartup(e);

        #region Commented out statements

        /////////////////////////////////////////////////////
        //FileSelectionWindow window = new() {
        //    DataContext = navigationService.CurrentViewModel = new FileSelectionViewModel(navigationService)          //new MainWindowViewModel(navigationService)
        //};
        //window.Show();

        //new MainWindowViewModel(navigationService)

        //navigationService.CurrentViewModel = new ContractsAndWarrantyViewModel(navigationService);
        //ContractsAndWarrantyWindow ContractsAndWarrantyWindow;

        //ContractsAndWarrantyWindow = new ContractsAndWarrantyWindow() {
        //    DataContext = new ContractsAndWarrantyViewModel(navigationService)
        //};
        //ContractsAndWarrantyWindow.ShowDialog();

        /////////////////////////////////////////////////////

        //navigationService.CurrentViewModel = new ContractsAndWarrantyViewModel(navigationService);
        //ContractAndWarrantyView ContractsAndWarranty;

        //ContractsAndWarranty = new ContractAndWarrantyView() {
        //    DataContext = new ContractsAndWarrantyViewModel(navigationService)
        //};
        //ContractsAndWarranty.ShowDialog();

        /////////////////////////////////////////////////////

        #endregion

    }
}
