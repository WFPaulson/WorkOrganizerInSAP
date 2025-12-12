namespace ContractWork.MVVM.AccountList;

public partial class AccountListWindow : Window
{
    

    public AccountListWindow() {
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

    private void Button_Click(object sender, RoutedEventArgs e) {
        MessageBox.Show("Hello");
    }
}
