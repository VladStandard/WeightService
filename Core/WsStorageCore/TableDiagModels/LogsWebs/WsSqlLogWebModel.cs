// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableDiagModels.LogsWebs;

/// <summary>
/// Table "LOGS_WEBS".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(LogWebModel)} | {Identity}")]
public class WsSqlLogWebModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual DateTime StampDt { get; set; } = DateTime.MinValue;
    [XmlElement] public virtual string Version { get; set; }
    [XmlElement] public virtual byte Direction { get; set; }
    [XmlElement] public virtual string Url { get; set; }
    [XmlElement] public virtual string Params { get; set; }
    [XmlElement] public virtual string Headers { get; set; }
    [XmlElement] public virtual byte DataType { get; set; }
    [XmlElement] public virtual string DataString { get; set; }
    [XmlElement] public virtual int CountAll { get; set; }
    [XmlElement] public virtual int CountSuccess { get; set; }
    [XmlElement] public virtual int CountErrors { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlLogWebModel() : base(WsSqlFieldIdentity.Uid)
    {
        Version = string.Empty;
        Direction = default;
        Url = string.Empty;
        Params = string.Empty;
        Headers = string.Empty;
        DataType = default;
        DataString = string.Empty;
        CountAll = default;
        CountSuccess = default;
        CountErrors = default;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlLogWebModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Version = info.GetString(nameof(Version));
        Direction = info.GetByte(nameof(Direction));
        Url = info.GetString(nameof(Url));
        Params = info.GetString(nameof(Params));
        Headers = info.GetString(nameof(Headers));
        DataType = info.GetByte(nameof(DataType));
        DataString = info.GetString(nameof(DataString));
        CountAll = info.GetInt32(nameof(CountAll));
        CountSuccess = info.GetInt32(nameof(CountSuccess));
        CountAll = info.GetInt32(nameof(CountErrors));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Version)}: {Version}. " +
        $"{nameof(Direction)}: {Direction}. " +
        $"{nameof(Url)}: {Url}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlLogWebModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(StampDt, DateTime.MinValue) &&
        Equals(Version, string.Empty) &&
        Equals(Direction, default(byte)) &&
        Equals(Url, string.Empty) &&
        Equals(Params, string.Empty) &&
        Equals(Headers, string.Empty) &&
        Equals(DataType, default(byte)) &&
        Equals(DataString, string.Empty) &&
        Equals(CountAll, default(int)) &&
        Equals(CountSuccess, default(int)) &&
        Equals(CountErrors, default(int));

    public override object Clone()
    {
        WsSqlLogWebModel item = new();
        item.CloneSetup(base.CloneCast());
        item.StampDt = StampDt;
        item.Version = Version;
        item.Direction = Direction;
        item.Url = Url;
        item.Params = Params;
        item.Headers = Headers;
        item.DataType = DataType;
        item.DataString = DataString;
        item.CountAll = CountAll;
        item.CountSuccess = CountSuccess;
        item.CountErrors = CountErrors;
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
        info.AddValue(nameof(StampDt), StampDt);
        info.AddValue(nameof(Version), Version);
        info.AddValue(nameof(Direction), Direction);
        info.AddValue(nameof(Url), Url);
        info.AddValue(nameof(Params), Params);
        info.AddValue(nameof(Headers), Headers);
        info.AddValue(nameof(DataType), DataType);
        info.AddValue(nameof(DataString), DataString);
        info.AddValue(nameof(CountAll), CountAll);
        info.AddValue(nameof(CountSuccess), CountSuccess);
        info.AddValue(nameof(CountErrors), CountErrors);
    }

    //public override void ClearNullProperties()
    //{
    //    //
    //}

    public override void FillProperties()
    {
        base.FillProperties();

        Version = LocaleCore.Sql.SqlItemFieldVersion;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlLogWebModel item) =>
        ReferenceEquals(this, item) ||
        base.Equals(item) &&
        Equals(StampDt, item.StampDt) &&
        Equals(Version, item.Version) &&
        Equals(Direction, item.Direction) &&
        Equals(Url, item.Url) &&
        Equals(Params, item.Params) &&
        Equals(Headers, item.Headers) &&
        Equals(DataType, item.DataType) &&
        Equals(DataString, item.DataString) &&
        Equals(CountAll, item.CountAll) &&
        Equals(CountSuccess, item.CountSuccess) &&
        Equals(CountErrors, item.CountErrors);

    public new virtual WsSqlLogWebModel CloneCast() => (WsSqlLogWebModel)Clone();

    #endregion
}
