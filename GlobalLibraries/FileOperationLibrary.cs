using ContractWork.csproj.ExtensionMethods;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Syroot.Windows.IO;
using System.IO;
using System.Windows;


namespace GlobalLibraries;
public static class FileOperationLibrary {

    #region Properties
    public static readonly string nl = Environment.NewLine;
    public static readonly string tb = "\t";

    public static Dictionary<string, string> FileAndFolderLocations { get; set; } = new();
    public static string UserConfigJson { get; set; } = string.Empty;
    public static string FolderPath { get; set; } = null;
    public static string SelectedFolderPath { get; set; } = string.Empty;
    public static string SelectedFilePath { get; set; } = string.Empty;
    public static string CurrentDirectory { get; set; } = string.Empty;
    public static string DocumentDirectory { get; set; } = string.Empty;
    public static string CurrentFileLocation { get; set; } = string.Empty;
    public static string UserConfigLocation { get; set; } = string.Empty;


    public static Timer timer;

    #endregion


    public static (string filePath, bool failed) getFile(string passedPath = null, string passedExt = null, string knownFolder = null) {
        bool didItFail = false;
        string fileFilter = passedExt ?? "All Files | *.*";


        if (passedPath != null && Directory.Exists(passedPath)) { }
        else if (passedPath != null && !Directory.Exists(passedPath)) { MessageBox.Show($"Directory doesn't exist : {passedPath}"); 
            passedPath = null;
        }

        if (passedPath == null) {
            DocumentDirectory = new KnownFolder(KnownFolderType.Documents).Path;
            if (FolderPath == null || knownFolder != null) {
                if (knownFolder == "Downloads") { FolderPath = new KnownFolder(KnownFolderType.Downloads).Path; }
                else if (knownFolder == "Documents") { FolderPath = new KnownFolder(KnownFolderType.Documents).Path; }
                else if (knownFolder == "VBA files and images") { 
                    FolderPath = @"D:\Cloud Services\NextCloud\Physio Work\VBA files and images\Access Customer DB files\Best version at ths time"; }
                
            }
        }

        SelectedFolderPath = passedPath ?? FolderPath ?? DocumentDirectory;

        return (SelectedFilePath, didItFail) = OpenFile(fileFilter);
    }

    public static (string, bool) OpenFile(string fileFilter, string passedPath = null)               {
        MessageBoxResult result;
        bool repeat = false;
        bool didItFail = false;

        OpenFileDialog ofd = new();
        string path = passedPath ?? SelectedFolderPath;

         // this is the path that you are checking if directory exists
        if (Directory.Exists(path)) { ofd.InitialDirectory = path; }
        else {
            MessageBox.Show($"Directory doesn't exist : {path}");
            ofd.InitialDirectory = DocumentDirectory ?? @"C:\"; 
        }

        ofd.Filter = fileFilter; // | All Files | *.*";  Excel files | *.xlsx;*.xlsm"

        do {
            SelectedFilePath = (bool)ofd.ShowDialog() ? ofd.FileName : "Need to pic File";
            if (SelectedFilePath == "Need to pic File") {
                result = MessageBox.Show("Need to pic File, try again?", "?Pick file?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes) {
                    repeat = true; didItFail = false;
                }
                else { repeat = false; didItFail = true; }
            }
            else { repeat = false; didItFail = false; }

        } while (repeat == true);

        CurrentDirectory = Path.GetDirectoryName(SelectedFilePath);

        return (SelectedFilePath, didItFail);
    }

    public delegate string userDefConfig();

    public static Dictionary<string, string> SetUserConfig(string v, userDefConfig userDefaultConfig) {
        //Dictionary<string, string> filelocationDict = new();
        CurrentDirectory = Directory.GetCurrentDirectory();
        UserConfigLocation = Directory.GetCurrentDirectory() + @"\" + v;

        if (File.Exists(UserConfigLocation)) { } //File.Delete(UserConfigLocation); }
        else {
            UserConfigJson = userDefaultConfig();
            File.WriteAllText(UserConfigLocation, UserConfigJson);
        }

        UserConfigJson = UserConfigJson.refreshJsonConfig(UserConfigLocation);

        return SetFileDefaults(UserConfigJson);

    }

    public static void UpdateJsonUserConfig(string jsonKey, string jsonValue) {
        
        try {
            UserConfigJson = UserConfigJson.refreshJsonConfig(UserConfigLocation);
            JObject rss = JObject.Parse(UserConfigJson);
            rss[jsonKey] = jsonValue;
            File.WriteAllText(UserConfigLocation, rss.ToString());
        }
        catch (Exception) {
            MessageBox.Show("Something wrong with user config json");
            throw;
        }
        

    }

    private static Dictionary<string, string> SetFileDefaults(string jsonInfo) {
        JObject jobject = JObject.Parse(jsonInfo);

        FileAndFolderLocations.Add("AccessFilePath", (string)jobject["AccessFilePath"]);
        FileAndFolderLocations.Add("AccessFolderPath", (string)jobject["AccessFolderPath"]);
        FileAndFolderLocations.Add("ContractsFilePath", (string)jobject["ContractsFilePath"]);
        FileAndFolderLocations.Add("ContractsFolderPath", (string)jobject["ContractsFolderPath"]);
        FileAndFolderLocations.Add("AssetsFilePath", (string)jobject["AssetsFilePath"]);
        FileAndFolderLocations.Add("AssetsFolderPath", (string)jobject["AssetsFolderPath"]);
        FileAndFolderLocations.Add("ExtendedAssetsFilePath", (string)jobject["ExtendedAssetsFilePath"]);
        FileAndFolderLocations.Add("ExtendedAssetsFolderPath", (string)jobject["ExtendedAssetsFolderPath"]);
        FileAndFolderLocations.Add("InventoryFilePath", (string)jobject["InventoryFilePath"]);
        FileAndFolderLocations.Add("InventoryFolderPath", (string)jobject["InventoryFolderPath"]);

        return FileAndFolderLocations;
    }

    public static string NormalizePath(string path) {
        if (path != "") {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }
        else {
            return "";
        }
    }
}