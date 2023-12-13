namespace Ws.Shared.Enums;

public class EnumTypeModel<T>
{
    public string Name { get; set; }
    public T Value { get; set; }

    public EnumTypeModel(string name, T value)
    {
        Name = name;
        Value = value;
    }

    public override string ToString() => $"{Name} | {Value}";
}
