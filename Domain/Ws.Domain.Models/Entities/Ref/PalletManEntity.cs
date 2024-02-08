// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class PalletManEntity() : Entity1CBase(SqlEnumFieldIdentity.Uid)
{
    public virtual string Surname { get; set; } = string.Empty;
    public virtual string Patronymic { get; set; } = string.Empty;
    public virtual string Password { get; set; } = string.Empty;
    
    public virtual string Fio => $"{Surname} {Name} {Patronymic}";
    
    public virtual string FioShort => 
        $"{Surname} {(string.IsNullOrEmpty(Name) ? "" : Name[..1].ToUpper())}." +
        $"{(string.IsNullOrEmpty(Patronymic) ? "" : Patronymic[..1].ToUpper())}.";

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PalletManEntity)obj);
    }
    
    public virtual bool Equals(PalletManEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Surname, item.Surname) &&
        Equals(Patronymic, item.Patronymic) &&
        Equals(Password, item.Password);
    
    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => FioShort;
}