using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Claims;

namespace DeviceControl.Source.Pages.Admin.Roles;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class RolesPage : SectionDataGridPageBase<Claim>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IClaimService ClaimService { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<RolesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(Claim item)
        => await OpenSectionModal<RolesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(Claim item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionRoles}/{item.Uid.ToString()}");

    protected override IEnumerable<Claim> SetSqlSectionCast() => ClaimService.GetAll();

    protected override IEnumerable<Claim> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ClaimService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(Claim item)
    {
        ClaimService.Delete(item);
        return Task.CompletedTask;
    }
}