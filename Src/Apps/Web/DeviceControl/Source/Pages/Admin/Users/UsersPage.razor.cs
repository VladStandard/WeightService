using Blazorise.Extensions;
using DeviceControl.Source.Shared.Api;
using DeviceControl.Source.Shared.Services;
using Ws.Domain.Models.Entities.Ref;
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
    [Inject] private UsersEndpoints UsersEndpoints { get; set; } = default!;

    #endregion

    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;

    private IEnumerable<User> UsersRelations { get; set; } = [];
    private ProductionSite ProductionSite { get; set; } = new();

    protected override void OnInitialized()
    {
        ProductionSite = UserProductionSite;
        base.OnInitialized();
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