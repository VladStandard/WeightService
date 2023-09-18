namespace WsStorageCore.Common;

[Serializable]
public class WsSqlFieldBase : SerializeBase, IWsSqlObjectBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string FieldName { get; set; }

    public WsSqlFieldBase()
    {
        FieldName = nameof(WsSqlFieldBase);
    }

    protected WsSqlFieldBase(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        FieldName = info.GetString(nameof(FieldName));
    }

    public WsSqlFieldBase(WsSqlFieldBase item) : base(item)
    {
        FieldName = nameof(WsSqlFieldBase);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{nameof(FieldName)}: {FieldName}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlFieldBase)obj);
    }

    public override int GetHashCode() => FieldName.GetHashCode();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(FieldName), FieldName);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool EqualsNew() => Equals(new());

    public virtual bool Equals(WsSqlFieldBase item) =>
        ReferenceEquals(this, item) || Equals(FieldName, item.FieldName);

    public virtual bool EqualsDefault() => Equals(FieldName, string.Empty);

    public virtual void ClearNullProperties()
    {
        throw new NotImplementedException();
    }

    public virtual void FillProperties()
    {
        //throw new NotImplementedException();
    }

    #endregion
}
