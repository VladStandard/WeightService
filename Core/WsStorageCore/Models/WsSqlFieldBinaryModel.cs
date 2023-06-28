// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

[Serializable]
public class WsSqlFieldBinaryModel : WsSqlFieldBase, ICloneable, IWsSqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlElement(IsNullable = true)] public virtual byte[]? Value { get; set; }

    [XmlIgnore]
    public virtual string ValueAscii
    {
        get => Value is null || Value.Length == 0 || Value.Equals(Array.Empty<byte>()) ? string.Empty : Encoding.Default.GetString(Value);
        set => Value = Encoding.Default.GetBytes(value);
    }

    [XmlElement]
    public virtual string ValueUnicode
    {
        get => Value is null || Value.Length == 0 || Value.Equals(Array.Empty<byte>()) ? string.Empty : Encoding.Unicode.GetString(Value);
        set => Value = Encoding.Unicode.GetBytes(value);
    }

    [XmlIgnore] public virtual string Info { get => WsDataUtils.GetBytesLength(Value, true); set => _ = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlFieldBinaryModel()
    {
        FieldName = nameof(WsSqlFieldBinaryModel);
        Value = Array.Empty<byte>();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlFieldBinaryModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Value = (byte[])info.GetValue(nameof(Value), typeof(byte[]));
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

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() => Value is not null && WsDataUtils.ByteEquals(Value, Array.Empty<byte>());

    public override object Clone()
    {
        WsSqlFieldBinaryModel item = new()
        {
            Value = Value is not null ? WsDataUtils.ByteClone(Value) : Array.Empty<byte>()
        };
        return item;
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Value), Value);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlFieldBinaryModel item) =>
        item.Value is not null && Value is not null && (ReferenceEquals(this, item) || WsDataUtils.ByteEquals(Value, item.Value));

    public new virtual WsSqlFieldBinaryModel CloneCast() => (WsSqlFieldBinaryModel)Clone();

    #endregion
}