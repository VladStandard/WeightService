// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Diag;

[DebuggerDisplay("{ToString()}")]
public class TableSizeEntity() : EntityBase(SqlEnumFieldIdentity.Uid)
{
    public virtual string Schema { get; init; } = string.Empty;
    public virtual string Table { get; init; } = string.Empty;
    public virtual string FileName { get; init; } = string.Empty;
    public virtual long RowsCount { get; init; }
    public virtual int UsedSpaceMb { get; init; }

    public override string ToString() => $"{Schema} | {Table} | {RowsCount} | {UsedSpaceMb} | {FileName}";
}