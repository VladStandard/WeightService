using DeviceControl.Source.Shared.Api;

namespace DeviceControl.Source.Pages.Admin.Users;

public record UserWithProductionSite(KeycloakUser User, Guid ProductionSiteId);