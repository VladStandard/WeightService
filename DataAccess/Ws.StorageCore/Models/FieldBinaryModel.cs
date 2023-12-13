using System;
using Ws.Shared.Utils;

namespace Ws.StorageCore.Models;

public class FieldBinaryModel
{
    #region Public and private fields, properties, constructor
    
    public virtual byte[]? Value { get; set; }

    
    public virtual string ValueUnicode
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
        Value = DataUtils.ByteClone(item.Value);
    }

    #endregion

    #region Public and private methods - override
    
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FieldBinaryModel)obj);
    }

    public override int GetHashCode() => Value is not null ? Value.GetHashCode() : 0;
    
    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(FieldBinaryModel item) =>
        item.Value is not null && Value is not null && (ReferenceEquals(this, item) || DataUtils.ByteEquals(Value, item.Value));

    #endregion
}