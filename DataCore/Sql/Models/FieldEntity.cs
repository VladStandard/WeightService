// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NHibernate.Criterion;
using System;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Models;

/// <summary>
/// DB field entity.
/// </summary>
public class FieldEntity
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
    public FieldEntity(DbField field, DbComparer comparer, object? value)
    {
        Name = field.ToString();
        Comparer = comparer;
        Value = value;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="comparer"></param>
    /// <param name="value"></param>
    /// <param name="valueType"></param>
    public FieldEntity(string name, DbComparer comparer, object? value, Type? valueType = null)
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
    public virtual bool Equals(FieldEntity item)
    {
        //if (item is null) return false;
        if (Value == null && item.Value != null) return false;
        if (Value != null && item.Value == null) return false;
        if (ReferenceEquals(this, item)) return true;
        return Name.Equals(item.Name) &&
            object.Equals(Value, item.Value);
    }

    /// <summary>
    /// Equals.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        //if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FieldEntity)obj);
    }

    /// <summary>
    /// Get hash code.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Name.GetHashCode();

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
