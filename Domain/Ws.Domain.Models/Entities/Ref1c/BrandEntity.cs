// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class BrandEntity : Table1CBase
{
    public virtual string Code { get; set; }

    public BrandEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Code = string.Empty;
    }

    public BrandEntity(BrandEntity item) : base(item)
    {
        Code = item.Code;
    }

    public override string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Code)}: {Code}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BrandEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(BrandEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Code, item.Code);
}