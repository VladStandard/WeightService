using DeviceControl.Source.Shared.Api;
using Ws.Domain.Models.Entities.Ref;

namespace DeviceControl.Source.Pages.Admin.Users;

public class UserWithProductionSite
{
    public KeycloakUser KeycloakUser { get; set; } = default!;
    public ProductionSite? ProductionSite { get; set; }
}