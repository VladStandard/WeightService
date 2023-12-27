namespace Ws.StorageCore.Views.ViewDiagModels.TableSize;

[DebuggerDisplay("{ToString()}")]
public sealed record SqlViewTableSizeModel
{
    public string SchemaTable { get; init; }
    public string Schema { get; init; }
    public string Table { get; init; }
    public uint RowsCount { get; init; }
    public ushort UsedSpaceMb { get; init; }
    public ushort UnusedSpaceMb { get; init; }
    public ushort TotalSpaceMb { get; init; }
    public string FileName { get; init; }
    
    public SqlViewTableSizeModel(string schemaTable, string schema, string table, uint rowsCount,
        ushort usedSpaceMb, ushort unusedSpaceMb, ushort totalSpaceMb, string fileName)
    {
        SchemaTable = schemaTable;
        Schema = schema;
        Table = table;
        RowsCount = rowsCount;
        UsedSpaceMb = usedSpaceMb;
        UnusedSpaceMb = unusedSpaceMb;
        TotalSpaceMb = totalSpaceMb;
        FileName = fileName;
    }
    
    public override string ToString() => $"{Schema} | {Table} | {RowsCount} | {UsedSpaceMb} | {UnusedSpaceMb} | {TotalSpaceMb}";
}