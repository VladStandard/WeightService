namespace Ws.DeviceControl.Models.Features.Admins.Users.Queries;

public sealed record UserDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }
}