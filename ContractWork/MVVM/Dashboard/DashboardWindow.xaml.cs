namespace ContractWork.MVVM.Dashboard;

public partial class DashboardWindow : Window {
    
    public DashboardWindow() {
        InitializeComponent();
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

    private void dashboard_Loaded(object sender, RoutedEventArgs e) {
        var destopWorkingArea = System.Windows.SystemParameters.WorkArea;
        this.Left = destopWorkingArea.Right - this.Width;
        this.Top = destopWorkingArea.Bottom - this.Height - 130;
    }
}
