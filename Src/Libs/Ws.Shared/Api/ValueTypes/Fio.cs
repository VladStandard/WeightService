using System.Text.Json.Serialization;
using Ws.Shared.Extensions;

namespace Ws.Shared.Api.ValueTypes;

public sealed record Fio
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("surname")]
    public required string Surname { get; init; }

    [JsonPropertyName("patronymic")]
    public required string Patronymic { get; init; }

    [JsonIgnore]
    public string DisplayShortName => $"{Surname} {GetInitialChar(Name)}{GetInitialChar(Patronymic)}";

    [JsonIgnore]
    public string DisplayFullName => $"{Surname} {Name} {Patronymic}".Capitalize();

    #region Private

    private static string GetInitialChar(string s) => string.IsNullOrWhiteSpace(s) ? "" : $"{char.ToUpper(s[0])}.";

    #endregion
}