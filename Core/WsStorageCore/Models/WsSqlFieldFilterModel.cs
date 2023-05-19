// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

/// <summary>
/// SQL field comparing model.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlFieldFilterModel
{
    #region Public and private fields, properties, constructor

    public string Name { get; init; } = "";
    public WsSqlFieldComparer Comparer { get; init; } = WsSqlFieldComparer.Equal;
    public object? Value { get; init; }
    public List<object> Values { get; init; } = new();

    #endregion

    #region Public and private methods

    public override int GetHashCode() => (Name.ToUpper(), Comparer, Value?.ToString().ToUpper() ?? null).GetHashCode();
    
    public override string ToString() => $"{Name} {Comparer} {Value}";

    #endregion
}