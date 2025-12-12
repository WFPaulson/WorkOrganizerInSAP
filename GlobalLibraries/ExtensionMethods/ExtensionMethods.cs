using Newtonsoft.Json;
using System.IO;

namespace ContractWork.csproj.ExtensionMethods;
public static class ExtensionMethods {

    public static string refreshJsonConfig(this string jsonConfig, string configLocation) {
        jsonConfig = string.Empty;
        jsonConfig = File.ReadAllText(configLocation);
        return jsonConfig;

    }

    private static readonly string Null = "null";
    private static readonly string Exception = "Exception";

    public static string toJson(this object value, Formatting formatting = Formatting.None) {

        if (value == null) return Null;
        try {
            return JsonConvert.SerializeObject(value, formatting);
        }
        catch {
            return Exception;
        }
    }

    public static T FromJson<T>(this object obj) {
        return JsonConvert.DeserializeObject<T>(obj as string);
    }

    public static T FromJson<T>(this string json) {
        return JsonConvert.DeserializeObject<T>(json);
    }


}
