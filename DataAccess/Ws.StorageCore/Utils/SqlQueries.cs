namespace Ws.StorageCore.Utils;

public static class SqlQueries
{
    public static string GetTopRecords(int topRecords) => topRecords == 0 ? string.Empty : $"TOP {topRecords}";
    
    public static string TrimQuery(string queryString) =>
        queryString.TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
}