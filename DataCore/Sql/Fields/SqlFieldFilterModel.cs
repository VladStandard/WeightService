// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Fields;

/// <summary>
/// DB table field comparing model.
/// </summary>
[Serializable]
public class SqlFieldFilterModel
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Field name.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Field comparer.
    /// </summary>
    public SqlFieldComparerEnum Comparer { get; }
    /// <summary>
    /// Field value.
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="comparer"></param>
    /// <param name="value"></param>
    public SqlFieldFilterModel(string name, SqlFieldComparerEnum comparer, object? value)
    {
        Name = name;
        Comparer = comparer;
        Value = value;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public SqlFieldFilterModel(string name, object? value)
    {
        Name = name;
        Comparer = SqlFieldComparerEnum.Equal;
        Value = value;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="comparer"></param>
    /// <param name="value"></param>
    /// <param name="valueType"></param>
    public SqlFieldFilterModel(string name, SqlFieldComparerEnum comparer, object? value, Type? valueType = null)
    {
        Name = name;
        Comparer = comparer;
        Value = value;
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Equals.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public virtual bool Equals(SqlFieldFilterModel item)
    {
        if (Value == null && item.Value is not null) return false;
        if (Value is not null && item.Value == null) return false;
        if (ReferenceEquals(this, item)) return true;
        return
            Equals(Name, item.Name) &&
            Equals(Value, item.Value);
    }

    /// <summary>
    /// Equals.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlFieldFilterModel)obj);
    }

    /// <summary>
    /// Get hash code.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => (Name, Comparer, Value).GetHashCode();

    public override string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Comparer)}: {Comparer}. " +
        $"{nameof(Value)}: {Value}. ";

    #endregion
}
