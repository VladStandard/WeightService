namespace WsStorageCore.Entities.SchemaConf.DeviceSettings;

/// <summary>
/// Таблица "DEVICES_SETTINGS".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceSettingsEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor

    public override string DisplayName => IsNew ? WsLocaleCore.Table.FieldEmpty : GetLocalizationName(Name);

    public WsSqlDeviceSettingsEntity() : base(WsSqlEnumFieldIdentity.Uid) { }

    public WsSqlDeviceSettingsEntity(WsSqlDeviceSettingsEntity item) : base(item) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlDeviceSettingsEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceSettingsEntity item) => ReferenceEquals(this, item) || base.Equals(item);

    protected virtual string GetLocalizationName(string name) =>
        name switch
        {
            nameof(WsLocaleCore.LabelPrint.IsShowPrintButton) => WsLocaleCore.LabelPrint.IsShowPrintButton,
            nameof(WsLocaleCore.LabelPrint.IsShowMaximizeButton) => WsLocaleCore.LabelPrint.IsShowMaximizeButton,
            nameof(WsLocaleCore.LabelPrint.IsShowMinimizeButton) => WsLocaleCore.LabelPrint.IsShowMinimizeButton,
            _ => WsLocaleCore.LabelPrint.TranslationError
        };

    #endregion
}