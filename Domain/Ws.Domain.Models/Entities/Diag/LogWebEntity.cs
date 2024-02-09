// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Diag;

[DebuggerDisplay("{ToString()}")]
public class LogWebEntity() : EntityBase(SqlEnumFieldIdentity.Uid)
{
    public virtual DateTime StampDt { get; set; } = DateTime.MinValue;
    public virtual string Version { get; set; } = string.Empty;
    public virtual string Url { get; set; } = string.Empty;
    public virtual string DataRequest { get; set; } = string.Empty;
    public virtual string DataResponse { get; set; } = string.Empty;
    public virtual int CountAll { get; set; }
    public virtual int CountSuccess { get; set; }
    public virtual int CountErrors { get; set; }

    public override string ToString() => $"{Version} | {Url}";

    protected override bool CastEquals(EntityBase obj)
    {
        LogWebEntity item = (LogWebEntity)obj;
        return Equals(StampDt, item.StampDt) &&
               Equals(Version, item.Version) &&
               Equals(Url, item.Url) &&
               Equals(DataResponse, item.DataResponse) &&
               Equals(DataRequest, item.DataRequest) &&
               Equals(CountAll, item.CountAll) &&
               Equals(CountSuccess, item.CountSuccess) &&
               Equals(CountErrors, item.CountErrors);
    }
}