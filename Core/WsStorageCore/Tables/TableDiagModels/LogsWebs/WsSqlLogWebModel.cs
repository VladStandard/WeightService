namespace WsStorageCore.Tables.TableDiagModels.LogsWebs;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlLogWebModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual DateTime StampDt { get; set; } = DateTime.MinValue;
    [XmlElement] public virtual string Version { get; set; }
    [XmlElement] public virtual string Url { get; set; }
    [XmlElement] public virtual string DataRequest { get; set; }
    [XmlElement] public virtual string DataResponse { get; set; }
    [XmlElement] public virtual int CountAll { get; set; }
    [XmlElement] public virtual int CountSuccess { get; set; }
    [XmlElement] public virtual int CountErrors { get; set; }
    
    public WsSqlLogWebModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Version = string.Empty;
        Url = string.Empty;
        DataRequest = string.Empty;
        DataResponse = string.Empty;
        CountAll = default;
        CountSuccess = default;
        CountErrors = default;
    }
    
    protected WsSqlLogWebModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Version = info.GetString(nameof(Version));
        Url = info.GetString(nameof(Url));
        DataRequest = info.GetString(nameof(DataRequest));
        CountAll = info.GetInt32(nameof(CountAll));
        CountSuccess = info.GetInt32(nameof(CountSuccess));
        CountAll = info.GetInt32(nameof(CountErrors));
    }

    public WsSqlLogWebModel(WsSqlLogWebModel item) : base(item)
    {
        StampDt = item.StampDt;
        Version = item.Version;
        Url = item.Url;
        DataResponse = item.DataResponse;
        DataRequest = item.DataRequest;
        CountAll = item.CountAll;
        CountSuccess = item.CountSuccess;
        CountErrors = item.CountErrors;

    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {Version} | {Url}";

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
        Equals(DataResponse, string.Empty) &&
        Equals(Url, string.Empty) &&
        Equals(DataRequest, string.Empty) &&
        Equals(CountAll, default(int)) &&
        Equals(CountSuccess, default(int)) &&
        Equals(CountErrors, default(int));

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
        info.AddValue(nameof(DataResponse), DataResponse);
        info.AddValue(nameof(Url), Url);
        info.AddValue(nameof(DataRequest), DataRequest);
        info.AddValue(nameof(CountAll), CountAll);
        info.AddValue(nameof(CountSuccess), CountSuccess);
        info.AddValue(nameof(CountErrors), CountErrors);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Version = WsLocaleCore.Sql.SqlItemFieldVersion;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlLogWebModel item) =>
        ReferenceEquals(this, item) ||
        base.Equals(item) &&
        Equals(StampDt, item.StampDt) &&
        Equals(Version, item.Version) &&
        Equals(Url, item.Url) &&
        Equals(DataResponse, item.DataResponse) &&
        Equals(DataRequest, item.DataRequest) &&
        Equals(CountAll, item.CountAll) &&
        Equals(CountSuccess, item.CountSuccess) &&
        Equals(CountErrors, item.CountErrors);

    #endregion
}