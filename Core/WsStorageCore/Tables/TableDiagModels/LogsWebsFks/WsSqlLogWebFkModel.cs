// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsSqlTableBase = WsStorageCore.Common.WsSqlTableBase;

namespace WsStorageCore.Tables.TableDiagModels.LogsWebsFks;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlLogWebFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructornomenclatureCharacteristicsFk

    private WsSqlLogWebModel _logWebRequest;
    [XmlElement] public virtual WsSqlLogWebModel LogWebRequest { get => _logWebRequest; set => _logWebRequest = value; }
    private WsSqlLogWebModel _logWebResponse;
    [XmlElement] public virtual WsSqlLogWebModel LogWebResponse { get => _logWebResponse; set => _logWebResponse = value; }
    private WsSqlAppModel _app;
    [XmlElement] public virtual WsSqlAppModel App { get => _app; set => _app = value; }
    private WsSqlLogTypeModel _logType;
    [XmlElement] public virtual WsSqlLogTypeModel LogType { get => _logType; set => _logType = value; }
    private WsSqlDeviceModel _device;
    [XmlElement] public virtual WsSqlDeviceModel Device { get => _device; set => _device = value; }
    
    public WsSqlLogWebFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        _logWebRequest = new();
        _logWebResponse = new();
        _app = new();
        _logType = new();
        _device = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlLogWebFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        _logWebRequest = (WsSqlLogWebModel)info.GetValue(nameof(LogWebRequest), typeof(WsSqlLogWebModel));
        _logWebResponse = (WsSqlLogWebModel)info.GetValue(nameof(LogWebResponse), typeof(WsSqlLogWebModel));
        _app = (WsSqlAppModel)info.GetValue(nameof(App), typeof(WsSqlAppModel));
        _logType = (WsSqlLogTypeModel)info.GetValue(nameof(LogType), typeof(WsSqlLogTypeModel));
        _device = (WsSqlDeviceModel)info.GetValue(nameof(WsSqlDeviceModel), typeof(WsSqlDeviceModel));
    }

    public WsSqlLogWebFkModel(WsSqlLogWebFkModel item)
    {
        LogWebRequest = new(item.LogWebRequest);
        LogWebResponse = new(item.LogWebResponse);
        App = new(item.App);
        LogType = new(item.LogType);
        Device = new(item.Device);

    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{LogWebRequest.CountAll} | {LogWebResponse.CountSuccess} | {LogWebResponse.CountErrors}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlLogWebFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        LogWebRequest.EqualsDefault() &&
        LogWebResponse.EqualsDefault() &&
        App.EqualsDefault() &&
        LogType.EqualsDefault() &&
        Device.EqualsDefault();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(LogWebRequest), LogWebRequest);
        info.AddValue(nameof(LogWebResponse), LogWebResponse);
        info.AddValue(nameof(App), App);
        info.AddValue(nameof(LogType), LogType);
        info.AddValue(nameof(Device), Device);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        LogWebRequest.FillProperties();
        LogWebResponse.FillProperties();
        App.FillProperties();
        LogType.FillProperties();
        Device.FillProperties();
    }

    public virtual void UpdateProperties(WsSqlLogWebFkModel item)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(item, true);
        
        LogWebRequest = item.LogWebRequest;
        LogWebResponse = item.LogWebResponse;
        App = new(item.App);
        LogType = new(item.LogType);
        Device = new(item.Device);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlLogWebFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        LogWebRequest.Equals(item.LogWebRequest) &&
        LogWebResponse.Equals(item.LogWebResponse) &&
        App.Equals(item.App) &&
        LogType.Equals(item.LogType) &&
        Device.Equals(item.Device);

    #endregion
}