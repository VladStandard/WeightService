// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;

namespace DataCore.Models
{
    public class TableSystemEntity : ITableEntity
    {
        public EnumTableSystem Value { get; set; }

        public TableSystemEntity(EnumTableSystem value)
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
