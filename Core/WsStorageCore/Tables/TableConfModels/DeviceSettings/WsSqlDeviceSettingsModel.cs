namespace WsStorageCore.Tables.TableConfModels.DeviceSettings;

/// <summary>
/// Таблица "DEVICES_SETTINGS".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceSettingsModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public override string DisplayName => IsNew ? WsLocaleCore.Table.FieldEmpty : GetLocalizationName(Name);

    public WsSqlDeviceSettingsModel() : base(WsSqlEnumFieldIdentity.Uid) { }

    public WsSqlDeviceSettingsModel(WsSqlDeviceSettingsModel item) : base(item) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlDeviceSettingsModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceSettingsModel item) => ReferenceEquals(this, item) || base.Equals(item);

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