//using static ContractWork.MVVM.Popups.NewTaskPopup;

using System;
using System.ComponentModel;
using ContractWork.MVVM.PMandInventory;

namespace ContractWork.MVVM.Popups;

public partial class NewInventoryItemPopup : Window
{
    public InventoryModel newItem;

   

    //public enum EquipmentList {
    //    Accessories = 1,
    //    ECG = 2,
    //    LP1000 = 3,
    //    LP15 = 4,
    //    LP20 = 5,
    //    Lucas = 6,
    //    NIBP = 7,
    //    SPO2 = 8,
    //    Ambulance = 12,
    //    Hospital = 14,
    //    LPCR2 = 20,
    //    LPCRPlus = 21,
    //    LP35 = 22
    //};

    //public enum CategoryList {
    //    Accessories_Disposables = 9,
    //    PM = 10,
    //    Repair = 11,
    //    WheelChair = 16,
    //    S3 = 17,
    //    GoBed = 18,
    //    Cots = 19
    //}

    public NewInventoryItemPopup(InventoryModel _newItem)
    {
        InitializeComponent();
        newItem = _newItem ?? new();
        
        Startup();
    }

    private void Startup() {
        cboEquipment.ItemsSource = InventoryModel.dictEquipmentType;     //Enum.GetValues(typeof(EquipmentList));
        cboCategory.ItemsSource = InventoryModel.dictCategoryType;       //Enum.GetValues(typeof(CategoryList));

        if (newItem != null) {
            chkFrequentUsed.IsChecked = newItem.FreqUsedItem;
            txtbxCatalogID.txtbxText = newItem.CatalogNumberID.ToString();

            cboEquipment.Text = newItem.EquipmentType;

            txtbxDescription.txtbxText = newItem.Description;
            txtbxCatalogNumber.txtbxText = newItem.CatalogNumber;

            cboCategory.Text = newItem.Category;
            
            txtbxPartNumber.txtbxText= newItem.PartNumber;
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
        if (!Window.GetWindow(this).DialogResult == true) {
            Window.GetWindow(this).DialogResult = false;
        }
        Window.GetWindow(this).Close();
    }

    private void Next_btnClick(object sender, RoutedEventArgs e) {
        string enumCategory = cboCategory.Text;
        string enumEquipment = cboEquipment.Text;

        newItem.EquipmentTypeID = InventoryModel.revDictEquipmentType[enumEquipment];
        newItem.CategoryID = InventoryModel.revDictCategoryType[enumCategory];           

        newItem.Description = txtbxDescription.txtbxText.ToString();
        newItem.CatalogNumber = txtbxCatalogNumber.txtbxText.ToString();

        newItem.PartNumber = txtbxPartNumber.txtbxText ?? "";

        newItem.FreqUsedItem = (bool)chkFrequentUsed.IsChecked;
        
        Window.GetWindow(this).DialogResult = true;
        btnExit_Click(sender, e);
    }
}
