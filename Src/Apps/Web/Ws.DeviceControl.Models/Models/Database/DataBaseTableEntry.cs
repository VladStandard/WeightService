namespace Ws.DeviceControl.Models.Models.Database;

public sealed record DataBaseTableEntry
{
    public string Schema;
    public string Table;
    public int Rows;
    public int UsedMb;
    public string FileName;
}