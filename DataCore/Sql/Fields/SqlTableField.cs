// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Fields;

/// <summary>
/// SQL table field.
/// </summary>
/// <typeparam name="T"></typeparam>
public class SqlTableField<T> where T : IConvertible
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Field name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Field value.
    /// </summary>
    public T? Value { get; set; }

    /// <summary>
    /// Field default value.
    /// </summary>
    public T? DefaultValue { get; }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    public SqlTableField() : this(string.Empty, default, default) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="defValue"></param>
    /// <exception cref="ArgumentException"></exception>
    public SqlTableField(string name, T? value, T? defValue)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(name);

        Name = name;
        Value = value;
        DefaultValue = defValue;

        if (typeof(T) == typeof(string))
        {
            if (Value == null)
                Value = (T)Convert.ChangeType(string.Empty, typeof(T));
            if (DefaultValue == null)
                DefaultValue = (T)Convert.ChangeType(string.Empty, typeof(T));
        }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public SqlTableField(string name, T? value) : this(name, value, value) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    public SqlTableField(string name) : this(name, default, default) { }

    #endregion

    #region Public and private methods

    /// <summary>
    /// To string override.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Value is string value ? value : string.Empty;
    }

    #endregion
}
