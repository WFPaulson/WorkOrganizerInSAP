using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Servicelibraries.ExtensionMethods.ExtensionMethods;
public static class ExtensionMethods
{
    public static bool IsEmpty(this DataRow row)
    {
        return row == null || row.ItemArray.All(i => i.IsNullEquivalent());
    }
    public static bool IsNullEquivalent(this object value)
    {
        return value == null
               || value is DBNull
               || string.IsNullOrWhiteSpace(value.ToString());
    }


    public static bool ocIsNullorEmpty(this IList ObservableCollection)
    {
        return ObservableCollection == null || ObservableCollection.Count < 1;
    }

    public static bool dictIsNullOrEmpty(this IDictionary Dictionary)
    {
        return Dictionary == null || Dictionary.Count < 1;
    }

    public static bool dtISNullOrEmpty(this DataTable dataTable)
    {
        return dataTable == null || dataTable.Rows.Count < 1;
    }

    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
    }

    public static bool updateDictValue(this IDictionary dictionary, string key, object value)
    {
        if (dictionary.dictIsNullOrEmpty())
        {
            return false;
        }
        try
        {
            dictionary[key] = value;
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public static bool IsNullOrEmpty([NotNullWhen(false)] string? value) {
        return value == null || value.Length == 0;
    }

    //public static bool DatatableToClass<TClass>(this <TClass> dtToThisClass, DataTable dataTable) {

    //    foreach (DataRow row in dataTable.Rows) {

    //}

    //public static void DataTableToDict(this DataTable dataTable, out IDictionary dictTemp)
    //{
    //    //IDictionary dictTemp;

    //    string kType = dataTable.Rows[0][0].GetType().ToString();

    //    Type keyType = Type.GetType(kType);
    //    //string _key = CheckType(keyType);
    //    Type valueType = (Type)dataTable.Rows[0][1];
    //    //string _value = CheckType(valueType);
    //    //Type type = Type.GetType(inputString);

    //    foreach (DataRow row in dataTable.Rows)
    //    {
    //        dictTemp.Add(keyType, row[1]); 

    //    }

    //    //dictTemp = dataTable.AsEnumerable()
    //    //    .ToDictionary(row => row.Field<keyType>(0),
    //    //                            row => row.Field<valueType>(1));



    //    //return true;

    //}

    //private static string CheckType(Type keyType)
    //{
    //    string typeOfKeyValue = string.Empty;

    //    switch (keyType)
    //    {

    //        case typeof(string):
    //            typeOfKeyValue = "String"; break;

    //        case typeOf(int):
    //            typeOfKeyValue = "int"; break;

    //        default: break;
    //    }

    //    return typeOfKeyValue; 
    //}

    //public static bool ReportDictionary<K,V>(this DataTable dataTable, out IDictionary dictTemp)
    //{

    //    foreach (KeyValuePair<K,V> KVP in dictTemp)
    //    {
    //        Console.WriteLine("{0}: {1}", KVP.Key, KVP.Value);
    //    }

    //    return true;
    //}

}
