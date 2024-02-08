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

    protected override bool CastEquals(EntityBase obj)
    {
        PalletManEntity item = (PalletManEntity)obj;
        return Equals(Surname, item.Surname) && 
               Equals(Patronymic, item.Patronymic) && 
               Equals(Password, item.Password);
    }
    
    public override string ToString() => FioShort;
}