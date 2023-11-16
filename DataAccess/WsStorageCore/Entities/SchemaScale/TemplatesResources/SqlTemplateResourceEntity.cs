namespace WsStorageCore.Entities.SchemaScale.TemplatesResources;

/// <summary>
/// Table "TEMPLATES_RESOURCES".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class SqlTemplateResourceEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual string Type { get; set; }
    public virtual FieldBinaryModel Data { get; set; }

    public virtual byte[] DataValue { get => Data.Value ?? Array.Empty<byte>(); set => Data.Value = value; }
    
    public SqlTemplateResourceEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Type = string.Empty;
        Data = new();
    }
    
    public SqlTemplateResourceEntity(SqlTemplateResourceEntity item) : base(item)
    {
        Type = item.Type;
        Data = new(item.Data);
        DataValue = DataUtils.ByteClone(item.DataValue);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Type)}: {Type}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlTemplateResourceEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Type, string.Empty) &&
        Data.Equals(new());

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlTemplateResourceEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Type, item.Type) &&
        Data.Equals(item.Data);

    #endregion
}