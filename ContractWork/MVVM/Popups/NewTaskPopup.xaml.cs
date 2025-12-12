using ContractWork.MVVM.TaskManager;

namespace ContractWork.MVVM.Popups;

public partial class NewTaskPopup : Window
{
    public Dictionary<int, string> _selCustomer { get; set; }
    public NewTaskContent taskContent;

    public enum ViewList {
        Routine = 1,
        Priority = 2,
        Urgent = 3
    };

    public NewTaskPopup(NewTaskContent _task)
    {
        
        InitializeComponent();
        taskContent = _task;
        dpDateCreated.SelectedDate = DateTime.Now.Date;
        cboFillInImportance();

    }

    private void cboFillInImportance() {
        cboImportance.ItemsSource = Enum.GetValues(typeof(ViewList));
        if (taskContent.taskID !=0 && taskContent != null) {
            txtbxCustomerName.txtbxText = taskContent.customerName;
            txtbxDescription.txtbxText = taskContent.description;
            dpDateCreated.SelectedDate = taskContent.dateStarted;
            dpDateDue.SelectedDate = taskContent.dateDue;
            ViewList pickedImportance = (ViewList)taskContent.importance;
            cboImportance.SelectedValue = pickedImportance;
        }

    }

    private void GetCustomerName_Click(object sender, MouseButtonEventArgs e) {
        if (e.ChangedButton == MouseButton.Left) {
            CustomerPickPopup selectCustomer = new CustomerPickPopup();
            selectCustomer.Owner = this;

            if (selectCustomer.ShowDialog() == true) {
                _selCustomer = selectCustomer.selectedAccount;
                foreach (var pair in _selCustomer) {
                    taskContent.customerID = pair.Key;
                    taskContent.customerName = pair.Value.ToString();
                }
                txtbxCustomerName.txtbxText = taskContent.customerName;
            }
            else MessageBox.Show("No customer selected");
            
        }

    }

    private void NextButton_click(object sender, RoutedEventArgs e) {
        taskContent.customerName = txtbxCustomerName.txtbxText;
        taskContent.description = txtbxDescription.txtbxText;
        taskContent.dateStarted = dpDateCreated.SelectedDate;
        taskContent.dateDue = dpDateDue.SelectedDate;
        taskContent.importance = (int)cboImportance.SelectedValue;
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