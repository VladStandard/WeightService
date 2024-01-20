using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Claims;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.Admin.Roles;

public sealed partial class RolesDataGrid: SectionDataGridBase<ClaimEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private SqlClaimRepository ClaimRepository { get; set; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<RolesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(ClaimEntity item)
        => await OpenSectionModal<RolesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(ClaimEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionRoles}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = ClaimRepository.GetEnumerable().ToList();

    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<ClaimEntity>(itemUid)];
    }
}