// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Models;

/// <summary>
/// SQL result for CRUD operations.
/// </summary>
[DebuggerDisplay("Type = {nameof(SqlCrudResultModel)} | {IsOk} | {Exception}")]
public sealed record SqlCrudResultModel
{
    public bool IsOk { get; init; }
    public Exception? Exception { get; init; }

    public override int GetHashCode() => (IsOk, Exception?.ToString().ToUpper() ?? null).GetHashCode();
}