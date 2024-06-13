using System.Diagnostics.CodeAnalysis;

namespace Ws.Domain.Models.ValueTypes;

[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
public class Fio : IEquatable<Fio>
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Surname { get; set; } = string.Empty;
    public virtual string Patronymic { get; set; } = string.Empty;

    #region Display

    public virtual string DisplayFull => $"{Surname} {Name} {Patronymic}";
    public virtual string DisplayShort =>
        $"{Surname} {(string.IsNullOrEmpty(Name) ? "" : Name[..1].ToUpper())}." +
        $"{(string.IsNullOrEmpty(Patronymic) ? "" : Patronymic[..1].ToUpper())}.";

    #endregion

    # region System

    public override string ToString() => DisplayFull;
    public bool Equals(Fio? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Surname == other.Surname && Patronymic == other.Patronymic;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Fio)obj);
    }

    public override int GetHashCode() => HashCode.Combine(Name, Surname, Patronymic);

    # endregion
}