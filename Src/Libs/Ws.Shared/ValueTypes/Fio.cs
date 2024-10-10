using Ws.Shared.Extensions;

namespace Ws.Shared.ValueTypes;

public sealed record Fio(string Surname, string Name, string Patronymic)
{
    public string DisplayFullName => $"{Surname} {Name} {Patronymic}".Trim().Capitalize();
    public string DisplayShortName => $"{Surname.Capitalize()} {GetInitialChar(Name)}.{GetInitialChar(Patronymic)}";

    #region Private

    private static string GetInitialChar(string s) =>
        string.IsNullOrWhiteSpace(s) ? "" : $"{char.ToUpper(s[0])}";

    #endregion
}