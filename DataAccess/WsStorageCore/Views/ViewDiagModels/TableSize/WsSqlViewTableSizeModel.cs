namespace WsStorageCore.Views.ViewDiagModels.TableSize;

[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlViewTableSizeModel
{
    public string SchemaTable { get; init; }
    public string Schema { get; init; }
    public string Table { get; init; }
    public uint RowsCount { get; init; }
    public ushort UsedSpaceMb { get; init; }
    public ushort UnusedSpaceMb { get; init; }
    public ushort TotalSpaceMb { get; init; }
    public string FileName { get; init; }
    
    #region Public and private fields, properties, constructor
    
    public WsSqlViewTableSizeModel() : this(string.Empty, string.Empty, string.Empty, 
        default, default, default, default, string.Empty) { }
    
    public WsSqlViewTableSizeModel(string schemaTable, string schema, string table, uint rowsCount,
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
    
    #endregion
    
    #region Public and private methods - override

    public override string ToString() => $"{Schema} | {Table} | {RowsCount} | {UsedSpaceMb} | {UnusedSpaceMb} | {TotalSpaceMb}";

    #endregion
}