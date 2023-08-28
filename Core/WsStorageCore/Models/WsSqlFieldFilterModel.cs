namespace WsStorageCore.Models;

[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlFieldFilterModel()
{
    #region Public and private fields, properties, constructor

    public string Name { get; init; } = string.Empty;
    public WsSqlEnumFieldComparer Comparer { get; init; } = WsSqlEnumFieldComparer.Equal;
    public object? Value { get; init; }
    public List<object> Values { get; init; } = new();

    public WsSqlFieldFilterModel(WsSqlFieldFilterModel item)
    {
        Name = item.Name;
        Comparer = item.Comparer;
        Value = item.Value;
        Values = new(item.Values);
    }

    #endregion

    #region Public and private methods

    public override int GetHashCode() => (Name.ToUpper(), Comparer, Value?.ToString().ToUpper() ?? null).GetHashCode();
    
    public override string ToString() => $"{Name} {Comparer} {Value}";

    #endregion
}