//using Microsoft.Identity.Client;

namespace ContractWork.MVVM.Popups;

public partial class CustomerContractSerialNumbers : Window {

    //public bool chkbxOnQuote { get; set; }
    //public bool IsSelected { get; set; }

    //public ObservableCollection<ContractAndAssetsModel>onContractAndAssets { get; set; }
    //public ContractAndAssetsModel contractAndAssets_current { get; set; }
    //public DataTable dteContractAndAssets { get; set; }



    public double MaximumWidth {
        get {
            // I don't want anything hard-coded.
            // Some other (correct) parameter would be fine as well,
            // if we can't get the window on screen.
            return SystemParameters.WorkArea.Width - 10;
        }
    }

    public double MaximumHeight {
        get {
            // I don't want anything hard-coded.
            // Some other (correct) parameter would be fine as well,
            // if we can't get the window on screen.
            return SystemParameters.WorkArea.Height - 100;
                //.Width - 10;
        }
    }

    public CustomerContractSerialNumbers(DataTable onContract, DataTable expireContract, DataTable noContract, string contractNumber) {
        InitializeComponent();
        
        TxtBoxTitle.Text = contractNumber;
        AssetsOnThisContractDataGrid.ItemsSource = onContract.AsDataView();
        ContractExpiredDataGrid.ItemsSource = expireContract.AsDataView();
        AssetsWithNoContractDataGrid.ItemsSource = noContract.AsDataView();
    }

    private void createQuote_Click(object sender, RoutedEventArgs e) {

        //MessageBox.Show("clicked");

        Window.GetWindow(this).DialogResult = true;
        btnExit_Click(sender, e);

    }

    private void btnExit_Click(object sender, RoutedEventArgs e) {
        if (!Window.GetWindow(this).DialogResult == true) {
            Window.GetWindow(this).DialogResult = false;
        }
        Window.GetWindow(this).Close();
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
}
