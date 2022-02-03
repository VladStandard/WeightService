// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Models
{
    public class TypeEntity<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }

        public TypeEntity(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}. " +
                $"{nameof(Value)}: {Value}. ";
        }
    }
}
