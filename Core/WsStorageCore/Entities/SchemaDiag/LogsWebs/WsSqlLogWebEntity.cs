namespace WsStorageCore.Entities.SchemaDiag.LogsWebs;

[DebuggerDisplay("{ToString()}")]
public class WsSqlLogWebEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual DateTime StampDt { get; set; } = DateTime.MinValue;
    public virtual string Version { get; set; }
    public virtual string Url { get; set; }
    public virtual string DataRequest { get; set; }
    public virtual string DataResponse { get; set; }
    public virtual int CountAll { get; set; }
    public virtual int CountSuccess { get; set; }
    public virtual int CountErrors { get; set; }
    
    public WsSqlLogWebEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Version = string.Empty;
        Url = string.Empty;
        DataRequest = string.Empty;
        DataResponse = string.Empty;
        CountAll = default;
        CountSuccess = default;
        CountErrors = default;
    }
    
    public WsSqlLogWebEntity(WsSqlLogWebEntity item) : base(item)
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
        return Equals((WsSqlLogWebEntity)obj);
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

    public override void FillProperties()
    {
        base.FillProperties();
        Version = WsLocaleCore.Sql.SqlItemFieldVersion;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlLogWebEntity item) =>
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