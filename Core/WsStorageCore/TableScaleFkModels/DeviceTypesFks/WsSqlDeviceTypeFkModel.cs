// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsSqlTableBase = WsStorageCore.Common.WsSqlTableBase;

namespace WsStorageCore.TableScaleFkModels.DeviceTypesFks;

/// <summary>
/// Table "DEVICES_TYPES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceTypeFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlDeviceModel Device { get; set; }
    [XmlElement] public virtual WsSqlDeviceTypeModel Type { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceTypeFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Device = new();
        Type = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlDeviceTypeFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Device = (WsSqlDeviceModel)info.GetValue(nameof(Device), typeof(WsSqlDeviceModel));
        Type = (WsSqlDeviceTypeModel)info.GetValue(nameof(WsSqlDeviceTypeModel), typeof(WsSqlDeviceTypeModel));
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
        return Equals((WsSqlDeviceTypeFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Device.EqualsDefault() &&
        Type.EqualsDefault();

    public override object Clone()
    {
        WsSqlDeviceTypeFkModel item = new();
        item.CloneSetup(this);
        item.Device = Device.CloneCast();
        item.Type = Type.CloneCast();
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
        info.AddValue(nameof(Device), Device);
        info.AddValue(nameof(Type), Type);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Device.FillProperties();
        Type.FillProperties();
    }

    public virtual void UpdateProperties(WsSqlDeviceTypeFkModel deviceTypeFk)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(deviceTypeFk, true);
        
        Device = deviceTypeFk.Device;
        Type = deviceTypeFk.Type;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceTypeFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Device.Equals(item.Device) &&
        Type.Equals(item.Type);

    public new virtual WsSqlDeviceTypeFkModel CloneCast() => (WsSqlDeviceTypeFkModel)Clone();

    #endregion
}