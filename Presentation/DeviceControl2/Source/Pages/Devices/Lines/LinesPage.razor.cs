using System.Security.Claims;
using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.User;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.Devices.Lines;

public sealed partial class LinesPage : SectionDataGridPageBase<LineEntity>
{
    #region Inject

    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private ILineService LineService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;

    #endregion

    private UserEntity User { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        ClaimsPrincipal userClaims = (await AuthState).User;
        if (userClaims is { Identity.Name: not null })
            User = UserService.GetItemByNameOrCreate(userClaims.Identity.Name);
        await base.OnInitializedAsync();
    }
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<LinesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(LineEntity item)
        => await OpenSectionModal<LinesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(LineEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLines}/{item.Uid.ToString()}");

    protected override IEnumerable<LineEntity> SetSqlSectionCast()
    {
        if (User.ProductionSite == null) return [];
        return LineService.GetAllByProductionSite(User.ProductionSite)
            .OrderBy(item => item.Number).ToList();
    }

    protected override IEnumerable<LineEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [LineService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(LineEntity item) {
        LineService.Delete(item);
        return Task.CompletedTask;
    }

}