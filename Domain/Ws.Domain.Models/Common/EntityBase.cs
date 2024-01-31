// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Models;

namespace Ws.Domain.Models.Common;

[DebuggerDisplay("{ToString()}")]
public class EntityBase
{
    public virtual SqlFieldIdentityModel Identity { get; set; }
    public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
    public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }
    public virtual DateTime CreateDt { get; set; } = DateTime.MinValue;
    public virtual DateTime ChangeDt { get; set; } = DateTime.MinValue;
    public virtual string Name { get; set; } = string.Empty;
    public virtual bool IsExists => Identity.IsExists;
    public virtual bool IsNew => Identity.IsNew;
    public virtual string DisplayName => IsNew ? string.Empty : Name;

    public EntityBase()
    {
        Identity = new(SqlEnumFieldIdentity.Uid);
    }

    public EntityBase(SqlEnumFieldIdentity identityName) : this()
    {
        Identity = new(identityName);
    }

    public override string ToString() => Name;

    public virtual bool Equals(EntityBase item) =>
        ReferenceEquals(this, item) || Identity.Equals(item.Identity) &&
        Equals(CreateDt, item.CreateDt) &&
        Equals(ChangeDt, item.ChangeDt) &&
        Equals(Name, item.Name);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((EntityBase)obj);
    }

    public override int GetHashCode() => Identity.GetHashCode();
}