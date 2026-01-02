namespace ContractWork.MVVM.PMandInventory;

public partial class PMSchedulingWindow : Window {

    public GC ColumnWidths { get; set; }
    public PMSchedulingWindow() {
        InitializeComponent();
    }

    private void Window_ContentRendered(object sender, System.EventArgs e) {
        double width = 0;
        int count = 0;

        //ColumnWidths = ((PMSchedulingViewModel)(this.DataContext)).PMMonthColumnWidths;

        //foreach (string key in ColumnWidths.ColumnWidths.Keys) {
        //    if (ColumnWidths.ColumnWidths[key] != 0 && ColumnWidths.ColumnWidths[key] != 99) {
        //        width = 6.8 * ColumnWidths.ColumnWidths[key];
        //        PMMonthSchedule.Columns[count].Width = width;

        //    }
        //    count++;
        //}
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
        //Application.Current.Shutdown();
        Window.GetWindow(this).Close();
    }
}
