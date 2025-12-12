namespace ContractWork.MVVM.PMandInventory;

public partial class InventoryModel : ObservableObject {

    public static Dictionary<int, string> dictEquipmentType = new Dictionary<int, string> {
            { 1, "Accessory" },
            { 2, "ECG" },
            { 3, "LP1000" },
            { 4, "LP15" },
            { 5, "LP20" },
            { 6, "Lucas" },
            { 7, "NIBP" },
            { 8, "SPO2" },
            { 12, "Amblance" },
            { 14, "Hospital" },
            { 15, "****" },
            { 20, "LPCR2" },
            { 21, "LPCRPlus" },
            { 22,"LP35" },
            { 0, "" }
    };

    public static Dictionary<string, int> revDictEquipmentType = new Dictionary<string, int> {
            { "Accessory", 1 },
            { "ECG", 2 },
            { "LP1000", 3 },
            { "LP15", 4 },
            { "LP20", 5 },
            { "Lucas", 6 },
            { "NIBP", 7 },
            { "SPO2", 8 },
            { "Amblance", 12 },
            { "Hospital", 14 },
            { "****", 15 },
            { "LPCR2", 20 },
            { "LPCRPlus", 21 },
            { "LP35", 22 },
            { "", 0 }
    };

    public static Dictionary<int, string> dictCategoryType = new Dictionary<int, string> {
            { 9, "Accessories/Disposables" },
            { 10, "PM" },
            { 11, "Repair" },
            { 13, "*" },
            { 16, "WheelChair" },
            { 17, "S3" },
            { 18, "GoBed" },
            { 19, "Cots" }
    };

    public static Dictionary<string, int> revDictCategoryType = new Dictionary<string, int> {
            { "Accessories/Disposables", 9 },
            { "PM", 10 },
            { "Repair", 11 },
            { "*", 13 },
            { "WheelChair", 16 },
            { "S3", 17 },
            { "GoBed", 18 },
            { "Cots", 19 }
    };

    [ObservableProperty]
    private string _equipmentType;

    [ObservableProperty]
    private int _equipmentTypeID = 0;

    [ObservableProperty]
    private string _category;

    [ObservableProperty]
    private int _categoryID = 0;

    [ObservableProperty]
    private int _catalogNumberID;

    [ObservableProperty]
    private string _catalogNumber;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private bool _freqUsedItem;

    [ObservableProperty]
    private string _notes;

    [ObservableProperty]
    private int _lotCodeID;

    [ObservableProperty]
    private string _lotCode;

    [ObservableProperty]
    private DateOnly _lotCodeExpiration;

    [ObservableProperty]
    private int _partNumberID;

    [ObservableProperty]
    private string _partNumber;

    [ObservableProperty]
    private DateTime? _lastDateUpdated;

   

    [RelayCommand]
    private void CopyCatalogNumber() {
        Clipboard.SetText(CatalogNumber);
    }

    [RelayCommand]
    private void SetFrequentUsed() {
        MessageBox.Show($"clicked is {FreqUsedItem}");
    }



    public InventoryModel() {
            
    }


    public InventoryModel(DataRow dr) {
        //int EquipType = 0;
        //int CategType = 0;

        CatalogNumberID = (int)dr["CatalogNumberID"];
        CatalogNumber = dr["CatalogNumber"].ToString();
        Description = dr["Description"].ToString();
        _equipmentTypeID = (int)dr["EquipmentType"];
        EquipmentType = dictEquipmentType[_equipmentTypeID] ;

        if (dr.Table.Columns.Contains("Category")) {
            if (dr["Category"] != DBNull.Value && (int)dr["Category"] != 0) {
                _categoryID = (int)dr["Category"];
                Category = dictCategoryType[_categoryID];
            }
        }
        

        FreqUsedItem = (bool)dr["FrequentUsedItem"];

        if (dr.Table.Columns.Contains("DateUpdated")) {
            if (dr["DateUpdated"] != DBNull.Value) {
                LastDateUpdated = ((DateTime)dr["DateUpdated"]);
            }
        }

        if (dr.Table.Columns.Contains("Notes")) {
            if (dr["Notes"] != DBNull.Value) {
                Notes = (dr["Notes"].ToString());
            }
        }

        //LastDateUpdated = (DateTime)dr["DateUpdated"];

    }

    public ObservableCollection<InventoryModel> GetPartsList(DataTable dteInventory) {
        ObservableCollection<InventoryModel> inventoryList = new();

        foreach (DataRow dr in dteInventory.Rows) {

            inventoryList.Add(new InventoryModel(dr));
        }
            


        return inventoryList;
    }
}
