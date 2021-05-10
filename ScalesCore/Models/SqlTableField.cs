// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace ScalesCore.Models
{
    /// <summary>
    /// Поле таблицы.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlTableField<T> where T : IConvertible
    {
        public SqlTableField(string name, T value, T defValue)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(name);

            Name = name;
            Value = value;
            Default = defValue;

            if (typeof(T) == typeof(string))
            {
                if (Value == null)
                    Value = (T)Convert.ChangeType(string.Empty, typeof(T));
                if (Default == null)
                    Default = (T)Convert.ChangeType(string.Empty, typeof(T));
            }
        }

        public SqlTableField(string name, T value) : this(name, value, value) { }

        public SqlTableField(string name) : this(name, default(T), default(T)) { }

        /// <summary>
        /// Конструктор без параметров нужен для сериализации.
        /// </summary>
        public SqlTableField() : this(string.Empty, default(T), default(T)) { }

        /// <summary>
        /// Имя поля.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Значение.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Значение по-умолчанию.
        /// </summary>
        public T Default { get; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
