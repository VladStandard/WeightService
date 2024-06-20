using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.ProductionSites;
using Ws.Domain.Services.Features.Users;

namespace DeviceControl.Source.Pages.Admin.Users;

public sealed partial class UsersUpdateForm : SectionFormBase<UserWithProductionSite>
{
    #region Inject
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;

    #endregion

    private IEnumerable<ProductionSite> ProductionSite { get; set; } = [];

    protected override void OnInitialized()
    {
        ProductionSite = ProductionSiteService.GetAll();
        base.OnInitialized();
    }

    protected override UserWithProductionSite UpdateItemAction(UserWithProductionSite item)
    {
        User user = new()
        {
            Uid = item.KeycloakUser.Id,
            ProductionSite = item.ProductionSite!
        };
        try
        {
            UserService.Update(user);
        }
        catch
        {
            UserService.Create(user);
        }

        return item;
    }
}

public class UsersUpdateFormValidator : AbstractValidator<UserWithProductionSite>
{
    public UsersUpdateFormValidator()
    {
        RuleFor(item => item.ProductionSite).NotEmpty();
    }
}