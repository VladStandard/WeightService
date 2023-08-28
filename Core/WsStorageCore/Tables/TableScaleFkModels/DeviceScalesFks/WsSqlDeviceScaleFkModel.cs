namespace WsStorageCore.Tables.TableScaleFkModels.DeviceScalesFks;

/// <summary>
/// Table "DEVICES_SCALES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceScaleFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlDeviceModel Device { get; set; }
    [XmlElement] public virtual WsSqlScaleModel Scale { get; set; }

    public WsSqlDeviceScaleFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Device = new();
        Scale = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlDeviceScaleFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Device = (WsSqlDeviceModel)info.GetValue(nameof(Device), typeof(WsSqlDeviceModel));
        Scale = (WsSqlScaleModel)info.GetValue(nameof(Scale), typeof(WsSqlScaleModel));
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

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Device), Device);
        info.AddValue(nameof(Scale), Scale);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Device.FillProperties();
        Scale.FillProperties();
    }

    public virtual void UpdateProperties(WsSqlDeviceScaleFkModel item)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(item, true);
        
        Device = new(item.Device);
        Scale = new(item.Scale);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceScaleFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Device.Equals(item.Device) &&
        Scale.Equals(item.Scale);

    #endregion
}