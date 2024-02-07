namespace Ws.Shared.Enums;

public class EnumTypeModel<T>(string name, T value)
{
    public string Name { get; } = name;
    public T Value { get; } = value;

    public override string ToString() => $"{Name} | {Value}";
}
