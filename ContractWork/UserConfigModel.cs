namespace ContractWork;

[Serializable]
public class UserConfigModel {
    public string AccessFilePath { get; set; }
    public string AccessFolderPath { get; set; }
    public string ContractsFilePath { get; set; }
    public string ContractsFolderPath { get; set; }
    public string AssetsFilePath { get; set; }
    public string AssetsFolderPath { get; set; }
    public string InventoryFilePath { get; set; }
    public string InventoryFolderPath { get; set; }


    public UserConfigModel()
    {
        
    }
    public UserConfigModel(string defaultJson)
    {
        AccessFilePath = @"D:\Cloud Services\NextCloud\Physio Work\VBA files and images\Access Customer DB files" +
                @"\Best version at ths time\Template Customer Contract and Equipment - 2024-0420.accdb";
        AccessFolderPath = @"D:\Cloud Services\NextCloud\Physio Work\VBA files and images\Access Customer DB files\Best version at ths time\";
        ContractsFilePath = @"D:\Cloud Services\NextCloud\Physio Work\VBA files and images\Contracts from Power Bi\raw spreadsheets\Contracts.xlsx";
        ContractsFolderPath = @"D:\Cloud Services\NextCloud\Physio Work\VBA files and images\Contracts from Power Bi\raw spreadsheets\";
        AssetsFilePath = @"D:\Cloud Services\NextCloud\Physio Work\VBA files and images\Contracts from Power Bi\raw spreadsheets\Assets.xlsx";
        AssetsFolderPath = @"D:\Cloud Services\NextCloud\Physio Work\VBA files and images\Contracts from Power Bi\raw spreadsheets\";
        InventoryFilePath = @"D:\Cloud Services\NextCloud\Physio Work\VBA files and images\Access Inventory DB files\Inventory 2022-01-25.accdb";
        InventoryFolderPath = @"D:\Cloud Services\NextCloud\Physio Work\VBA files and images\Access Inventory DB files";
    }
}

//public string ExtendedAssetsFilePath { get; set; }
//public string ExtendedAssetsFolderPath { get; set; }
//ExtendedAssetsFilePath = @"D:\Cloud Services\NextCloud\Physio Work\VBA files and images\Contracts From Power bi\assetsExtended.xlsx";
//ExtendedAssetsFolderPath = @"D:\Cloud Services\NextCloud\Physio Work\VBA files and images\Contracts From Power bi";
