using System.Text.Json.Serialization;

namespace Ws.DeviceControl.Models.Dto.Admins.Users.Queries;

public record UserDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}