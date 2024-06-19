using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Claim;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.User;

namespace DeviceControl.Source.Pages.Admin.Users;

public sealed partial class UsersUpdateForm : SectionFormBase<User>
{
    #region Inject
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IClaimService ClaimService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;

    #endregion

    private HashSet<Claim> RolesEntities { get; set; } = [];
    private IEnumerable<ProductionSite> ProductionSite { get; set; } = [];

    protected override void OnInitialized()
    {
        RolesEntities = [.. ClaimService.GetAll()];
        ProductionSite = ProductionSiteService.GetAll();
        base.OnInitialized();
    }

    protected override User UpdateItemAction(User item)
    {
        UserService.Update(item);
        // ClaimsCacheProvider.ClearCacheByUserName(item.Name); TODO: fix
        return item;
    }

    protected override Task DeleteItemAction(User item)
    {
        // ClaimsCacheProvider.ClearCacheByUserName(item.Name); TODO: fix
        UserService.Delete(item);
        return Task.CompletedTask;
    }
}

public class UsersUpdateFormValidator : AbstractValidator<User>
{
    public UsersUpdateFormValidator()
    {
        RuleFor(item => item.Name).Matches(@"^KOLBASA-VS\\.+");
    }
}