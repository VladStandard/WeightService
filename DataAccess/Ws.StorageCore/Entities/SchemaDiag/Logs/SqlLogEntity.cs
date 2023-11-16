using Ws.StorageCore.Common;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaScale.Apps;
using Ws.StorageCore.Enums;

namespace Ws.StorageCore.Entities.SchemaDiag.Logs;

[DebuggerDisplay("{ToString()}")]
public class SqlLogEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual SqlHostEntity? Device { get; set; }
    public virtual SqlAppEntity? App { get; set; }
    public virtual LogTypeEnum Type { get; set; }
    public virtual string Version { get; set; }
    public virtual string File { get; set; }
    public virtual int Line { get; set; }
    public virtual string Member { get; set; }
    public virtual string Message { get; set; }

    public SqlLogEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Device = null;
        App = null;
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
        App = item.App is null ? null : new(item.App);
        Type = item.Type;
        Version = item.Version;
        File = item.File;
        Line = item.Line;
        Member = item.Member;
        Message = item.Message;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | {Device?.Name ?? "null"} | {App?.Name ?? "null"} | {Type.ToString()} | {Version} | {File} " +
        $"{Line} | {Member} | {Message}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlLogEntity)obj);
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
        (Type is LogTypeEnum.Info);

    public override void FillProperties()
    {
        base.FillProperties();

        Version = LocaleCore.Sql.SqlItemFieldVersion;
        File = LocaleCore.Sql.SqlItemFieldFile;
        Line = 1;
        Member = LocaleCore.Sql.SqlItemFieldMember;
        Message = LocaleCore.Sql.SqlItemFieldMessage;
        Type = LogTypeEnum.Info;
        
        Device?.FillProperties();
        App?.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

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
        (App is null && item.App is null || App is not null && item.App is not null && App.Equals(item.App)) &&
        Type.Equals(item.Type);

    #endregion
}