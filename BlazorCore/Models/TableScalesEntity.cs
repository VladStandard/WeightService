// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorCore.Models
{
    public class TableScalesEntity : ITableEntity
    {
        public EnumTableScale Value { get; set; }

        public TableScalesEntity(EnumTableScale value)
        {
            Value = value;
        }

        public override string ToString()
        {

            return
                $"{nameof(Value)}: {Value}. ";
        }
    }
}
