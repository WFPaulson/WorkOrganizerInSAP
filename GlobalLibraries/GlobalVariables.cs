 namespace GlobalLibraries;
public static class GlobalVariables {
    public static Dictionary<int, string> _modelLookUP = new Dictionary<int, string> {
            { 1, "LP35" },
            { 2, "LP15" },
            { 3, "Lucas" },
            { 4, "LP1000" },
            { 5, "LP20" },
            { 6, "LPCR Plus" },
            { 7, "LPCR2" },
            { 8, "Cot" },
            { 9, "Power Load" },
            { 10, "Stair Chair" },
            { 0, "" }
        };

    public static Dictionary<string, int> _reversemodelLookUP = new() {
            { "LP35", 1 },
            { "LP15", 2 },
            { "Lucas", 3 },
            { "LP1000", 4 },
            { "LP20", 5 },
            { "LPCR Plus", 6 },
            { "LPCR2", 7 },
            { "Cot", 8 },
            { "Power Load", 9 },
            { "Stair Chair" , 10 },
            { "", 0 }
        };

    public static Dictionary<int, string> _versionLookUP = new() {
            { 1, "v4" },
            { 2, "v2" },
            { 3, "v1" },
            { 4, "3.1" },
            { 5, "2.2" },
            { 6, "2.1" },
            { 7, "2.0" },
            { 8, "1" },
            { 9, "1" },
            { 10, "1" },
            { 11, "1" },
            {0, "" }
        };

    public static Dictionary<string, int> _reverseVersionLookUP = new Dictionary<string, int> {
            { "v4", 1 },
            { "v2", 2 },
            { "v1", 3 },
            { "3.1", 4 },
            { "2.2", 5 },
            { "2.1", 6 },
            { "2.0", 7 },
            { "1", 8 },
            { "", 0 }
        };

    public static Dictionary<string, int> _monthNumberLookup = new Dictionary<string, int> {
            {"No Date", 0},
            {"Jan", 1 },
            {"Feb", 2 },
            {"Mar", 3 },
            {"Apr", 4 },
            {"May", 5 },
            {"Jun", 6 },
            {"Jul", 7 },
            {"Aug", 8 },
            {"Sep", 9 },
            {"Oct", 10 },
            {"Nov", 11 },
            {"Dec", 12 }
        };

    public static Dictionary<int, string> _monthNameLookup = new Dictionary<int, string> {
            {0, "No Date"},
            {1 , "Jan" },
            {2 , "Feb" },
            {3 , "Mar" },
            {4 , "Apr" },
            {5 , "May" },
            {6 , "Jun" },
            {7 , "Jul" },
            {8 , "Aug" },
            {9 , "Sep" },
            {10 , "Oct" },
            {11 , "Nov" },
            {12 , "Dec" }

        };

    public static List<string> _modelList = new List<string> {
            { "LP35" },
            { "LP15" },
            { "Lucas" },
            { "LP1000" },
            { "LP20" },
            { "LPCR Plus" },
            { "LPCR2" },
            { "Cot" },
            { "Power Load" },
            { "Stair Chair" },
        };

    public static List<string> _statusList = new List<string> {
            { "Active" },
            { "Cancelled" },
            { "Current" },
            { "Expired" },
            { "Future" },
            { "In Work" },
            { "No Contract" },
            { "PO Expired" },
            { "Renewed" }
        };
    //{ "Future Contract" },

    public static List<string> _pmMonthList = new List<string> {
            {"No Date"},
            {"Jan-1"},
            {"Feb-2"},
            {"Mar-3"},
            {"Apr-4"},
            {"May-5"},
            {"Jun-6"},
            {"Jul-7"},
            {"Aug-8"},
            {"Sep-9"},
            {"Oct-10"},
            {"Nov-11"},
            {"Dec-12"}
        };



    public static List<string> LP15ABBAList;
    public static List<string> LP20ABBAList;
    public static List<string> LPCR2ABBAList;
    public static List<string> LucasABBAList = new List<string> {
            "No ABBA String"
        };

    public static string _popupClosedByX;

    
}
