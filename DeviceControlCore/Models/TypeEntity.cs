using System;

namespace DeviceControlCore.Models
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
                $"{nameof(Name)}: {Name}. " + Environment.NewLine +
                $"{nameof(Value)}: {Value}. ";
        }
    }
}