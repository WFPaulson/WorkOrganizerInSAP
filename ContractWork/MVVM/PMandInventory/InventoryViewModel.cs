namespace ContractWork.MVVM.PMandInventory;

public partial class InventoryViewModel : ObservableObject {

    //private const string All  = "*";

    private AccessService dbInventory;
    private Dictionary<int, string> dictInventoryCategory;
    //private InventoryItem newItem;

    #region Observable
    [ObservableProperty]
    private ObservableCollection<InventoryModel> _inventoryPartsList;

    [ObservableProperty]
    private InventoryModel _inventoryParts;

    [ObservableProperty]
    private DataTable _inventoryCategoryList;

    [ObservableProperty]
    private List<string> _listOfPartsList;

    [ObservableProperty]
    private InventoryModel _selectedInventoryItem;



    [ObservableProperty]
    private string _partstextOverlay = "View by...";

    [ObservableProperty]
    private string _partsSelectedView;
    partial void OnPartsSelectedViewChanged(string value) {
        ChangePartsListView();
    }

    [ObservableProperty]
    private string _equipmentSelectedView;

    [ObservableProperty]
    private bool _rbAllisChecked = true;
    [ObservableProperty]
    private bool _rbP15IsChecked;
    [ObservableProperty]
    private bool _radbtnLucasIsChecked;
    [ObservableProperty]
    private bool _radbtnLP35IsChecked;
    [ObservableProperty]
    private bool _radbtnAccessoriesIsChecked;
    [ObservableProperty]
    private bool _radbtnAEDsIsChecked;
    [ObservableProperty]
    private bool _radbtnSMKEMSIsChecked;
    [ObservableProperty]
    private bool _radbtnSMKHospitalIsChecked;

    [ObservableProperty]
    private InventoryModel _newItem;

    //[ObservableProperty]
    //private bool? _freqUsedItem;


    #endregion

    private string EquipmentSelected { get; set; } = "All";


    public NavigationService _navigationService { get; set; }
    public InventoryViewModel(NavigationService navigationService) {
        _navigationService = navigationService;

        Startup();
    }

    private void Startup()
    {
        dbInventory = new AccessService();
        InventoryPartsList = new();
        InventoryParts = new();
        InventoryCategoryList = new DataTable();
        dictInventoryCategory = new Dictionary<int, string>();

        ListOfPartsList = new List<string> { "Catalog List", "Parts List", "Frequent Parts" };
        string FileLocation = AccessService.AccessFileLocation;

        AccessService.AccessFileLocation = DashboardViewModel.fileLocatinDict["InventoryFilePath"];

        string getCategory =
            "SELECT [PartDetailsID], [PartData] " +
            "FROM [tblPartDetail_LU] " +
            "WHERE [PartDetailTypeID_FK] = 2";
        InventoryCategoryList = dbInventory.FetchDBRecordRequest(getCategory);
       
        dictInventoryCategory = InventoryCategoryList.AsEnumerable()
            .ToDictionary(row => row.Field<int>(0),
                                    row => row.Field<string>(1));

        string SQLGetInventory =
            "SELECT * " +
            "FROM [tblCatalogNumberDetails] " +
            "WHERE [FrequentUsedItem] = -1 " +
            "ORDER BY [CatalogNumber]";

        PartstextOverlay = "Frequent Parts";
        EquipmentSelectedView = "rbAll";
        DataTable dteInventory = dbInventory.FetchDBRecordRequest(SQLGetInventory);
        InventoryPartsList = InventoryParts.GetPartsList(dteInventory);

        

        AccessService.AccessFileLocation = FileLocation;
    }

