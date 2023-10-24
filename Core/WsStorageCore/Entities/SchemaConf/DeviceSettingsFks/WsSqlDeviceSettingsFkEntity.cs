namespace WsStorageCore.Entities.SchemaConf.DeviceSettingsFks;

/// <summary>
/// Таблица "DEVICES_SETTINGS_FK".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceSettingsFkEntity : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    
    public virtual bool IsEnabled { get; set; }
    public virtual WsSqlDeviceEntity Device { get; set; }
    public virtual WsSqlDeviceSettingsEntity Setting { get; set; }

    public WsSqlDeviceSettingsFkEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Device = new();
        Setting = new();
    }

    public WsSqlDeviceSettingsFkEntity(WsSqlDeviceSettingsFkEntity item) : base(item)
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
        return Equals((WsSqlDeviceSettingsFkEntity)obj);
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
    
    public virtual void UpdateProperties(WsSqlDeviceSettingsFkEntity item, WsSqlDeviceEntity device, WsSqlDeviceSettingsEntity setting)
    {
        if (!item.CreateDt.Equals(DateTime.MinValue))
            CreateDt = item.CreateDt;
        if (!item.ChangeDt.Equals(DateTime.MinValue))
            ChangeDt = item.ChangeDt;
        IsMarked = item.IsMarked;
        Name = item.Name;
        if (!string.IsNullOrEmpty(item.Description))
            Description = item.Description;
        IsEnabled = item.IsEnabled;
        Device = new(device);
        Setting = new(setting);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceSettingsFkEntity item) => 
        ReferenceEquals(this, item) || base.Equals(item) &&
        IsEnabled.Equals(item.IsEnabled) &&
        Device.Equals(item.Device) &&
        Setting.Equals(item.Setting);

    #endregion
}