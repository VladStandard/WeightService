// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;

namespace Ws.Domain.Models.Common;

[DebuggerDisplay("{ToString()}")]
public abstract class EntityBase
{
    public virtual Guid Uid { get; protected set; } = Guid.Empty;
    public virtual DateTime CreateDt { get; set; } = DateTime.MinValue;
    public virtual DateTime ChangeDt { get; set; } = DateTime.MinValue;
    public virtual bool IsExists => !IsNew;
    public virtual bool IsNew => Uid == Guid.Empty;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        EntityBase entity = (EntityBase)obj;

        return Equals(Uid, entity.Uid) &&
               Equals(CreateDt, entity.CreateDt) &&
               Equals(ChangeDt, entity.ChangeDt) &&
               CastEquals(entity);
    }

    protected virtual bool CastEquals(EntityBase obj) => true;

    public override int GetHashCode() => Uid.GetHashCode();
}