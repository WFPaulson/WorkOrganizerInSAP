namespace ContractWork.MVVM.CustomerDetails;

public partial class CustomerAccountSelectionWindow : Window {
    
    public CustomerAccountSelectionWindow() {
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

    private void btnClearSearchTextBox_btnClick(object sender, RoutedEventArgs e) {
        SearchTermTextBox.Text = "";
    }


}