    [RelayCommand]
    private void ChangeEquipment(string sortEquip) {
        string FileLocation = AccessService.AccessFileLocation;
        AccessService.AccessFileLocation = DashboardViewModel.fileLocatinDict["InventoryFilePath"];
        string filters = string.Empty;

        EquipmentSelectedView = sortEquip;

        switch (sortEquip) {
            case "rbAll":
                filters = "All";
                break;

            case "rbLP15":
                filters = "[EquipmentType] = 4 ";
                break;

            case "rbLucas":
                filters = "[EquipmentType] = 6 ";
                break;
            
            case "rbLP35":
                filters = "[EquipmentType] = 22 ";
                break;

            case "rbAccessories":
                filters = "[EquipmentType] = 1 ";
                break;

            case "rbAEDs":
                filters = "[EquipmentType] = 3 AND [EquipmentType] = 20 AND [EquipmentType] = 21 ";
                break;

            case "rbSMKEMS":
                filters = "[EquipmentType] = 12 ";
                break;

            case "rbSMKHospital":
                filters = "[EquipmentType] = 14 ";
                break;

            default: break;


        }

        EquipmentSelected = filters;

        if (PartstextOverlay == "Frequent Parts" && EquipmentSelected != "All")
            filters = "WHERE [FrequentUsedItem] = -1 " + "AND " + filters;
        else if (PartstextOverlay == "Frequent Parts")
            filters = "WHERE [FrequentUsedItem] = -1 ";
        else if (EquipmentSelected != "All")
            filters = "WHERE " + filters;
        else filters = "";

        //ChangePartsListView(PartstextOverlay, filters);


        string sqlFilterInv =
            "SELECT * " +
            "FROM [tblCatalogNumberDetails] ";
            

        sqlFilterInv += filters + "ORDER BY [CatalogNumber]";


        DataTable dteInventory = dbInventory.FetchDBRecordRequest(sqlFilterInv);
        InventoryPartsList = InventoryParts.GetPartsList(dteInventory);

        AccessService.AccessFileLocation = FileLocation;

    }

    [RelayCommand]
    private void ChangePartsListView() {
        string FileLocation = AccessService.AccessFileLocation;
        AccessService.AccessFileLocation = DashboardViewModel.fileLocatinDict["InventoryFilePath"];
        //ListOfPartsList = new List<string> { "Catalog List", "Parts List", "Frequent Parts" };
        string sqlWhichParts = string.Empty;

        switch (PartsSelectedView) {
            case "Catalog List":
                //sqlWhichParts = " ";
                break;

            case "Parts List":
                //sqlWhichParts = " ";
                break;

            case "Frequent Parts":
                sqlWhichParts = "WHERE [FrequentUsedItem] = -1 ";
                break;

            default:
                break;

        }
        string sqlParts =
            "SELECT * " +
            "FROM [tblCatalogNumberDetails] ";
        
        if (PartsSelectedView == "Frequent Parts" && EquipmentSelected != "All")
            sqlWhichParts = sqlWhichParts + "AND " + EquipmentSelected;
        else if (PartsSelectedView != "Frequent Parts" && EquipmentSelected != "All")
            sqlWhichParts = "WHERE " + EquipmentSelected;

        sqlParts += sqlWhichParts + "ORDER BY [CatalogNumber]";

        DataTable dteInventory = dbInventory.FetchDBRecordRequest(sqlParts);
        InventoryPartsList = InventoryParts.GetPartsList(dteInventory);

        AccessService.AccessFileLocation = FileLocation;
    }

    [RelayCommand]
    private void NewInventory() {
        NewItem = new();
        NewInventoryItemPopup newInventoryItem = new(NewItem);           //NewItem);
        if (newInventoryItem.ShowDialog() == true) {
            NewItem = newInventoryItem.newItem;

            NewItem_db(NewItem);
        }
        ChangeEquipment(EquipmentSelectedView);
    }

    [RelayCommand]
    private void EditInventoryItem() {

        //NewInventoryItemPopup newInventoryItem = new(NewItem);

        NewInventoryItemPopup newInventoryItem = new(SelectedInventoryItem);
        if (newInventoryItem.ShowDialog() == true) {
            NewItem = newInventoryItem.newItem;

            UpdateItem_db(NewItem);
        }
        ChangeEquipment(EquipmentSelectedView);

    }

    private void UpdateItem_db(InventoryModel newItem) {
        AccessService db = new();

        string currentlocation = AccessService.AccessFileLocation;
        AccessService.AccessFileLocation = DashboardViewModel.fileLocatinDict["InventoryFilePath"];

        string sqlUpdate =
            "UPDATE [tblCatalogNumberDetails] " +
            $"SET [EquipmentType] = {newItem.EquipmentTypeID}, " +
            $"[CatalogNumber] = '{newItem.CatalogNumber}', " +
            $"[Description] = '{newItem.Description}', " +
            $"[Category] = {newItem.CategoryID}, " +
            $"[FrequentUsedItem] = {newItem.FreqUsedItem}, " +
            $"[DateUpdated] = Now() " +
            $"WHERE [CatalogNumberID] = {newItem.CatalogNumberID}";

        db.AddToAccount(SQLInsert: sqlUpdate);

        AccessService.AccessFileLocation = currentlocation;

    }

