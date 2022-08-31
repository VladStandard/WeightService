// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace MdmControlCore;

public class TypeModel<T>
{
    public string Name { get; init; }
    public T Value { get; init; }

    public TypeModel(string name, T value)
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
