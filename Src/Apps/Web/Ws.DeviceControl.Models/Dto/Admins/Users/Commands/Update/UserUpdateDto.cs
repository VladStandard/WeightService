namespace Ws.DeviceControl.Models.Dto.Admins.Users.Commands.Update;

public sealed record UserUpdateDto
{
    [JsonPropertyName("productionSiteId")]
    public Guid ProductionSiteId { get; set; }
}
