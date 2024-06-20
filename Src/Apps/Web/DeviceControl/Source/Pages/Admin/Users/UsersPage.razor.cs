using DeviceControl.Source.Shared.Api;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Users;

namespace DeviceControl.Source.Pages.Admin.Users;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class UsersPage : SectionDataGridPageBase<UserWithProductionSite>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private UserApi UserApi { get; set; } = default!;
    [Inject] private IKeycloakApi KeycloakApi { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

    #endregion

    private IEnumerable<User> UsersRelations { get; set; } = [];

    protected override IEnumerable<UserWithProductionSite> SetSqlSectionCast()
    {
        UsersRelations = UserService.GetAll();
        return [];
    }

    protected override IEnumerable<UserWithProductionSite> SetSqlSearchingCast() => [];

    private IEnumerable<UserWithProductionSite> GetAllUsers(IEnumerable<KeycloakUser> users)
    {
        Dictionary<Guid, User> userDictionary = UsersRelations.ToDictionary(user => user.Uid);
        return users.Select(keycloakUser => new UserWithProductionSite
        {
            KeycloakUser = keycloakUser,
            ProductionSite = userDictionary.TryGetValue(keycloakUser.Id, out User? user) ? user.ProductionSite : null
        }).ToList();
    }

    private async Task LogoutUser(Guid userId)
    {
        try
        {
            await KeycloakApi.LogoutUser(userId);
            ToastService.ShowSuccess("User logged out");
        }
        catch (Exception e)
        {
            ToastService.ShowError($"Failed to log out user {e.Message}");
        }
    }
}