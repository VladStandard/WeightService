using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Claim;

namespace DeviceControl.Features.Sections.Admin.Roles;

public sealed partial class RolesDataGrid : SectionDataGridBase<ClaimEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IClaimService ClaimService { get; set; } = null!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<RolesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(ClaimEntity item)
        => await OpenSectionModal<RolesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(ClaimEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionRoles}/{item.IdentityValueUid.ToString()}");

    protected override IEnumerable<ClaimEntity> SetSqlSectionCast() => ClaimService.GetAll();

    protected override IEnumerable<ClaimEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ClaimService.GetItemByUid(itemUid)];
    }
}