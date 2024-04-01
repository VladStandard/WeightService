using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Claim;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.User;
using Ws.Domain.Services.Features.Warehouse;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.Admin.Users;

public sealed partial class UsersCreateForm: SectionFormBase<UserEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IClaimService ClaimService { get; set; } = default!;

    #endregion

    private HashSet<ClaimEntity> RolesEntities { get; set; } = [];
    private IEnumerable<ProductionSiteEntity> ProductionSites { get; set; } = [];

    protected override void OnInitialized()
    {
        DialogItem.Name = "KOLBASA-VS\\";
        ProductionSites = ProductionSiteService.GetAll();
        RolesEntities = [..ClaimService.GetAll()];
        base.OnInitialized();
    }

    protected override UserEntity CreateItemAction(UserEntity item) =>
        UserService.Create(item);
}

public class UsersCreateFormValidator : AbstractValidator<UserEntity>
{
    public UsersCreateFormValidator()
    {
        RuleFor(item => item.Name).Matches(@"^KOLBASA-VS\\.+");
    }
}
