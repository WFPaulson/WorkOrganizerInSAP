namespace ContractWork.MVVM.Quoting; 

public partial class CustomerDeviceListWindow : Window {
    public CustomerDeviceListWindow() {
        InitializeComponent();
    }

    private void DragMe(object sender, MouseButtonEventArgs e) {
        try {
            DragMove();
        }
        catch (Exception) {

        }
    }

    //private void dtgrdXLSpreadsheet_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
    //    if (e.PropertyType == typeof(DateTime)) {
    //        (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
    //    }
    //    if (e.PropertyType == typeof(decimal)) {
    //        (e.Column as DataGridTextColumn).Binding.StringFormat = "c2";
    //        e.Column.CellStyle = new Style(typeof(DataGridCell));
    //        e.Column.CellStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Right));
    //    }

    //}

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
