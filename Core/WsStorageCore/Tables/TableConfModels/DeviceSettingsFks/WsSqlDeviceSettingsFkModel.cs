namespace WsStorageCore.Tables.TableConfModels.DeviceSettingsFks;

/// <summary>
/// Таблица "DEVICES_SETTINGS_FK".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceSettingsFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    
    public virtual bool IsEnabled { get; set; }
    public virtual WsSqlDeviceModel Device { get; set; }
    public virtual WsSqlDeviceSettingsModel Setting { get; set; }

    public WsSqlDeviceSettingsFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Device = new();
        Setting = new();
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
        $"{GetIsMarked()} | {Device} | {Setting} | {GetIsBool(IsEnabled, "Enabled", "Disabled")}";

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
    
    public override void FillProperties()
    {
        base.FillProperties();
        Device.FillProperties();
        Setting.FillProperties();
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