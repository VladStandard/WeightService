// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Interfaces;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.LogsWebs;

namespace DataCore.Sql.TableScaleFkModels.LogsWebsFks;

/// <summary>
/// Table "LOGS_WEBS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(LogWebFkModel)} | {ToString()}")]
public class LogWebFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructornomenclatureCharacteristicsFk

    private LogWebModel _logWeb;
    [XmlElement] public virtual LogWebModel LogWeb { get => _logWeb; set => _logWeb = value; }
    private AppModel _app;
    [XmlElement] public virtual AppModel App { get => _app; set => _app = value; }
    private LogTypeModel _logType;
    [XmlElement] public virtual LogTypeModel LogType { get => _logType; set => _logType = value; }
    private DeviceModel _device;
    [XmlElement] public virtual DeviceModel Device { get => _device; set => _device = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public LogWebFkModel() : base(SqlFieldIdentity.Uid)
    {
        _logWeb = new();
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
        _logWeb = (LogWebModel)info.GetValue(nameof(LogWeb), typeof(LogWebModel));
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
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(LogWeb)}: {LogWeb}. " +
        $"{nameof(App)}: {App}. " +
        $"{nameof(LogType)}: {LogType}. " +
        $"{nameof(Device)}: {Device}";

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
        LogWeb.EqualsDefault() &&
        App.EqualsDefault() &&
        LogType.EqualsDefault() &&
        Device.EqualsDefault();

    public override object Clone()
    {
        LogWebFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.LogWeb = LogWeb.CloneCast();
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
        info.AddValue(nameof(LogWeb), LogWeb);
        info.AddValue(nameof(App), App);
        info.AddValue(nameof(LogType), LogType);
        info.AddValue(nameof(Device), Device);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        LogWeb.FillProperties();
        App.FillProperties();
        LogType.FillProperties();
        Device.FillProperties();
    }

    public override void UpdateProperties(ISqlTable item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_nomenclatures/.
        if (item is not LogWebFkModel logWebFk) return;
        LogWeb = logWebFk.LogWeb;
        App = logWebFk.App;
        LogType = logWebFk.LogType;
        Device = logWebFk.Device;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(LogWebFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        LogWeb.Equals(item.LogWeb) &&
        App.Equals(item.App) &&
        LogType.Equals(item.LogType) &&
        Device.Equals(item.Device);

    public new virtual LogWebFkModel CloneCast() => (LogWebFkModel)Clone();

    #endregion
}