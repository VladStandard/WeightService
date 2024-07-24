using System.Diagnostics.CodeAnalysis;

namespace Ws.DeviceControl.Models.Dto.Database;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed record MigrationHistoryDto
{
    [JsonPropertyName("migrationId")]
    public string MigrationId { get; set; } = string.Empty;

    [JsonPropertyName("productVersion")]
    public string ProductVersion { get; set; } = string.Empty;
}