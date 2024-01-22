namespace Ws.Database.Core.Utils;

public static class SqlQueries
{
    public static string TrimQuery(string queryString) =>
        queryString.TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
}