// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using static DataCore.ShareEnums;

namespace DataCore.Sql.Fields;

/// <summary>
/// DB table field comparing model.
/// </summary>
public class FieldFilterModel
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Field name.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Field comparer.
    /// </summary>
    public DbComparer Comparer { get; }
    /// <summary>
    /// Field value.
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="field"></param>
    /// <param name="comparer"></param>
    /// <param name="value"></param>
    public FieldFilterModel(DbField field, DbComparer comparer, object? value)
    {
        Name = field.ToString();
        Comparer = comparer;
        Value = value;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    public FieldFilterModel(DbField field, object? value)
    {
        Name = field.ToString();
        Comparer = DbComparer.Equal;
        Value = value;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="comparer"></param>
    /// <param name="value"></param>
    /// <param name="valueType"></param>
    public FieldFilterModel(string name, DbComparer comparer, object? value, Type? valueType = null)
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
    public virtual bool Equals(FieldFilterModel item)
    {
		if (Value == null && item.Value != null) return false;
        if (Value != null && item.Value == null) return false;
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
        return Equals((FieldFilterModel)obj);
    }

    /// <summary>
    /// Get hash code.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => (Name, Comparer, Value).GetHashCode();

    /// <summary>
    /// To string override.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Comparer)}: {Comparer}. " +
        $"{nameof(Value)}: {Value}. ";

    #endregion
}
