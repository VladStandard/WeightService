// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Models;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;

namespace DataCore.Sql.TableScaleFkModels.DeviceTypesFks;

/// <summary>
/// Table "DEVICES_TYPES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(DeviceTypeFkModel)}")]
public class DeviceTypeFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual DeviceModel Device { get; set; }
    [XmlElement] public virtual DeviceTypeModel Type { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceTypeFkModel() : base(WsSqlFieldIdentity.Uid)
    {
        Device = new();
        Type = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected DeviceTypeFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Device = (DeviceModel)info.GetValue(nameof(Device), typeof(DeviceModel));
        Type = (DeviceTypeModel)info.GetValue(nameof(DeviceTypeModel), typeof(DeviceTypeModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Device)}: {Device}. " +
        $"{nameof(Type)}: {Type}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DeviceTypeFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Device.EqualsDefault() &&
        Type.EqualsDefault();

    public override object Clone()
    {
        DeviceTypeFkModel item = new();
        item.CloneSetup(base.CloneCast());
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

    public override void UpdateProperties(IWsSqlTable item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_nomenclatures/.
        if (item is not DeviceTypeFkModel deviceTypeFk) return;
        Device = deviceTypeFk.Device;
        Type = deviceTypeFk.Type;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(DeviceTypeFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Device.Equals(item.Device) &&
        Type.Equals(item.Type);

    public new virtual DeviceTypeFkModel CloneCast() => (DeviceTypeFkModel)Clone();

    #endregion
}