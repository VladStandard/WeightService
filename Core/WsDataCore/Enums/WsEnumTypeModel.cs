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
