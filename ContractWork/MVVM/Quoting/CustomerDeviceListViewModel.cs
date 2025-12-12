
namespace ContractWork.MVVM.Quoting; 
public partial class CustomerDeviceListViewModel : ObservableObject {

    #region Observable statements
    [ObservableProperty]
    private string _selectedContractName;

    [ObservableProperty]
    private ObservableCollection<XLData> _onContractList;

    [ObservableProperty]
    private ObservableCollection<XLData> _onExpiredList;

    [ObservableProperty]
    private ObservableCollection<XLData> _onNoList;

    #endregion

    #region Observable Checkbox
    [ObservableProperty]
    private bool _onContractQuote = false;
    partial void OnOnContractQuoteChanged(bool value) {
        
    }

    [ObservableProperty]
    private bool _onExpiredQuote = true;
    partial void OnOnExpiredQuoteChanged(bool value) {
        
    }

    [ObservableProperty]
    private bool _onNoListQuote = true;
    partial void OnOnNoListQuoteChanged(bool value) {
        
    }

    #endregion


    public NavigationService _navigationService { get; set; }
    public CustomerDeviceListViewModel(NavigationService navigationService, DataTable onContract, DataTable expireContract, 
                                                                                DataTable noContract, string contractName) {
        _navigationService = navigationService;
        _selectedContractName = contractName;

        OnContractList = CreateModelFromList(onContract);
        OnExpiredList = CreateModelFromList(expireContract);
        OnNoList = CreateModelFromList(noContract);

    }

    private ObservableCollection<XLData> CreateModelFromList(DataTable dtContractList) {  //}, ref ObservableCollection<XLData> contractAndAssetsModel) {
        ObservableCollection<XLData>  contractAndAssetsModel = new();

        foreach (DataRow row in dtContractList.Rows) {
            contractAndAssetsModel.Add(new XLData(row));

        }
        return contractAndAssetsModel;
    }
}