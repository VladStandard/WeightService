// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.Scales;

namespace DataCore.Sql.TableScaleFkModels.DeviceScalesFks;

/// <summary>
/// Table "DEVICES_SCALES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(DeviceScaleFkModel)}")]
public class DeviceScaleFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual DeviceModel Device { get; set; }
    [XmlElement] public virtual ScaleModel Scale { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceScaleFkModel() : base(SqlFieldIdentity.Uid)
    {
        Device = new();
        Scale = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected DeviceScaleFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Device = (DeviceModel)info.GetValue(nameof(Device), typeof(DeviceModel));
        Scale = (ScaleModel)info.GetValue(nameof(Scale), typeof(ScaleModel));
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
        $"{nameof(Scale)}: {Scale}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DeviceScaleFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Device.EqualsDefault() &&
        Scale.EqualsDefault();

    public override object Clone()
    {
        DeviceScaleFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Device = Device.CloneCast();
        item.Scale = Scale.CloneCast();
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
        info.AddValue(nameof(Scale), Scale);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Device.FillProperties();
        Scale.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(DeviceScaleFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Device.Equals(item.Device) &&
        Scale.Equals(item.Scale);

    public new virtual DeviceScaleFkModel CloneCast() => (DeviceScaleFkModel)Clone();

    #endregion
}