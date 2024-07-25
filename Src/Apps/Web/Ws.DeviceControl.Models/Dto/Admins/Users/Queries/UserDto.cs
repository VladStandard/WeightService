namespace Ws.DeviceControl.Models.Dto.Admins.Users.Queries;

public sealed record UserDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }
}