using WsStorageCore.Entities.SchemaRef.Hosts;
namespace WsStorageCore.Entities.SchemaConf.DeviceSettingsFks;

/// <summary>
/// Таблица "DEVICES_SETTINGS_FK".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceSettingsFkEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual bool IsEnabled { get; set; }
    public virtual WsSqlHostEntity Host { get; set; }
    public virtual WsSqlDeviceSettingsEntity Setting { get; set; }

    public WsSqlDeviceSettingsFkEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Host = new();
        Setting = new();
    }

    public WsSqlDeviceSettingsFkEntity(WsSqlDeviceSettingsFkEntity item) : base(item)
    {
        IsEnabled = item.IsEnabled;
        Host = new(item.Host);
        Setting = new(item.Setting);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | {Host} | {Setting} | {GetIsBool(IsEnabled, "Enabled", "Disabled")}";

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
        Host.EqualsDefault() &&
        Setting.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        Host.FillProperties();
        Setting.FillProperties();
    }

    public virtual void UpdateProperties(WsSqlDeviceSettingsFkEntity item, WsSqlHostEntity host,
        WsSqlDeviceSettingsEntity setting)
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
        Host = new(host);
        Setting = new(setting);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceSettingsFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        IsEnabled.Equals(item.IsEnabled) &&
        Host.Equals(item.Host) &&
        Setting.Equals(item.Setting);

    #endregion
}