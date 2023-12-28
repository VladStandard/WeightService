using System;

namespace Ws.StorageCore.Models;

[DebuggerDisplay("{ToString()}")]
public class SqlFieldIdentityModel
{
     public virtual SqlEnumFieldIdentity Name { get; }
     public virtual Guid Uid { get; private set; }
     public virtual long Id { get; private set; }

    public SqlFieldIdentityModel()
    {
        Uid = Guid.Empty;
        Id = 0;
    }

    public SqlFieldIdentityModel(SqlEnumFieldIdentity identityName) : this()
    {
        Name = identityName;
    }

    public SqlFieldIdentityModel(SqlFieldIdentityModel item)
    {
        Name = item.Name;
        Uid = item.Uid;
        Id = item.Id;
    }

    public override string ToString() =>
        Name.Equals(SqlEnumFieldIdentity.Id) ? $"{Id}" : Name.Equals(SqlEnumFieldIdentity.Uid) ? $"{Uid}" : string.Empty;
    
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlFieldIdentityModel)obj);
    }

    public override int GetHashCode() => Name switch
    {
        SqlEnumFieldIdentity.Id => Id.GetHashCode(),
        SqlEnumFieldIdentity.Uid => Uid.GetHashCode(),
        _ => default
    };
    
    public virtual bool Equals(SqlFieldIdentityModel item) =>
        ReferenceEquals(this, item) || Equals(Name, item.Name) && //-V3130
        Id.Equals(item.Id) &&
        Uid.Equals(item.Uid);

    public virtual void SetId(long value) => Id = value;

    public virtual void SetUid(Guid value) => Uid = value;

    public virtual bool IsNew => Name switch
    {
        SqlEnumFieldIdentity.Id => Equals(Id, default(long)),
        SqlEnumFieldIdentity.Uid => Equals(Uid, Guid.Empty),
        _ => default
    };

    public virtual bool IsExists => Name switch
    {
        SqlEnumFieldIdentity.Id => !Equals(Id, default(long)),
        SqlEnumFieldIdentity.Uid => !Equals(Uid, Guid.Empty),
        _ => default
    };
}