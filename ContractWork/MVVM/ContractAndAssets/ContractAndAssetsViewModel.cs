using System.Collections.Generic;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Primitives;
using SQL = ContractWork.MVVM.ContractAndAssets.ContractAndAssetsSQLStatements;

namespace ContractWork.MVVM.ContractAndAssets;

public partial class ContractAndAssetsViewModel : ObservableObject {
    private enum MyEnum
    {
        update,
        add

    }

    #region Const statements
    private const string excelExt = "Excel files | *.xlsx; *.xlsm";
    private const string accessExt = "Access files | *.accdb;*.mdb;*.laccdb;*.adp;*.mda;*.accda;*.mde;*.accde;*.ade";

    #endregion

    #region Property statements
    Dictionary<int, int> dictServicePlans;
    Dictionary<int, int> dictAccountNumbers;

    #endregion

    #region Observable statements
    [ObservableProperty]
    private AccessService _accessDB;

    [ObservableProperty]
    private ExcelService _excelWB;

    //[ObservableProperty]
    //private ContractAndAssetsModel _contractAndAssetsModel;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CompareAccountNumbersCommand))]
    private string _contractFileName = "blank";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CompareAccountNumbersCommand))]
    private string _accessFileName = "blank";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CompareAccountNumbersCommand))]
    private string _assetFileName = "blank";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CompareServicePlanCommand))]
    private string _extendedAssetsFileName = "blank";


    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CompareAccountNumbersCommand))]
    private string _inventoryFileName = "blank";

    [ObservableProperty]
    private DataTable _accessDatatable;
    [ObservableProperty]
    private DataTable _reducedAccessDatatable;

    [ObservableProperty]
    private DataTable _inventoryAccessDatatable;

    [ObservableProperty]
    private DataTable _excelContractsDatatable;

    [ObservableProperty]
    private DataTable _excelCustomerContractsPopupDatatable;

    [ObservableProperty]
    private DataTable _excelAssetDatatable;

    [ObservableProperty]
    private DataTable _excelAssetsExtendedDatatable;

    [ObservableProperty]
    private DataTable _excelCustomerAssetsPopupDatatable;

    [ObservableProperty]
    private DataRowView _selectedCustomerAccount;

    [ObservableProperty]
    private string _exceptionMessage = string.Empty;

    [ObservableProperty]
    private bool _didItFail = false;
   

    //partial void OnDidItFailChanged(bool value) {
    //    if (value) {
    //        throw new FileLocationNotFoundException();
    //        DidItFail = false;
    //    }
    //}





    #endregion


    private NavigationService _navigationService { get; set; }
    public ContractAndAssetsViewModel(NavigationService navigationService) {
        
        _navigationService = navigationService;
        AccessDB = new();
        ExcelWB = new();
        dictServicePlans = new();
        dictAccountNumbers = new();
        
        ShowFileLocations();
    }

    [RelayCommand]
    private void ContractExpireStatus() {
        UpdateExpireStatus();
    }

    //TODO: need to update tblEquipment status also
    public static void UpdateExpireStatus() {
        AccessService db = new();

        #region MyRegion
        //update planstatus Expired to checkmark and Status to Expired 
        //sqlUpdateStatus;
        //"UPDATE tblServicePlan " +
        //"SET tblServicePlan.Expired = IIf([tblServicePlan]![ServicePlanExpireDate] < Date(), True, False), " +
        //"tblServicePlan.ServicePlanStatus = IIf([tblServicePlan]![ServicePlanExpireDate] < Date(), 'Expired', tblServicePlan.ServicePlanStatus)";

        //db.AddToAccount(sqlUpdateStatus, FL.fileLocatinDict["AccessFilePath"]);

        //update planstatus from Future to Active if meets todays date
        //sqlUpdateStatus =
        //"UPDATE tblServicePlan " +
        //"SET tblServicePlan.ServicePlanStatus = IIf([tblServicePlan]![ServicePlanStatus] = 'Future' OR IIf([tblServicePlan]![ServicePlanStatus] = '', " +
        //"IIf([tblServicePlan]![ServicePlanStartDate] <= Date() AND IIf([tblServicePlan]![ServicePlanExpireDate] >= Date(), 'Active', " +
        //"IIf([tblServicePlan]![ServicePlanStartDate] > Date(), 'Future', " +
        //"IIf([tblServicePlan]![ServicePlanExpireDate] < Date(), 'Expired', tblServicePlan.ServicePlanStatus)";

        //db.AddToAccount(sqlUpdateStatus, FL.fileLocatinDict["AccessFilePath"]); 
        #endregion

        string sqlUpdateStatus =
        "UPDATE tblServicePlan " +
        "SET tblServicePlan.Expired = IIf([tblServicePlan.ServicePlanExpireDate] < Date(), True, False), " +
        "tblServicePlan.ServicePlanStatus = IIf([tblServicePlan.ServicePlanExpireDate] < Date(), 'Expired', " +
        "IIf([tblServicePlan.ServicePlanStartDate] > Date(), 'Future', " +
        "IIf([tblServicePlan.ServicePlanStartDate] < Date() AND [tblServicePlan.ServicePlanExpireDate] > Date(), 'Active', '')))";

        db.AddToAccount(sqlUpdateStatus, FL.fileLocatinDict["AccessFilePath"]);


        #region MyRegion
        //Need to check for empty status
        // sqlUpdateStatus =
        // "UPDATE tblServicePlan " +
        // "SET tblServicePlan.ServicePlanStatus = " +
        // "IIf([tblServicePlan]![ServicePlanStatus] = '', " +
        // "IIf([tblServicePlan]![ServicePlanStartDate] <= Date(), " +
        // "IIf([tblServicePlan]![ServicePlanExpireDate] >= Date(), 'Active', " +

        //;

        //db.AddToAccount(sqlUpdateStatus, FL.fileLocatinDict["AccessFilePath"]);

        //update tblEquipment.planstatus from tblServicePlan.ServicePlanStatus 
        #endregion

        sqlUpdateStatus =
            "UPDATE tblServicePlan " +
            "INNER JOIN tblEquipment ON tblServicePlan.ServicePlanID = tblEquipment.ServicePlanID_FK " +
            "SET tblEquipment.ServicePlanStatusLU_cbo = [tblServicePlan]![ServicePlanStatus]";

        db.AddToAccount(sqlUpdateStatus, FL.fileLocatinDict["AccessFilePath"]);

        MessageBox.Show("Update is done");

    }

    
    private void PopulateGrid() {
        (ExcelContractsDatatable, _) = ExcelWB.OpenExcelFile(SQL.OpenExcelFileSQL(), ContractFileName);
    }

    private void ShowFileLocations() {
        DidItFail = true;
        do {
            ContractFileName = DashboardViewModel.fileLocatinDict["ContractsFilePath"];
            AssetFileName = DashboardViewModel.fileLocatinDict["AssetsFilePath"];
            ExtendedAssetsFileName = DashboardViewModel.fileLocatinDict["ExtendedAssetsFilePath"];
            AccessFileName = DashboardViewModel.fileLocatinDict["AccessFilePath"];
            InventoryFileName = DashboardViewModel.fileLocatinDict["InventoryFilePath"];
        
            try {
                
                (ExcelContractsDatatable, DidItFail) = ExcelWB.OpenExcelFile(SQL.OpenExcelFileSQL(), ContractFileName);
                (ExcelAssetDatatable, DidItFail) = ExcelWB.OpenExcelFile(SQL.OpenExcelFileSQL(), AssetFileName);
                //(ExcelAssetsExtendedDatatable, DidItFail) = ExcelWB.OpenExcelFile(SQL.OpenAssetsExtendedFileSQL(), ExtendedAssetsFileName);
                (AccessDatatable, _) = AccessDB.OpenAccessFile(SQL.OpenDBForAccountConversionSQL(), AccessFileName);
                (ReducedAccessDatatable, _) = AccessDB.OpenAccessFile(SQL.ReduceDBSQL(), AccessFileName);
                (InventoryAccessDatatable, _) = AccessDB.OpenAccessFile(SQL.OpenInventoryFileSQL(), InventoryFileName);
                DidItFail = false;
                
                
            }
            catch (OleDbException) {
                RequestFileLocationChange();
            }
            catch (Exception ex) {
                MessageBox.Show($"All Error message: {ex.Message}");
            }

            //catch (FileLocationNotFoundException ex) {
            //    //if (DidItFail) {
            //       // if (ExceptionMessage.Contains("OleDbException")) {
            //            RequestFileLocationChange();
            //        //}
            //    //}
            //}

            //catch (Exception) {
            //    if (DidItFail) {
            //        if (ExceptionMessage.Contains("OleDbException")) {
            //            RequestFileLocationChange();
            //        }
            //        DidItFail = false;
            //    }


            //}


        } while (DidItFail == true);

        

    }

    private void RequestFileLocationChange() {
        MessageBoxResult result;

        result = MessageBox.Show($"Do you want to change file location?", "?Pick file location?",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes) {
            FileSelectionWindow SelectFiles = new() {
                DataContext = _navigationService.CurrentViewModel = new FileSelectionViewModel(_navigationService)
            };
            SelectFiles.ShowDialog();
        }
        else {
            MessageBox.Show("Please fix issue and run again, thanks.");
            Environment.Exit(0);
        }
    }




    private void UpdateFileLocations(string updatedFile, [CallerMemberName] string callingMethod = default) {
        string pathFolderLocation = string.Empty;
        string pathFileLocation = string.Empty;

        if (callingMethod.ToLower().Contains("contract")) {
            pathFolderLocation = "ContractsFolderPath";
            pathFileLocation = "ContractsFilePath";
        }
        else if (callingMethod.ToLower().Contains("extended")) {
            pathFolderLocation = "ExtendedAssetsFolderPath";
            pathFileLocation = "ExtendedAssetsFilePath";
        }
        else if (callingMethod.ToLower().Contains("asset")) {
            pathFolderLocation = "AssetsFolderPath";
            pathFileLocation = "AssetsFilePath";
        }
        else if (callingMethod.ToLower().Contains("access")) {
            pathFolderLocation = "AccessFolderPath";
            pathFileLocation = "AccessFilePath";
        }

        else if (callingMethod.ToLower().Contains("inventory")) {
            pathFolderLocation = "InventoryFolderPath";
            pathFileLocation = "InventoryFilePath";
        }

        string currDir = Path.GetDirectoryName(updatedFile);
        string fileLocation = updatedFile;

        GL.FileAndFolderLocations.updateDictValue(pathFolderLocation, currDir);
        GL.FileAndFolderLocations.updateDictValue(pathFileLocation, fileLocation);

        GL.UpdateJsonUserConfig(pathFolderLocation, currDir);
        GL.UpdateJsonUserConfig(pathFileLocation, fileLocation);

    }


    #region Excel Methods
    [RelayCommand]
    private void PickContractsFile() {
        ExcelContractsDatatable = new();
        ExcelContractsDatatable.Clear();

        (ContractFileName, _) = GL.getFile(passedPath: DashboardViewModel.fileLocatinDict["ContractsFolderPath"], passedExt: excelExt);    // GL.FileAndFolderLocations["ContractsFolderPath"], passedExt: excelExt);

        string fileName = Path.GetFileName(ContractFileName);

        if (!Path.GetFileName(ContractFileName).ToLower().Contains("contracts")) {
            MessageBox.Show($"Filename is not for Contracts: {fileName} ");
            ContractFileName = "blank";
            return;
        }

        UpdateFileLocations(ContractFileName);

        (ExcelContractsDatatable, _) = ExcelWB.OpenExcelFile(SQL.OpenExcelFileSQL(), ContractFileName);
        //ClearBadContracts();
    }

    [RelayCommand]
    private void PickAssetsFile() {
        ExcelAssetDatatable = new();
        ExcelAssetDatatable.Clear();

        (AssetFileName, _) = GL.getFile(passedPath: DashboardViewModel.fileLocatinDict["AssetsFolderPath"], passedExt: excelExt);                //GL.FileAndFolderLocations["AssetsFolderPath"], passedExt: excelExt);

        string fileName = Path.GetFileName(AssetFileName);

        if (!Path.GetFileName(AssetFileName).ToLower().Contains("assets")) {
            MessageBox.Show($"Filename is not for Assets: {fileName} ");
            AssetFileName = "blank";
            return;
        }

        UpdateFileLocations(AssetFileName);

        (ExcelAssetsExtendedDatatable, _) = ExcelWB.OpenExcelFile(SQL.OpenExcelFileSQL(), AssetFileName);
    }

    [RelayCommand]
    private void PickExtendedAssetsFile() {
        ExcelAssetsExtendedDatatable = new();
        ExcelAssetsExtendedDatatable.Clear();

        (ExtendedAssetsFileName, _) = GL.getFile(passedPath: DashboardViewModel.fileLocatinDict["ExtendedAssetsFolderPath"], passedExt: excelExt);
        string fileName = Path.GetFileName(ExtendedAssetsFileName);

        if (!Path.GetFileName(ExtendedAssetsFileName).ToLower().Contains("extended") &&
                    !Path.GetFileName(ExtendedAssetsFileName).ToLower().Contains("assets")) {
            MessageBox.Show($"Filename is not for Extended Assets: {fileName} ");
            AssetFileName = "blank";
            return;
        }

        UpdateFileLocations(ExtendedAssetsFileName);
        (ExcelAssetsExtendedDatatable, _) = ExcelWB.OpenExcelFile(SQL.OpenExcelFileSQL(), ExtendedAssetsFileName);
    }

    [RelayCommand(CanExecute = nameof(AssetAndContractSelected))]
    private void OpenAccountContracts() {  //you get this by double click customer in datagrid // add a canexecute to this
        ExcelCustomerContractsPopupDatatable = new();
        ExcelCustomerContractsPopupDatatable.Clear();

        ExcelCustomerAssetsPopupDatatable = new();
        ExcelCustomerAssetsPopupDatatable.Clear();

        string accountNumber = SelectedCustomerAccount.Row["Account"].ToString();
        string accountName = SelectedCustomerAccount.Row["Customer"].ToString();
        //change this to a t parameter to use either accountname or accountnumber, if both strings maybe number be int??
        (ExcelCustomerContractsPopupDatatable, _) = ExcelWB.OpenExcelFile(SQL.GetCustomersContractList(accountNumber), ContractFileName);
        (ExcelCustomerAssetsPopupDatatable, _) = ExcelWB.OpenExcelFile(SQL.GetCustomersContractList(accountNumber), AssetFileName);

        //OpenCustomerContractList();

        string customerName = ExcelCustomerContractsPopupDatatable.Rows[0]["Customer"].ToString();

        AccountListWindow contractList = new() {
            DataContext = _navigationService.CurrentViewModel = new AccountListViewModel(_navigationService,
                                                                ExcelCustomerContractsPopupDatatable, customerName,
                                                                ExcelCustomerAssetsPopupDatatable)
        };
        //maybe ExcelCustomerAssetsPopupDatatable not ExcelAssetsExtendedDatatable ExcelCustomerAssetsPopupDatatable ExcelAssetsExtendedDatatable
        contractList.ShowDialog();
    }
    private bool AssetAndContractSelected => (ContractFileName != "blank") && (AssetFileName != "blank");


    [RelayCommand(CanExecute = nameof(ExtendedAssetsNotBlank))]
    private void CompareServicePlan() {
        AccessService.AccessFileLocation = FL.fileLocatinDict["AccessFilePath"];
        ExcelService.ExcelFileLocation = FL.fileLocatinDict["ContractsFilePath"];
        AccessService dbService = new();
        AddServicePlanToAccess newPlan;
        List<AddServicePlanToAccess> servicePlans= new();

        string contractNumber = string.Empty;
        string sqlInsert = string.Empty;
        string sqlAccount = string.Empty;
        double shipAccount, mainAccount = 0;
        int recordCount = 0;

        sqlAccount =
                "SELECT DISTINCT [Account], [Customer], [Contract], [Status], [Cvg Start], [Cvg End] " +
                "FROM [Edited$] " +
                "WHERE [Type] = 'ProCare'";       //<> 'HSS' AND [Type] <> 'Credit' ";

        (DataTable dt,_) = ExcelWB.RefreshSpreadSheet(sqlAccount);
        int x = 0;
        foreach (DataRow drRow in dt.Rows) {
            contractNumber = "00";
            if (drRow["Contract"].ToString().Substring(0, 2) != "00") {
                contractNumber += drRow["Contract"].ToString();
            }
            else contractNumber = drRow["Contract"].ToString();

            if (string.IsNullOrEmpty(contractNumber) || contractNumber == "00") {
                continue;
            }

            mainAccount = 0;
            mainAccount = (double)drRow["Account"];
            //if (mainAccount == 20261793) {
            //    MessageBox.Show("Should be Lewis Co");
            //}

            if (!contractNumber.DoesThisServicePlanExist() || !mainAccount.DoesThisAccountExist()) {
                
                sqlInsert =
                    "INSERT INTO [tblServicePlan]([ShipToAccountNumber], [AccountName_cbo], [ServicePlanNumber], [ServicePlanStatus], " +
                    "[ServicePlanStartDate], [ServicePlanExpireDate]) " +
                    $"VALUES ({drRow["Account"]}, '{drRow["Customer"]}', '{contractNumber}', '{drRow["Status"]}', " +
                    $"#{((DateTime)drRow["Cvg Start"]).ToString("MM/d/yyyy")}#, #{((DateTime)drRow["Cvg End"]).ToString("MM/d/yyyy")}#)";
                dbService.AddToAccount(sqlInsert);

                servicePlans.Add(new AddServicePlanToAccess { planNumber = contractNumber, acctName = drRow["Customer"].ToString() });

                recordCount++; 
            }
            else if (contractNumber.GetServicePlanStatus() == "") {
                string sqlUpdate =
                        "UPDATE [tblServicePlan] " +
                        $"SET [ServicePlanStatus] = '{drRow["Status"]}' " +
                        $"WHERE [ServicePlanNumber ] = '{contractNumber}'";
                dbService.AddToAccount(SQLInsert: sqlUpdate);

                servicePlans.Add(new AddServicePlanToAccess { planNumber = contractNumber, acctName = drRow["Customer"].ToString() });
                recordCount++;
            }
        }

       // newPlan.wrapUpComment

        WrapUpComment(recordCount, servicePlans, "Service Plans");
    }

    private void WrapUpComment(int count, List<AddServicePlanToAccess> sbList, string plan) {
        StringBuilder addedbuilder = new StringBuilder();

        addedbuilder.AppendLine($"{plan} added: ");

        foreach (AddServicePlanToAccess item in sbList) {

            if (plan == "Service Plans") { addedbuilder.AppendLine($"- {item.planNumber}   - {item.acctName} "); }
            else if (plan == "Account Numbers") { addedbuilder.AppendLine($"- {item.acctName}   - {item.planNumber} "); }

        }
        string listContent = addedbuilder.ToString();

        MessageBox.Show($"Done with Compare of {plan}: \n" +
            $"Records updated: {count} \n" +
            $"{listContent} ");
    }

    private bool ExtendedAssetsNotBlank => (ExtendedAssetsFileName != "blank") && (AccessFileName != "blank");

    #endregion

    #region Access Methods
    [RelayCommand]
    private void PickAccessFile() {
        AccessDatatable = new();
        AccessDatatable.Clear();

        (AccessFileName, _) = GL.getFile(passedPath: DashboardViewModel.fileLocatinDict["AccessFolderPath"], passedExt: accessExt);

        UpdateFileLocations(AccessFileName);

        (AccessDatatable, _) = AccessDB.OpenAccessFile(SQL.OpenDBForAccountConversionSQL(), AccessFileName);

        (ReducedAccessDatatable, _) = AccessDB.OpenAccessFile(SQL.ReduceDBSQL(), AccessFileName);
    }

    [RelayCommand]
    private void PickInventoryFile() {
        InventoryAccessDatatable = new();
        InventoryAccessDatatable.Clear();

        (InventoryFileName, _) = GL.getFile(passedPath: DashboardViewModel.fileLocatinDict["InventoryFolderPath"], passedExt: accessExt);

        UpdateFileLocations(InventoryFileName);

        (InventoryAccessDatatable, _) = AccessDB.OpenAccessFile(SQL.OpenDBForAccountConversionSQL(), InventoryFileName);

        // (ReducedAccessDatatable, _) = AccessDB.OpenAccessFile(DashboardSQLStatements.ReduceDBSQL(), AccessFileName);
    }


    [RelayCommand(CanExecute = nameof(BothFilesSelected))]
    private void CompareAccountNumbers() {
        AccessService.AccessFileLocation = FL.fileLocatinDict["AccessFilePath"];
        ExcelService.ExcelFileLocation = FL.fileLocatinDict["ContractsFilePath"];            //"ExtendedAssetsFilePath"];    ContractsFilePath
        AccessService dbService = new();
        List<AddServicePlanToAccess> NewAcctNames = new();

        string sqlInsert = string.Empty;
        string sqlAccount = string.Empty;
        string compositePrimaryKey, SecondaryKey = string.Empty;
        int recordCount = 0;
        double PrimaryKey = 0;
        int mAccount = 0, ID = 0;
        
        sqlAccount =
            "SELECT DISTINCT [Account], [Customer], [Contract], [Tech], [Sales Rep] " +              //, [Bill To], [Tech], [Sales Rep] " + //, //[Bill To], , [Contract] " +, [Tech], [Sales Rep]
            "FROM [Edited$] " +
            "WHERE [Type] = 'ProCare'"; //[Contract] <> '' AND 

        (DataTable dt, _) = ExcelWB.RefreshSpreadSheet(sqlAccount);
        
        foreach (DataRow drRow in dt.Rows) {

            if (drRow.IsEmpty()) { continue; }

            PrimaryKey = (double)drRow["Account"];

            if (!AccessExtensionMethods.DoesThisAccountExist(PrimaryKey, "Main")) {

                //DoesThisAccountExist

                sqlInsert =
                    "INSERT INTO [tblCustomerAccounts]([AccountName], [MainAccount_PK], " +
                    "[ProCareRepLU_cbo], [SalesRepLU_cbo]) " +
                    $"VALUES ('{drRow["Customer"]}', {drRow["Account"]}, " +
                    $"'{drRow["Tech"]}', '{drRow["Sales Rep"]}')";

                dbService.AddToAccount(sqlInsert);

                NewAcctNames.Add(new AddServicePlanToAccess { acctName = drRow["Customer"].ToString(), planNumber = drRow["Contract"].ToString()  });

                recordCount++;
            }
        }

        WrapUpComment(recordCount, NewAcctNames, "Account Numbers");
    }

    private bool BothFilesSelected => (ContractFileName != "blank") && (AccessFileName != "blank");


    [RelayCommand(CanExecute = nameof(ExtendedAssetsNotBlank))]
    private void CompareEquipment() {
        List <AddSerialToContract> returnList = new();
        int count = 0, expiredcount = 0, archiveCount = 0;
        string exitstring = string.Empty;
        bool exitcode = false;

        (count, returnList, archiveCount, exitcode) = FilterEquipment(SQL.EquipmentNotExpiredContract());
        use return list  for display string builder
        if (exitcode) {
            exitstring = ": program forced Exit ";
        }

        MessageBox.Show($"Updated {count} new contracts, archived {archiveCount} {exitstring}");
    }


    private (int, List<AddSerialToContract>, int, bool) FilterEquipment(string sqlStatus) {
        AccessService.AccessFileLocation = FL.fileLocatinDict["AccessFilePath"];
        ExcelService.ExcelFileLocation = FL.fileLocatinDict["AssetsFilePath"];
        AccessService dbService = new();
        List<Tuple<string, string>> list = new();
        DataTable dtAccount = new();
        Dictionary<string, List<AddSerialToContract>> newEquipment = new();
        AddSerialToContract buildWrapUpComment = new();
        StringBuilder sb = new();

        string contractNumber = string.Empty, dbContractStatus = string.Empty, xlContractStatus = string.Empty;
        string messageBoxText = string.Empty, caption = string.Empty;
        MessageBoxButton button = MessageBoxButton.YesNoCancel;
        MessageBoxImage icon = MessageBoxImage.Question;
        MessageBoxResult choice;
        bool exit = false;
        string sqlInsert = string.Empty, sqlAcctNumb = string.Empty, sqlXLStatus = string.Empty;
        string sqlEquipment = string.Empty;
        int planID = 0, status = 0;
        // bool result = false;
        int recordCount = 0, archiveRecord = 0;

        string x = string.Empty;
        //     
        //need to check if serial # is already active, dont need to update
        //

        (DataTable dtEquipment, _) = ExcelWB.RefreshSpreadSheet(sqlStatus);


        foreach (DataRow drRow in dtEquipment.Rows) {
            status = 0;

            if (recordCount == 4 ) { exit = true; }

            if (drRow["Serial"].ToString().DoesSerialNumberHaveServicePlanAndStatus(out x)) {
                if (x == "Active" || x.Contains("Future")) {
                    continue;
                }
                else {  //this  means that both x != "Active" && x != "Future" - need
                    MessageBox.Show($"contract is not Active, and is not Future, update contract status");
                    UpdateSerialNumberContractStatus(drRow);

                    if (!newEquipment.ContainsKey(drRow["Contract"].ToString())) {
                        newEquipment.Add(drRow["Contract"].ToString(), new List<AddSerialToContract> ()); //{ serial = drRow["Serial"].ToString() });
                    }
                    //else {
                    newEquipment[drRow["Contract"].ToString()].Add(new AddSerialToContract { serial = drRow["Serial"].ToString() });
                    buildWrapUpComment.wrapUpComment()
                    //}

                    recordCount++;
                    continue;
                }
            }

            else {
                if (!drRow["Serial"].ToString().DoesThisSerialNumberExist()) {
                    // MessageBox.Show($"Serial # {drRow["Serial"].ToString()} does not exist");
                    status += -1;
                }
                status += 1;
                //serial number exists, does the service plan exist?
                if (!drRow["Contract"].ToString().DoesThisServicePlanExist()) {
                    //MessageBox.Show($"Contract # {drRow["Contract"].ToString()} does not exist");
                    status += -2;
                }
                status += 2;
                //MessageBox.Show($"Status is {status}");

                //both = 3 , contract no sn = 2, sn no contract = 1, neither = 0

                switch (status) {
                    case 0:
                        messageBoxText = $"Neither the SN: {drRow["Serial"].ToString()} or contract # {drRow["Contract"].ToString()} exists. \n" +
                            "They are propably a CPO device from a previous account. \n" +
                            "Select NO if you want to Research SN and Contract. \n" +
                            "Select YES if you want to archive this contract and SN \n" +
                            "Select CANCEL to Exit out of program";

                        caption = "Archive Serial and Contract?";

                        choice = MessageBox.Show(messageBoxText, caption, button, icon);

                        if (choice != MessageBoxResult.No && choice != MessageBoxResult.Cancel) {

                            drRow.AddNewAccount();
                            drRow.AddNewContract();
                            drRow.AddNewSerialNumber();
                            drRow["Contract"].ToString().ArchiveServicePlan();

                        }

                        if (choice == MessageBoxResult.Cancel) {
                            exit = true;
                        }
                        break;
                    case 1:
                        MessageBox.Show("SN exists but contract # does not exist. \n" +
                            "Need to run 'Service Plans' to update contracts ");
                        break;
                    case 2:
                        MessageBox.Show("The SN does not exist but contract # exists \n" +
                            "Going to add SN and assign contract to it.");
                        AddNewSerialNumber(drRow);
                        UpdateSerialNumberContractStatus(drRow);

                        //TODO: add new equipment count to include serial # and product definition

                        if (!newEquipment.ContainsKey(drRow["Contract"].ToString())) { newEquipment.Add(drRow["Contract"].ToString(), new List<AddSerialToContract>()); }
                     
                        newEquipment[drRow["Contract"].ToString()].Add(new AddSerialToContract { serial = drRow["Serial"].ToString() });

                        
                        recordCount++;
                        break;

                    case 3:
                        MessageBox.Show("Both the SN and contract # exists. \n " +
                            "Something is wrong, if both exist, everything should be good ");
                        break;

                }
                if (exit) break;

            }
                //    if (MessageBox.Show($"Do you want to add {drRow["Serial"].ToString()} to contract {drRow["Contract"].ToString()}",
                //        "My Title", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
        }
        return (recordCount, newEquipment, archiveRecord, exit);
    }
   
    //private void AddNewAccount(DataRow dr) {
    //    string sqlContract = string.Empty;

    //    AccessDB.AddToAccount(sqlContract);
    //}

    //private void AddNewContract(DataRow dr) {
    //    string sqlContract =
    //        "INSERT INTO [tblServicePlan] ([ShipToAccountNumber], [ServicePlanNumber], [ServicePlanStatus], [Archive]) " +
    //        $"VALUES ('{dr["Account"]}', '{dr["Contract"]} ', ' {dr["Status"]}')";

    //    AccessDB.AddToAccount(sqlContract);
    //}

    private void AddNewSerialNumber(DataRow dr) {
        double custID = (double)dr["Account"];
        //var custID = v["Account"];
        int mainCustID = (int)custID.getMainAccountOrCustID();

        string sqlAddSN =
            "INSERT INTO [tblEquipment] ([CustomerAccountID_FK], [ServicePlanID_FK], [EquipmentSerial]) " +
            $"VALUES ({mainCustID}, {dr["Contract"].ToString().GetServicePlanID()}, '{dr["Serial"].ToString()}')";
        AccessDB.AddToAccount(sqlAddSN);
    }

    private void UpdateSerialNumberContractStatus(DataRow v) {
        string sqlUpdateStatus =
            "UPDATE [tblEquipment] " +
            $"SET [ServicePlanID_FK] = {v["Contract"].ToString().GetServicePlanID()}, " +
            $"[ServicePlanStatusLU_cbo] = '{v["Status"].ToString()}' " +
            $"WHERE [EquipmentSerial] = '{v["Serial"].ToString()}' ";

        AccessDB.AddToAccount(sqlUpdateStatus);
    }


    #endregion

    #region general methods
    [RelayCommand]
    private void CreateQuote() {
        MessageBoxResult result;
        result = MessageBox.Show("Do You want to create a Quote?", "?Create Quote?", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes) {

        }
        else return;
    }
    private void OpenCustomerContractList() {
        string customerName = ExcelCustomerContractsPopupDatatable.Rows[0]["Customer"].ToString();

        AccountListWindow contractList = new() {
            DataContext = _navigationService.CurrentViewModel = new AccountListViewModel(_navigationService,
                                                                ExcelCustomerContractsPopupDatatable, customerName,
                                                                ExcelCustomerAssetsPopupDatatable)
        };
        //maybe ExcelCustomerAssetsPopupDatatable not ExcelAssetsExtendedDatatable ExcelCustomerAssetsPopupDatatable ExcelAssetsExtendedDatatable
        contractList.ShowDialog();

    }



    [RelayCommand(CanExecute = nameof(ExpireListCanExecute))]
    private void ExpireList() {

    }
    private bool ExpireListCanExecute => false;


    [RelayCommand(CanExecute = nameof(GoToAccounListCanExecute))]
    private void GoToAccounList() {

    }
    private bool GoToAccounListCanExecute => false;


    private void GetServicePlanDictionary() {
        dictServicePlans = new();

        string sqlStatements =
            "SELECT [ServicePlanID], [ServicePlanNumber] " +
            "FROM [tblServicePlan] ";

        DataTable dt = AccessDB.FetchDBRecordRequest(sqlStatements);


    }

    #endregion

}

