using System.Diagnostics;

namespace Ws.Domain.Abstractions.Entities.Common;

[DebuggerDisplay("{ToString()}")]
public class IdentityModel(SqlEnumFieldIdentity identityName)
{
    public virtual SqlEnumFieldIdentity Name { get; } = identityName;
    public virtual Guid Uid { get; set; } = Guid.Empty;
    public virtual long Id { get; set; } 

    public override string ToString() => Name switch
    {
        SqlEnumFieldIdentity.Id => $"{Id}",
        SqlEnumFieldIdentity.Uid => $"{Uid}",
        _ => string.Empty
    };

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        IdentityModel item = (IdentityModel)obj;
        return Equals(Name, item.Name) 
               && Equals(Id, item.Id) 
               && Equals(Uid, item.Uid);
    }

    public override int GetHashCode() => Name switch
    {
        SqlEnumFieldIdentity.Id => Id.GetHashCode(),
        SqlEnumFieldIdentity.Uid => Uid.GetHashCode(),
        _ => default
    };

    public virtual bool IsNew => Name switch
    {
        SqlEnumFieldIdentity.Id => Equals(Id, default(long)),
        SqlEnumFieldIdentity.Uid => Equals(Uid, Guid.Empty),
        _ => default
    };

    public virtual bool IsExists => !IsNew;
}