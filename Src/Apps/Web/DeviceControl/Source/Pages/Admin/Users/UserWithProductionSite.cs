using DeviceControl.Source.Shared.Api.Keycloak.Models;

namespace DeviceControl.Source.Pages.Admin.Users;

public record UserWithProductionSite(KeycloakUser User, Guid ProductionSiteId);