public class AddServicePlanToAccess {
   
    public string planNumber { get; set; }
    public string acctName { get; set; }

   

    public AddServicePlanToAccess() {
       
            
    }

    //public string wrapUpComment(string customer = null, string sn = null, string type = null) {
    //    StringBuilder sb = new StringBuilder();

    //}

    public string wrapUpComment(int count, List<AddServicePlanToAccess> sbList, string plan) {
        StringBuilder addedbuilder = new StringBuilder();

        addedbuilder.AppendLine($"{plan} added: ");

        foreach (AddServicePlanToAccess item in sbList) {

            if (plan == "Service Plans") { addedbuilder.AppendLine($"- {item.planNumber}   - {item.acctName} "); }
            else if (plan == "Account Numbers") { addedbuilder.AppendLine($"- {item.acctName}   - {item.planNumber} "); }

        }
        string listContent = addedbuilder.ToString();

        MessageBox.Show($"Done with Compare of {plan}: \n" +
            $"Records updated: {count} \n" +
            $"{listContent} ");
    }

}

public class AddSerialToContract {

    public string acctName { get; set; }
    public string serial { get; set; }
    public string product { get; set; }

   Dictionary<string, string> newContractAsset { get; set; }

    public AddSerialToContract() {
        
    }

    public string wrapUpComment(string customer = null, string plan = null, string sn = null, string type = null) {
        StringBuilder sb = new StringBuilder();

        if (customer != null) {
            sb.Append($"{customer} ")
        }
        if (plan != null) {
            sb.Append()
        }
        if (sn != null) {
            sb.Append()
        }
        if (type != null) {
            sb.Append()
        }



        string lineOfContent = sb.ToString();
        return lineOfContent;
    }

    //public string wrapUpComment(int count, AddSerialToContract sbList, string plan) {
    //    StringBuilder addedbuilder = new StringBuilder();

    //    addedbuilder.AppendLine($"{plan} added: ");

    //    foreach (AddServicePlanToAccess item in sbList) {

    //        if (plan == "Service Plans") { addedbuilder.AppendLine($"- {item.planNumber}   - {item.acctName} "); }
    //        else if (plan == "Account Numbers") { addedbuilder.AppendLine($"- {item.acctName}   - {item.planNumber} "); }

    //    }
    //    string listContent = addedbuilder.ToString();

    //    MessageBox.Show($"Done with Compare of {plan}: \n" +
    //        $"Records updated: {count} \n" +
    //        $"{listContent} ");
    //}

}
