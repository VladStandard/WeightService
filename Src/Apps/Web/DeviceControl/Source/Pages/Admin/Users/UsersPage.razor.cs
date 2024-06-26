using System.Security.Claims;
using Blazorise.Extensions;
using DeviceControl.Source.Shared.Api;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.ProductionSites;
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
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;

    #endregion

    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;

    private IEnumerable<User> UsersRelations { get; set; } = [];
    private bool IsAdmin { get; set; }
    private ProductionSite ProductionSite { get; set; } = new();
    private List<ProductionSite> ProductionSiteEntities { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        ClaimsPrincipal userPrincipal = (await AuthState).User;
        IsAdmin = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.Admin)).Succeeded;
        ProductionSiteEntities = ProductionSiteService.GetAll().Append(new()).ToList();
        await base.OnInitializedAsync();
    }

    protected override async Task OpenDataGridEntityModal(UserWithProductionSite item)
        => await OpenSectionModal<UsersUpdateDialog>(item);

    protected override IEnumerable<UserWithProductionSite> SetSqlSectionCast()
    {
        UsersRelations = UserService.GetAll();
        return [];
    }

    private IEnumerable<UserWithProductionSite> GetAllUsers(IEnumerable<KeycloakUser> users)
    {
        Dictionary<Guid, User> userDictionary = UsersRelations.ToDictionary(user => user.Uid);
        List<UserWithProductionSite> usersWithProductionSite = users.Select(keycloakUser => new UserWithProductionSite
        {
            KeycloakUser = keycloakUser,
            ProductionSite = userDictionary.TryGetValue(keycloakUser.Id, out User? user) ? user.ProductionSite : null
        }).ToList();
        return ProductionSite.IsNew ? usersWithProductionSite.Where(x => x.ProductionSite == null) :
            usersWithProductionSite.Where(x => x.ProductionSite.IsEqual(ProductionSite));
    }
}