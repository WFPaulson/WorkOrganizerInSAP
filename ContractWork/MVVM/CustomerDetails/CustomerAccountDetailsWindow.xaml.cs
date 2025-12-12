using System.Diagnostics.Eventing.Reader;

namespace ContractWork.MVVM.CustomerDetails;

public partial class CustomerAccountDetailsWindow : Window {

    public CustomerAccountDetailsWindow() {
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
    
    private void btnExit_Click(object sender, RoutedEventArgs e) {
        Window.GetWindow(this).Close();
    }

    private void dtgrdXLSpreadsheet_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
        if (e.PropertyType == typeof(DateTime)) {
            //if ((e.Column as DataGridTextColumn).Equals(DateTime.MinValue)) {
            //    (e.Column as DataGridTextColumn).Equals(null);
            //}
            //else {
            (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
            
        }
        if (e.PropertyType == typeof(decimal)) {
            (e.Column as DataGridTextColumn).Binding.StringFormat = "c2";
            e.Column.CellStyle = new Style(typeof(DataGridCell));
            e.Column.CellStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Right));
        }

    }


    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {

    }



    //private void dateNow_Click(object sender, RoutedEventArgs e) {
    //    MessageBox.Show("Hello");
    //}
}
