
using System.Runtime.CompilerServices;
using System.Text;

namespace ContractWork.MVVM.Popups;

public partial class CustomerPickPopup : Window {

    public Dictionary<int, string> dte { get; set; }
    public Dictionary<int, string> selectedAccount { get; set; }

    public CustomerPickPopup()
    {
        InitializeComponent();
        dte = CustomerAccountModel.GetAccountNameList();
        lstViewValue.ItemsSource = dte;
    }

    private void GetCustomerName_Click(object sender, MouseButtonEventArgs e) {
       
        NextButton_click(sender, e);
    }

    private void NextButton_click(object sender, RoutedEventArgs e) {
        var item = (KeyValuePair<int, string>)lstViewValue.SelectedItem;
        selectedAccount = new Dictionary<int, string> { { item.Key, item.Value } };
        Window.GetWindow(this).DialogResult = true;
        btnExit_Click(sender, e);
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
        if (!Window.GetWindow(this).DialogResult == true) {
            Window.GetWindow(this).DialogResult = false;
        }
        Window.GetWindow(this).Close();
    }
}
