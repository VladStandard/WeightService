using System.Text;

namespace Ws.Domain.Models.Models;

public sealed class FieldBinaryModel
{
    public byte[] Value { get; set; }
    
    public string ValueUnicode
    {
        get => Encoding.Unicode.GetString(Value);
        set => Value = Encoding.Unicode.GetBytes(value);
    }

    public FieldBinaryModel()
    {
        Value = Array.Empty<byte>();
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        FieldBinaryModel item = (FieldBinaryModel)obj;
        return Value.SequenceEqual(item.Value);
    }
    
    public override int GetHashCode() => Value.GetHashCode();
}