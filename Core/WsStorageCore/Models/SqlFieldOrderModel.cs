// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Enums;

namespace WsStorageCore.Models;

/// <summary>
/// SQL order model.
/// </summary>
[DebuggerDisplay("Type = {nameof(SqlFieldOrderModel)} | {ToString} | {Direction}")]
public sealed record SqlFieldOrderModel
{
    public string Name { get; init; } = "";
    public WsSqlOrderDirection Direction { get; init; } = WsSqlOrderDirection.Asc;

    public override int GetHashCode() => (Name.ToUpper(), Direction).GetHashCode();
}