namespace Ws.Shared.ValueTypes;

public record Fio
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Surname { get; set; } = string.Empty;
    public virtual string Patronymic { get; set; } = string.Empty;

    #region Display

    public virtual string DisplayFull => $"{Surname} {Name} {Patronymic}";
    public virtual string DisplayShort =>
        $"{Surname} {(string.IsNullOrWhiteSpace(Name) ? "" : Name[..1].ToUpper())}." +
        $"{(string.IsNullOrWhiteSpace(Patronymic) ? "" : Patronymic[..1].ToUpper())}.";

    #endregion
}