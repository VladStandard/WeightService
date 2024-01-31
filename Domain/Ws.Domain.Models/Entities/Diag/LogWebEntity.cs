// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Diag;

[DebuggerDisplay("{ToString()}")]
public class LogWebEntity : EntityBase
{
    public virtual DateTime StampDt { get; set; } = DateTime.MinValue;
    public virtual string Version { get; set; }
    public virtual string Url { get; set; }
    public virtual string DataRequest { get; set; }
    public virtual string DataResponse { get; set; }
    public virtual int CountAll { get; set; }
    public virtual int CountSuccess { get; set; }
    public virtual int CountErrors { get; set; }

    public LogWebEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Version = string.Empty;
        Url = string.Empty;
        DataRequest = string.Empty;
        DataResponse = string.Empty;
        CountAll = default;
        CountSuccess = default;
        CountErrors = default;
    }

    public override string ToString() => $"{Version} | {Url}";

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LogWebEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(LogWebEntity item) =>
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
}