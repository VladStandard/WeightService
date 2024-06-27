using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Ws.DeviceControl.Models.Models.Database;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed record MigrationHistoryEntry
{
    [JsonPropertyName("migrationId")]
    public string MigrationId { get; set; } = string.Empty;

    [JsonPropertyName("productVersion")]
    public string ProductVersion { get; set; } = string.Empty;
}