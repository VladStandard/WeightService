using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Claim;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.Admin.Roles;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class RolesPage : SectionDataGridPageBase<ClaimEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IClaimService ClaimService { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<RolesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(ClaimEntity item)
        => await OpenSectionModal<RolesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(ClaimEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionRoles}/{item.Uid.ToString()}");

    protected override IEnumerable<ClaimEntity> SetSqlSectionCast() => ClaimService.GetAll();

    protected override IEnumerable<ClaimEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ClaimService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(ClaimEntity item) {
        ClaimService.Delete(item);
        return Task.CompletedTask;
    }
}
