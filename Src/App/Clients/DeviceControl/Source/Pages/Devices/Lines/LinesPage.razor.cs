using System.Security.Claims;
using DeviceControl.Source.Shared.Auth.Policies;
using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using DeviceControl.Source.Widgets.Section.Dialogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.User;
using Ws.Shared.Extensions;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.Devices.Lines;

public sealed partial class LinesPage : SectionDataGridPageBase<LineEntity>
{
    #region Inject

    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private ILineService LineService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;

    #endregion

    private UserEntity User { get; set; } = new();
    private ProductionSiteEntity ProductionSite { get; set; } = new();
    private List<ProductionSiteEntity> ProductionSiteEntities { get; set; } = [];
    private bool IsSeniorSupport { get; set; }
    private bool IsDeveloper { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ClaimsPrincipal userPrincipal = (await AuthState).User;

        if (userPrincipal is { Identity.Name: not null })
        {
            User = UserService.GetItemByNameOrCreate(userPrincipal.Identity.Name);
            ProductionSite = User.ProductionSite ?? new();
            IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.SupportSenior))
                .Succeeded;
            IsDeveloper = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.Developer)).Succeeded ||
                          ProductionSite.Uid.IsMax();
        }

        if (IsSeniorSupport)
            ProductionSiteEntities = ProductionSiteService.GetAll().ToList();

        if (!IsDeveloper)
            ProductionSiteEntities.RemoveAll(i => i.Uid.IsMax());

        await base.OnInitializedAsync();
    }

    protected override async Task OpenSectionCreateForm()
        => await DialogService.ShowDialogAsync<LinesCreateDialog>(
            new SectionDialogContentWithProductionSite<LineEntity> { Item = new(), ProductionSite = ProductionSite }
            , DialogParameters);

    protected override async Task OpenDataGridEntityModal(LineEntity item)
        => await OpenSectionModal<LinesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(LineEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLines}/{item.Uid.ToString()}");

    protected override IEnumerable<LineEntity> SetSqlSectionCast() =>
        ProductionSite.IsNew ? [] : LineService.GetAllByProductionSite(ProductionSite)
            .OrderBy(item => item.Number);

    protected override IEnumerable<LineEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [LineService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(LineEntity item)
    {
        LineService.Delete(item);
        return Task.CompletedTask;
    }

}