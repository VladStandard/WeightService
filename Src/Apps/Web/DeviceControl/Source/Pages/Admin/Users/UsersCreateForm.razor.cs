using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Claims;
using Ws.Domain.Services.Features.ProductionSites;
using Ws.Domain.Services.Features.Users;

namespace DeviceControl.Source.Pages.Admin.Users;

public sealed partial class UsersCreateForm : SectionFormBase<User>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IClaimService ClaimService { get; set; } = default!;

    #endregion

    private HashSet<Claim> RolesEntities { get; set; } = [];
    private IEnumerable<ProductionSite> ProductionSites { get; set; } = [];

    protected override void OnInitialized()
    {
        DialogItem.Name = "KOLBASA-VS\\";
        ProductionSites = ProductionSiteService.GetAll();
        RolesEntities = [.. ClaimService.GetAll()];
        base.OnInitialized();
    }

    protected override User CreateItemAction(User item) =>
        UserService.Create(item);
}

public class UsersCreateFormValidator : AbstractValidator<User>
{
    public UsersCreateFormValidator()
    {
        RuleFor(item => item.Name).Matches(@"^KOLBASA-VS\\.+");
    }
}