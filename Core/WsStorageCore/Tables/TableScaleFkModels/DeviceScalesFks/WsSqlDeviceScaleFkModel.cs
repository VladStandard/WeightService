namespace WsStorageCore.Tables.TableScaleFkModels.DeviceScalesFks;

/// <summary>
/// Table "DEVICES_SCALES_FK".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceScaleFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlDeviceModel Device { get; set; }
    public virtual WsSqlScaleModel Scale { get; set; }

    public WsSqlDeviceScaleFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Device = new();
        Scale = new();
    }
    
    public WsSqlDeviceScaleFkModel(WsSqlDeviceScaleFkModel item) : base(item)
    {
        Device = new(item.Device);
        Scale = new(item.Scale);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Device)}: {Device}. " +
        $"{nameof(Scale)}: {Scale}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlDeviceScaleFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Device.EqualsDefault() &&
        Scale.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        Device.FillProperties();
        Scale.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceScaleFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Device.Equals(item.Device) &&
        Scale.Equals(item.Scale);

    #endregion
}