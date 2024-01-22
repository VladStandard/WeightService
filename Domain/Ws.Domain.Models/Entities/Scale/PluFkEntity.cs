// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class PluFkEntity : EntityBase
{
    public virtual PluEntity Plu { get; set; }
    public virtual PluEntity Parent { get; set; }
    public virtual PluEntity? Category { get; set; }
    
    public PluFkEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Parent = new();
        Category = null;
    }

    public override string ToString() =>
        $"{nameof(Plu)}: {Plu}. " +
        $"{nameof(Parent)}: {Parent}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluFkEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(PluFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Plu.Equals(item.Plu) &&
        Parent.Equals(item.Parent) &&
        (Category is null && item.Category is null || Category is not null && item.Category is not null && Category.Equals(item.Category));
}