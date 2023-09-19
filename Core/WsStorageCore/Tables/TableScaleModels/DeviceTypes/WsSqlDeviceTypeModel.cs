namespace WsStorageCore.Tables.TableScaleModels.DeviceTypes;

[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceTypeModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual string PrettyName { get; set; }
    
    public WsSqlDeviceTypeModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        PrettyName = string.Empty;
    }
    
    public WsSqlDeviceTypeModel(WsSqlDeviceTypeModel item) : base(item)
    {
        PrettyName = item.PrettyName;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlDeviceTypeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(PrettyName, string.Empty);
    
    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceTypeModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(PrettyName, item.PrettyName);

    #endregion
}