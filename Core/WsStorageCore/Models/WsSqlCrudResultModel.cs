// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

/// <summary>
/// SQL result for CRUD operations.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlCrudResultModel(bool IsOk, Exception? Exception = null)
{
    #region Public and private fields, properties, constructor

    public WsSqlCrudResultModel() : this(true) { }

    #endregion

    #region Public and private methods

    public override int GetHashCode() => (IsOk, Exception?.ToString().ToUpper() ?? null).GetHashCode();

    public override string ToString() => Exception is null ? $"{IsOk}" : $"{IsOk} | {Exception}";

    #endregion
}