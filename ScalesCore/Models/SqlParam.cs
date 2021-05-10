// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesCore.Models
{
    /// <summary>
    /// SQL-параметр.
    /// </summary>
    public class SqlParam//<T> where T : IConvertible
    {
        public SqlParam()
        {
        }

        public SqlParam(string name, object value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Имя поля.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Значение.
        /// </summary>
        //public T Value { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            //return Convert.ChangeType(Value, typeof(string)).ToString();
            return Value.ToString();
        }
    }
}
