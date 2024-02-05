// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Diag;

[DebuggerDisplay("{ToString()}")]
public class TableSizeEntity : EntityBase
{
    public virtual string Schema { get; init; }
    public virtual string Table { get; init; }
    public virtual long RowsCount { get; init; }
    public virtual int UsedSpaceMb { get; init; }
    public virtual string FileName { get; init; }
    
    public TableSizeEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Schema = string.Empty;
        Table = string.Empty;
        RowsCount = 0;
        UsedSpaceMb = 0;
        FileName = string.Empty;
    }

    public override string ToString() => $"{Schema} | {Table} | {RowsCount} | {UsedSpaceMb} | {FileName}";
}