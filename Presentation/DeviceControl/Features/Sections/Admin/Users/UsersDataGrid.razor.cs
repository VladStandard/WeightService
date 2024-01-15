using DeviceControl.Features.Sections.Devices.Lines;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.Users;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersDataGrid: SectionDataGridBase<SqlUserEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private SqlUserRepository UserRepository { get; set; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<UsersCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlUserEntity item)
        => await OpenSectionModal<UsersUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlUserEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionUsers}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = UserRepository.GetEnumerable(SqlCrudConfigSection).ToList();

    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<SqlUserEntity>(itemUid)];
    }
}