using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Enums;

namespace Ws.StorageCore.Entities.SchemaDiag.Logs;

[DebuggerDisplay("{ToString()}")]
public class SqlLogEntity : SqlEntityBase
{
    public virtual SqlHostEntity? Device { get; set; }
    public virtual LogTypeEnum Type { get; set; }
    public virtual string Version { get; set; }
    public virtual string File { get; set; }
    public virtual int Line { get; set; }
    public virtual string Member { get; set; }
    public virtual string Message { get; set; }

    public SqlLogEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Device = null;
        Type = LogTypeEnum.Info;
        Line = 0;
        Version = string.Empty;
        File = string.Empty;
        Member = string.Empty;
        Message = string.Empty;
    }

    public SqlLogEntity(SqlLogEntity item) : base(item)
    {
        Device = item.Device is null ? null : new(item.Device);
        Type = item.Type;
        Version = item.Version;
        File = item.File;
        Line = item.Line;
        Member = item.Member;
        Message = item.Message;
    }
    
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlLogEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override void FillProperties()
    {
        base.FillProperties();
        Line = 1;
        Type = LogTypeEnum.Info;
        
        Device?.FillProperties();
    }

    public virtual bool Equals(SqlLogEntity item) =>
        ReferenceEquals(this, item) ||
        base.Equals(item) &&
        Equals(Version, item.Version) &&
        Equals(File, item.File) &&
        Equals(Line, item.Line) &&
        Equals(Member, item.Member) &&
        Equals(Message, item.Message) &&
        (Device is null && item.Device is null ||
         Device is not null && item.Device is not null && Device.Equals(item.Device)) &&
        Type.Equals(item.Type);
}