namespace WsStorageCore.Models;

public class WsSqlFieldBinaryModel
{
    #region Public and private fields, properties, constructor
    
    public virtual byte[]? Value { get; set; }

    
    public virtual string ValueUnicode
    {
        get => Value is null || Value.Length == 0 || Value.Equals(Array.Empty<byte>()) ? string.Empty : Encoding.Unicode.GetString(Value);
        set => Value = Encoding.Unicode.GetBytes(value);
    }

    public virtual string Info { get => WsDataUtils.GetBytesLength(Value, true); set => _ = value; }

    public WsSqlFieldBinaryModel()
    {
        Value = Array.Empty<byte>();
    }
    

    public WsSqlFieldBinaryModel(WsSqlFieldBinaryModel item)
    {
        Value = WsDataUtils.ByteClone(item.Value);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Info)}: {Info}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlFieldBinaryModel)obj);
    }

    public override int GetHashCode() => Value is not null ? Value.GetHashCode() : 0;
    
    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlFieldBinaryModel item) =>
        item.Value is not null && Value is not null && (ReferenceEquals(this, item) || WsDataUtils.ByteEquals(Value, item.Value));

    #endregion
}