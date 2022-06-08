// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCore.Sql.Models
{
    /// <summary>
    /// SQL table field.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlTableField<T> where T : IConvertible
    {
        #region Public and private fields and properties

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

        #endregion

        #region Constructor and destructor

        /// <summary>
        /// Constructor for serialization.
        /// </summary>
        public SqlTableField() : this(string.Empty, default, default) { }

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

        public SqlTableField(string name, T? value) : this(name, value, value) { }

        public SqlTableField(string name) : this(name, default, default) { }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return Value is string value ? value : string.Empty;
        }

        #endregion
    }
}
