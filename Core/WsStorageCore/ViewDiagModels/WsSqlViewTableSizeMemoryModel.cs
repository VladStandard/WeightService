// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewDiagModels;

[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlViewTableSizeMemoryModel(string SchemaTable, string Schema, string Table,
    uint RowsCount, ushort UsedSpaceMb, ushort UnusedSpaceMb, ushort TotalSpaceMb) : WsSqlViewRecordBase(Guid.Empty)
{
    #region Public and private fields, properties, constructor

    public WsSqlViewTableSizeMemoryModel() : this(string.Empty, string.Empty, string.Empty,
        default, default, default, default) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{Schema} | {Table} | {RowsCount} | {UsedSpaceMb} | {UnusedSpaceMb} | {TotalSpaceMb}";

    #endregion
}