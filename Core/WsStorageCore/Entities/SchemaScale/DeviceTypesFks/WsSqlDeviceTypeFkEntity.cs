namespace WsStorageCore.Entities.SchemaScale.DeviceTypesFks;

[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceTypeFkEntity : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlDeviceEntity Device { get; set; }
    public virtual WsSqlDeviceTypeModel Type { get; set; }
    
    public WsSqlDeviceTypeFkEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Device = new();
        Type = new();
    }

    public WsSqlDeviceTypeFkEntity(WsSqlDeviceTypeFkEntity item) : base(item)
    {
        Device = new(item.Device);
        Type = new(item.Type);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Device)}: {Device}. " +
        $"{nameof(Type)}: {Type}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlDeviceTypeFkEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Device.EqualsDefault() &&
        Type.EqualsDefault();
    
    public override void FillProperties()
    {
        base.FillProperties();
        Device.FillProperties();
        Type.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceTypeFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Device.Equals(item.Device) &&
        Type.Equals(item.Type);

    #endregion
}