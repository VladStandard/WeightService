// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableConfModels.DeviceSettingsFks;

/// <summary>
/// Таблица "DEVICES_SETTINGS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceSettingsFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual bool IsEnabled { get; set; }
    [XmlElement] public virtual WsSqlDeviceModel Device { get; set; }
    [XmlElement] public virtual WsSqlDeviceSettingsModel Setting { get; set; }

    public WsSqlDeviceSettingsFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Device = new();
        Setting = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlDeviceSettingsFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IsEnabled = info.GetBoolean(nameof(IsEnabled));
        Device = (WsSqlDeviceModel)info.GetValue(nameof(Device), typeof(WsSqlDeviceModel));
        Setting = (WsSqlDeviceSettingsModel)info.GetValue(nameof(Setting), typeof(WsSqlDeviceSettingsModel));
    }

    public WsSqlDeviceSettingsFkModel(WsSqlDeviceSettingsFkModel item) : base(item)
    {
        IsEnabled = item.IsEnabled;
        Device = new(item.Device);
        Setting = new(item.Setting);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => 
        $"{GetIsMarked()} | {GetIsBool(IsEnabled, "Enabled", "Disabled")} | {Name} | {Device} | {Setting}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlDeviceSettingsFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        IsEnabled.Equals(false) &&
        Device.EqualsDefault() &&
        Setting.EqualsDefault();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IsEnabled), IsEnabled);
        info.AddValue(nameof(Device), Device);
        info.AddValue(nameof(Setting), Setting);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Device.FillProperties();
        Setting.FillProperties();
    }

    public virtual void UpdateProperties(WsSqlDeviceSettingsFkModel item)
    {
        base.UpdateProperties(item, true);
        IsEnabled = item.IsEnabled;
        Device = new(item.Device);
        Setting = new(item.Setting);
    }

    public virtual void UpdateProperties(WsSqlDeviceSettingsFkModel item, WsSqlDeviceModel device, WsSqlDeviceSettingsModel setting)
    {
        base.UpdateProperties(item, true);
        IsEnabled = item.IsEnabled;
        Device = new(device);
        Setting = new(setting);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceSettingsFkModel item) => 
        ReferenceEquals(this, item) || base.Equals(item) &&
        IsEnabled.Equals(item.IsEnabled) &&
        Device.Equals(item.Device) &&
        Setting.Equals(item.Setting);

    #endregion
}