// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableDiagModels.Logs;

/// <summary>
/// Table "LOGS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlLogModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement(IsNullable = true)] public virtual WsSqlDeviceModel? Device { get; set; }
    [XmlElement(IsNullable = true)] public virtual WsSqlAppModel? App { get; set; }
    [XmlElement(IsNullable = true)] public virtual WsSqlLogTypeModel? LogType { get; set; }
    [XmlElement] public virtual string Version { get; set; }
    [XmlElement] public virtual string File { get; set; }
    [XmlElement] public virtual int Line { get; set; }
    [XmlElement] public virtual string Member { get; set; }
    [XmlElement] public virtual string Message { get; set; }

    public WsSqlLogModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Device = null;
        App = null;
        LogType = null;
        Version = string.Empty;
        File = string.Empty;
        Line = 0;
        Member = string.Empty;
        Message = string.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlLogModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Device = (WsSqlDeviceModel?)info.GetValue(nameof(Device), typeof(WsSqlDeviceModel));
        App = (WsSqlAppModel?)info.GetValue(nameof(App), typeof(WsSqlAppModel));
        LogType = (WsSqlLogTypeModel?)info.GetValue(nameof(LogType), typeof(WsSqlLogTypeModel));
        Version = info.GetString(nameof(Version));
        File = info.GetString(nameof(File));
        Line = info.GetInt32(nameof(Line));
        Member = info.GetString(nameof(Member));
        Message = info.GetString(nameof(Message));
    }

    public WsSqlLogModel(WsSqlLogModel item) : base(item)
    {
        Device = item.Device is null ? null : new(item.Device);
        App = item.App is null ? null : new(item.App);
        LogType = item.LogType is null ? null : new(item.LogType);
        Version = item.Version;
        File = item.File;
        Line = item.Line;
        Member = item.Member;
        Message = item.Message;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | {Device?.Name ?? "null"} | {App?.Name ?? "null"} | {LogType?.Icon ?? "null"} | {Version} | {File} " +
        $"{Line} | {Member} | {Message}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlLogModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Version, string.Empty) &&
        Equals(File, string.Empty) &&
        Equals(Line, 0) &&
        Equals(Member, string.Empty) &&
        Equals(Message, string.Empty) &&
        (Device is null || Device.EqualsDefault()) &&
        (App is null || App.EqualsDefault()) &&
        (LogType is null || LogType.EqualsDefault());

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Version), Version);
        info.AddValue(nameof(File), File);
        info.AddValue(nameof(Line), Line);
        info.AddValue(nameof(Member), Member);
        info.AddValue(nameof(Message), Message);
        info.AddValue(nameof(Device), Device);
        info.AddValue(nameof(App), App);
        info.AddValue(nameof(LogType), LogType);
    }

    public override void ClearNullProperties()
    {
        if (App is not null && App.Identity.EqualsDefault())
            App = null;
        if (LogType is not null && LogType.Identity.EqualsDefault())
            LogType = null;
    }

    public override void FillProperties()
    {
        base.FillProperties();

        Version = WsLocaleCore.Sql.SqlItemFieldVersion;
        File = WsLocaleCore.Sql.SqlItemFieldFile;
        Line = 1;
        Member = WsLocaleCore.Sql.SqlItemFieldMember;
        //LogType = new();
        Message = WsLocaleCore.Sql.SqlItemFieldMessage;

        Device?.FillProperties();
        App?.FillProperties();
        LogType?.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlLogModel item) =>
        ReferenceEquals(this, item) ||
        base.Equals(item) &&
        Equals(Version, item.Version) &&
        Equals(File, item.File) &&
        Equals(Line, item.Line) &&
        Equals(Member, item.Member) &&
        Equals(Message, item.Message) &&
        (Device is null && item.Device is null || Device is not null && item.Device is not null && Device.Equals(item.Device)) &&
        (App is null && item.App is null || App is not null && item.App is not null && App.Equals(item.App)) &&
        (LogType is null && item.LogType is null || LogType is not null && item.LogType is not null && LogType.Equals(item.LogType));

    #endregion
}