namespace ContractWork.MVVM.FileSelection;

public partial class FileSelectionWindow : Window {
    public FileSelectionWindow() {
        InitializeComponent();
    }

    private void dtgrdXLSpreadsheet_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
        if (e.PropertyType == typeof(DateTime)) {
            (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
        }
        if (e.PropertyType == typeof(decimal)) {
            (e.Column as DataGridTextColumn).Binding.StringFormat = "c2";
            e.Column.CellStyle = new Style(typeof(DataGridCell));
            e.Column.CellStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Right));
        }

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
        Window.GetWindow(this).Close();
        
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
        for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            if (vis is DataGridRow) {
                var row = (DataGridRow)vis;
                row.DetailsVisibility =
                row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                break;
            }
    }


}



