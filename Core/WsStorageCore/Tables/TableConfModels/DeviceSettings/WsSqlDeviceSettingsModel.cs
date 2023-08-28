namespace WsStorageCore.Tables.TableConfModels.DeviceSettings;

/// <summary>
/// Таблица "DEVICES_SETTINGS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceSettingsModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public override string DisplayName => IsNew ? WsLocaleCore.Table.FieldEmpty : GetLocalizationName(Name);

    public WsSqlDeviceSettingsModel() : base(WsSqlEnumFieldIdentity.Uid) { }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlDeviceSettingsModel(SerializationInfo info, StreamingContext context) : base(info, context) { }

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

    public virtual void UpdateProperties(WsSqlDeviceSettingsModel item)
    {
        base.UpdateProperties(item, false);
    }

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