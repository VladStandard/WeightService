using DeviceControl.Source.Shared.Api;
using Ws.DeviceControl.Models.Dto.Shared;

namespace DeviceControl.Source.Pages.Admin.Users;

public record UserWithProductionSite(KeycloakUser User, Guid ProductionSiteId);