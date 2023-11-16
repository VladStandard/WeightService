namespace Ws.DataCore.Enums;

public class 
    EnumTypeModel<T>
{
    private string Name { get; }
    private T Value { get; }

    public EnumTypeModel(string name, T value)
    {
        Name = name;
        Value = value;
    }

    public override string ToString() => $"{Name} | {Value}";
}
