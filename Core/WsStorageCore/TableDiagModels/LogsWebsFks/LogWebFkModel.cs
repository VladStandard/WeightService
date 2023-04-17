// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Enums;
using WsStorageCore.TableDiagModels.LogsTypes;
using WsStorageCore.TableDiagModels.LogsWebs;
using WsStorageCore.TableScaleModels.Apps;
using WsStorageCore.TableScaleModels.Devices;
using WsSqlTableBase = WsStorageCore.Tables.WsSqlTableBase;

namespace WsStorageCore.TableDiagModels.LogsWebsFks;

/// <summary>
/// Table "LOGS_WEBS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(LogWebFkModel)} | {ToString()}")]
public class LogWebFkModel : Tables.WsSqlTableBase
{
    #region Public and private fields, properties, constructornomenclatureCharacteristicsFk

    private LogWebModel _logWebRequest;
    [XmlElement] public virtual LogWebModel LogWebRequest { get => _logWebRequest; set => _logWebRequest = value; }
    private LogWebModel _logWebResponse;
    [XmlElement] public virtual LogWebModel LogWebResponse { get => _logWebResponse; set => _logWebResponse = value; }
    private AppModel _app;
    [XmlElement] public virtual AppModel App { get => _app; set => _app = value; }
    private LogTypeModel _logType;
    [XmlElement] public virtual LogTypeModel LogType { get => _logType; set => _logType = value; }
    private DeviceModel _device;
    [XmlElement] public virtual DeviceModel Device { get => _device; set => _device = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public LogWebFkModel() : base(WsSqlFieldIdentity.Uid)
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
    protected LogWebFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        _logWebRequest = (LogWebModel)info.GetValue(nameof(LogWebRequest), typeof(LogWebModel));
        _logWebResponse = (LogWebModel)info.GetValue(nameof(LogWebResponse), typeof(LogWebModel));
        _app = (AppModel)info.GetValue(nameof(App), typeof(AppModel));
        _logType = (LogTypeModel)info.GetValue(nameof(LogType), typeof(LogTypeModel));
        _device = (DeviceModel)info.GetValue(nameof(DeviceModel), typeof(DeviceModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{LogWebRequest.CountAll} | " +
        $"{LogWebResponse.CountSuccess} | " +
        $"{LogWebResponse.CountErrors}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LogWebFkModel)obj);
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

    public override object Clone()
    {
        LogWebFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.LogWebRequest = LogWebRequest.CloneCast();
        item.LogWebResponse = LogWebResponse.CloneCast();
        item.App = App.CloneCast();
        item.LogType = LogType.CloneCast();
        item.Device = Device.CloneCast();
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

    public override void UpdateProperties(WsSqlTableBase item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_nomenclatures/.
        if (item is not LogWebFkModel logWebFk) return;
        LogWebRequest = logWebFk.LogWebRequest;
        LogWebResponse = logWebFk.LogWebResponse;
        App = logWebFk.App;
        LogType = logWebFk.LogType;
        Device = logWebFk.Device;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(LogWebFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        LogWebRequest.Equals(item.LogWebRequest) &&
        LogWebResponse.Equals(item.LogWebResponse) &&
        App.Equals(item.App) &&
        LogType.Equals(item.LogType) &&
        Device.Equals(item.Device);

    public new virtual LogWebFkModel CloneCast() => (LogWebFkModel)Clone();

    #endregion
}