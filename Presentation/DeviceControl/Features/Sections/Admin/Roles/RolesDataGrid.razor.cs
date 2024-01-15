using DeviceControl.Features.Sections.Admin.Users;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Claims;
using Ws.StorageCore.Entities.SchemaRef.Users;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.Admin.Roles;

public sealed partial class RolesDataGrid: SectionDataGridBase<SqlClaimEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private SqlClaimRepository ClaimRepository { get; set; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<RolesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlClaimEntity item)
        => await OpenSectionModal<RolesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlClaimEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionRoles}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = ClaimRepository.GetEnumerable().ToList();

    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<SqlClaimEntity>(itemUid)];
    }
}