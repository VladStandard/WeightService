using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.ValueTypes;

public sealed record Fio
{
    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [Required]
    [JsonPropertyName("surname")]
    public required string Surname { get; init; }

    [Required]
    [JsonPropertyName("patronymic")]
    public required string Patronymic { get; init; }

    [JsonIgnore]
    public string DisplayShortName =>
        $"{Surname} {(string.IsNullOrEmpty(Name) ? "" : Name[..1].ToUpper())}." +
        $"{(string.IsNullOrEmpty(Patronymic) ? "" : Patronymic[..1].ToUpper())}.";
}