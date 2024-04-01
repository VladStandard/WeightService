using DeviceControl2.Source.Shared.Auth.ClaimsTransform.CacheProviders.Common;
using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Claim;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.User;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.Admin.Users;

public sealed partial class UsersUpdateForm: SectionFormBase<UserEntity>
{
    #region Inject
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IClaimService ClaimService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IClaimsCacheProvider ClaimsCacheProvider { get; set; } = default!;

    #endregion

    private HashSet<ClaimEntity> RolesEntities { get; set; } = [];
    private IEnumerable<ProductionSiteEntity> ProductionSite { get; set; } = [];

    protected override void OnInitialized()
    {
        RolesEntities = [..ClaimService.GetAll()];
        ProductionSite = ProductionSiteService.GetAll();
        base.OnInitialized();
    }

    protected override UserEntity UpdateItemAction(UserEntity item) =>
        UserService.Update(item);

    protected override Task DeleteItemAction(UserEntity item)
    {
        ClaimsCacheProvider.ClearCacheByUserName(item.Name);
        UserService.Delete(item);
        return Task.CompletedTask;
    }
}

public class UsersUpdateFormValidator : AbstractValidator<UserEntity>
{
    public UsersUpdateFormValidator()
    {
        RuleFor(item => item.Name).Matches(@"^KOLBASA-VS\\.+");
    }
}
