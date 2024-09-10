namespace Ws.DeviceControl.Models.Features.Admins.Users.Commands.Update;

public sealed record UserUpdateDto
{
    [JsonPropertyName("productionSiteId")]
    public Guid ProductionSiteId { get; set; }
}