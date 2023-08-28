namespace WsStorageCore.Tables.TableScaleModels.TemplatesResources;

/// <summary>
/// Table "TEMPLATES_RESOURCES".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlTemplateResourceModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string Type { get; set; }
    [XmlElement] public virtual WsSqlFieldBinaryModel Data { get; set; }

    [XmlIgnore] public virtual byte[] DataValue { get => Data.Value ?? Array.Empty<byte>(); set => Data.Value = value; }
    
    public WsSqlTemplateResourceModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Type = string.Empty;
        Data = new();
    }
    
    protected WsSqlTemplateResourceModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Type = info.GetString(nameof(Type));
        Data = (WsSqlFieldBinaryModel)info.GetValue(nameof(Data), typeof(WsSqlFieldBinaryModel));
    }

    public WsSqlTemplateResourceModel(WsSqlTemplateResourceModel item) : base(item)
    {
        Type = item.Type;
        Data = new(item.Data);
        DataValue = WsDataUtils.ByteClone(item.DataValue);
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
        return Equals((WsSqlTemplateResourceModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Type, string.Empty) &&
        Data.Equals(new());

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Type), Type);
        info.AddValue(nameof(Data), Data);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Description = WsLocaleCore.Sql.SqlItemFieldDescription;
        Data.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlTemplateResourceModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Type, item.Type) &&
        Data.Equals(item.Data);

    #endregion
}