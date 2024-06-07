using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.Common;

[Serializable]
public record OutputDto<T>(
    [property: Required]
    [property: JsonPropertyName("data")] T Data
);