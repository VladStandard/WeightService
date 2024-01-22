// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class PluLineEntity : EntityBase
{
    public virtual PluEntity Plu { get; set; }
    public virtual LineEntity Line { get; set; }
    
    public PluLineEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Line = new();
    }
    
    public PluLineEntity(PluLineEntity item) : base(item)
    {
        Plu = new(item.Plu);
        Line = new(item.Line);
    }

    public override string ToString() => $"{Plu} | {Line}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluLineEntity)obj);
    }
    
    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(PluLineEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Plu, item.Plu) &&
        Equals(Line, item.Line) &&
        Plu.Equals(item.Plu) &&
        Line.Equals(item.Line);
}
