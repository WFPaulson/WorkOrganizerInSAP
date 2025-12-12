namespace ContractWork.MVVM.ContractsAndWarranty;



    public partial class ContractsAndWarrantyWindow : Window {

        public GC ColumnWidths;
        
        public ContractsAndWarrantyWindow() {
            InitializeComponent();
        }

        private void ContractsAndWarranty_ContentRendered(object sender, System.EventArgs e) {
            //ColumnWidths = ((ContractsAndWarrantyViewModel)(this.DataContext)).dgExcelColumnWidths;
            
            //double width = 0;
            //int count = 0;



            //foreach (string key in ColumnWidths.ColumnWidths.Keys) {
            //    if (ColumnWidths.ColumnWidths[key] != 0 && ColumnWidths.ColumnWidths[key] != 99) {
            //        width = 6.8 * ColumnWidths.ColumnWidths[key];
            //        dtgrdXLSpreadsheet.Columns[count].Width = width;

            //    }
            //    count++;
            //}
        }

        private void RefreshColumnSize_Click(object sender, RoutedEventArgs e) {
            RefreshColumnSize();
        }

        private void RefreshColumnSize() {
            double width = 0;
            int count = 0;
            int values = 0;
            string colName = string.Empty;

            //List<string> colName = new List<string>();

            for (int i = 0; i < dtgrdXLSpreadsheet.Columns.Count; i++) {
                colName = dtgrdXLSpreadsheet.Columns[i].Header.ToString();
                values = ColumnWidths.ColumnWidths[colName];
                if (values != 0 && values != 99) {
                    width = 6.8 * values;
                    dtgrdXLSpreadsheet.Columns[count].Width = width;
                }
                count++;
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

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            Window.GetWindow(this).Close();
            //this.Close();
            //Application.Current.Shutdown();
        }

        private void dtgrdAccessDataBase_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as System.Windows.Controls.DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
        }

    private void dtgrdXLSpreadsheet_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e) {
        //if (e.PropertyType == typeof(System.DateTime))
        //    (e.Column as System.Windows.Controls.DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";

        if (e.PropertyType == typeof(DateTime)) {
            (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
        }
        if (e.PropertyType == typeof(decimal)) {
            (e.Column as DataGridTextColumn).Binding.StringFormat = "c2";
            e.Column.CellStyle = new Style(typeof(DataGridCell));
            e.Column.CellStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Right));
        }
    }

    



        private void SortFilterListBox_MouseMove(object sender, MouseEventArgs e) {
            Point position = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed) {
                try {

                    DragDrop.DoDragDrop(SortFilterListBox, SortFilterListBox.SelectedItem, DragDropEffects.Move);
                }
                catch (System.Exception) {
                    throw;
                }
            }
        }

        private void txtbx1_Drop(object sender, DragEventArgs e) {


        }

        private void SortFilterListBox_Drop(object sender, DragEventArgs e) {

        }

        private void SortFilterListBox_DragLeave(object sender, DragEventArgs e) {

        }

        private void SortFilterListBox_DragOver(object sender, DragEventArgs e) {

        }

        private void SortFilterListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) {

            if (string.IsNullOrEmpty(txtbx1.Text)) { txtbx1.Text = SortFilterListBox.SelectedItem.ToString(); return; }
            if (string.IsNullOrEmpty(txtbx2.Text)) { txtbx2.Text = SortFilterListBox.SelectedItem.ToString(); return; }
            if (string.IsNullOrEmpty(txtbx3.Text)) { txtbx3.Text = SortFilterListBox.SelectedItem.ToString(); return; }
            if (string.IsNullOrEmpty(txtbx4.Text)) { txtbx4.Text = SortFilterListBox.SelectedItem.ToString(); return; }
            if (string.IsNullOrEmpty(txtbx5.Text)) { txtbx5.Text = SortFilterListBox.SelectedItem.ToString(); return; }
            if (string.IsNullOrEmpty(txtbx6.Text)) { txtbx6.Text = SortFilterListBox.SelectedItem.ToString(); return; }

        }

        private void txtbx1_PreviewDrop(object sender, DragEventArgs e) {
            e.Handled = true;
            txtbx1.Text = "";
            string tstring;
            tstring = e.Data.GetData(DataFormats.StringFormat).ToString();
            txtbx1.Text = tstring;
        }

        private void txtbx1_DragEnter(object sender, DragEventArgs e) {
            txtbx1.Clear();
            e.Effects = DragDropEffects.Copy;
        }
        
    }

