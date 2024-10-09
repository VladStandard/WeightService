using Ws.Shared.Extensions;

namespace Ws.Shared.ValueTypes;

public sealed record Fio(string Name, string Surname, string Patronymic)
{
    public string DisplayFullName => $"{Surname} {Name} {Patronymic}".Capitalize();
    public string DisplayShortName => $"{Surname} {GetInitialChar(Name)}{GetInitialChar(Patronymic)}";

    #region Private

    private static string GetInitialChar(string s) => string.IsNullOrWhiteSpace(s) ? "" : $"{char.ToUpper(s[0])}.";

    #endregion
}