// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Enums;

public class WsEnumTypeModel<T>
{
    private string Name { get; }
    private T Value { get; }

    public WsEnumTypeModel(string name, T value)
    {
        Name = name;
        Value = value;
    }

    public override string ToString() => $"{Name} | {Value}";
}
