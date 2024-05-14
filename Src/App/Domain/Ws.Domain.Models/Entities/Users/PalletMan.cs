// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Users;

[DebuggerDisplay("{ToString()}")]
public class PalletMan : EntityBase
{
    public virtual string Surname { get; set; } = string.Empty;
    public virtual string Patronymic { get; set; } = string.Empty;
    public virtual string Password { get; set; } = string.Empty;
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Fio => $"{Surname} {Name} {Patronymic}";

    public virtual string FioShort =>
        $"{Surname} {(string.IsNullOrEmpty(Name) ? "" : Name[..1].ToUpper())}." +
        $"{(string.IsNullOrEmpty(Patronymic) ? "" : Patronymic[..1].ToUpper())}.";

    protected override bool CastEquals(EntityBase obj)
    {
        PalletMan item = (PalletMan)obj;
        return Equals(Surname, item.Surname) &&
               Equals(Patronymic, item.Patronymic) &&
               Equals(Name, item.Name) &&
               Equals(Password, item.Password);
    }

    public override string ToString() => FioShort;
}