    [RelayCommand]
    private void SetFrequentUsed(bool item) {
        AccessService db = new();
        string FileLocation = AccessService.AccessFileLocation;
        AccessService.AccessFileLocation = DashboardViewModel.fileLocatinDict["InventoryFilePath"];

        int ID = SelectedInventoryItem.CatalogNumberID;
        bool freqUsed = SelectedInventoryItem.FreqUsedItem;

        string sqlUpdateFreqItem =
            "UPDATE [tblCatalogNumberDetails] " +
            $"SET [FrequentUsedItem] = {freqUsed} " +
            $"WHERE [CatalogNumberID] = {ID}";

        db.AddToAccount(sqlUpdateFreqItem);
        ChangeEquipment(EquipmentSelectedView);

        AccessService.AccessFileLocation = FileLocation;
    }

    private void NewItem_db(InventoryModel newItem) {
        AccessService db = new();
        string currentlocation = AccessService.AccessFileLocation;

        //fix frequent used item also following columns dont match values fix that
        DateTime time = DateTime.Now;              // Use current time
        string format = "MM-dd-yyyy";    // modify the format depending upon input required in the column in database 
        //string insert = @" insert into Table(DateTime Column) values ('" + time.ToString(format) + "')";

        string sqlNewTask =
            "INSERT INTO [tblCatalogNumberDetails] ([EquipmentType], [CatalogNumber], [Description], [Category], " +
            "[FrequentUsedItem], [DateUpdated]) " +
            $"VALUES ({NewItem.EquipmentTypeID}, '{NewItem.CatalogNumber}', '{NewItem.Description}',  " +
            $"{NewItem.CategoryID}, {NewItem.FreqUsedItem}, '{time.ToString(format)}')";    // {NewItem.partNumber})";  #{NewItem.category}#
        
        db.AddToAccount(SQLInsert: sqlNewTask, fileLocation: DashboardViewModel.fileLocatinDict["InventoryFilePath"]);

        if (!string.IsNullOrEmpty(NewItem.PartNumber)) { //Category     CatalogNumberID


            string sqlGetID =
                "SELECT [CatalogNumberID] " +
                "FROM [tblCatalogNumberDetails] " +
                $"WHERE [CatalogNumber] = '{NewItem.CatalogNumber}'";

            //error code from here look at sql first
            DataTable dte = db.FetchDBRecordRequest(sqlGetID);

            int ID = (int)dte.Rows[0]["CatalogNumberID"];

            string sqlUpdateParts =
                "INSERT INTO [tblPartNumber] ([CatalogNumberID_FK], [PartNumber], [PrimaryPartNumber]) " +
                $"VALUES ({ID}, '{NewItem.PartNumber}', {NewItem.FreqUsedItem}";

            db.AddToAccount(SQLInsert: sqlUpdateParts, fileLocation: DashboardViewModel.fileLocatinDict["InventoryFilePath"]);
        }
        AccessService.AccessFileLocation = currentlocation;
    }
}

//public partial class InventoryItem : ObservableObject {
//    public int catalogID { get; set; } = 0;
//    public int equipmentID { get; set; } = 0;
//    public string equipment { get; set; } = string.Empty;
//    public string description { get; set; } = string.Empty;
//    public string catalogNumber { get; set; } = string.Empty;
//    public int categoryID { get; set; } = 0;
//    public string category { get; set; } = string.Empty;
//    public string partNumber { get; set; } = string.Empty;
//    public string notes { get; set; } = string.Empty;
//    public bool frequentUsedItem { get; set; } = false;
//    public DateOnly DateUpdated { get; set; }




//    public InventoryItem() {
        
//    }

//    public InventoryItem(DataRow row) {
//        ScrubDataFromRow(row);
//    }

//    private void ScrubDataFromRow(DataRow row) {
        
//    }

   
//}




