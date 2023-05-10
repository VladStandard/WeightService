// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewDiagModels;

[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlViewTableSizeMemory
{
    #region Public and private fields, properties, constructor

    public string SchemaTable { get; init; }
    public string Schema { get; init; }
    public string Table { get; init; }
    public uint RowsCount { get; init; }
    public ushort UsedSpaceMb { get; init; }
    public ushort UnusedSpaceMb { get; init; }
    public ushort TotalSpaceMb { get; init; }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public WsSqlViewTableSizeMemory() : this("", "", "", 0, 0, 0)
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="schemaTable"></param>
    /// <param name="schema"></param>
    /// <param name="table"></param>
    /// <param name="rowsCount"></param>
    /// <param name="usedSpaceMb"></param>
    /// <param name="unusedSpaceMb"></param>
    /// <param name="totalSpaceMb"></param>
    public WsSqlViewTableSizeMemory(string schemaTable = "", string schema = "", string table = "",
        uint rowsCount = 0, ushort usedSpaceMb = 0, ushort unusedSpaceMb = 0, ushort totalSpaceMb = 0)
    {
        SchemaTable = schemaTable;
        Schema = schema;
        Table = table;
        RowsCount = rowsCount;
        UsedSpaceMb = usedSpaceMb;
        UnusedSpaceMb = unusedSpaceMb;
        TotalSpaceMb = totalSpaceMb;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Schema)}: {Schema}. {nameof(Table)}: {Table}. {nameof(RowsCount)}: {RowsCount}. " +
        $"{nameof(UsedSpaceMb)}: {UsedSpaceMb}. {nameof(UnusedSpaceMb)}: {UnusedSpaceMb}. {nameof(TotalSpaceMb)}: {TotalSpaceMb}";

    #endregion
}