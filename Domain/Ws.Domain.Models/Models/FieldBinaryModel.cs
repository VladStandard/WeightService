using System.Text;
using Ws.Shared.Utils;

namespace Ws.Domain.Models.Models;

public sealed class FieldBinaryModel
{
    public byte[]? Value { get; set; }
    
    public string ValueUnicode
    {
        get => Value is null || Value.Length == 0 || Value.Equals(Array.Empty<byte>()) ? string.Empty : Encoding.Unicode.GetString(Value);
        set => Value = Encoding.Unicode.GetBytes(value);
    }

    public FieldBinaryModel()
    {
        Value = Array.Empty<byte>();
    }

    public FieldBinaryModel(FieldBinaryModel item)
    {
        Value = DataUtil.ByteClone(item.Value);
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FieldBinaryModel)obj);
    }

    public override int GetHashCode() => Value is not null ? Value.GetHashCode() : 0;

    public bool Equals(FieldBinaryModel item) =>
        item.Value is not null && Value is not null && (ReferenceEquals(this, item) || DataUtil.ByteEquals(Value, item.Value));
}