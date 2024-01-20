using DeviceControl.Auth.Common;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Users;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersDataGrid: SectionDataGridBase<UserEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IUserCacheService UserCacheService { get; set; } = null!;

    private SqlUserRepository UserRepository { get; set; } = new();
    private IEnumerable<string> UserNames { get; set; } = [];

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<UsersCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(UserEntity item)
        => await OpenSectionModal<UsersUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(UserEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionUsers}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast()
    {
        SectionItems = UserRepository.GetEnumerable().ToList();
        UserNames = UserCacheService.GetCachedUsernames();
    }

    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<UserEntity>(itemUid)];
    }

    private Task DeleteUserWithRelogin(UserEntity item)
    {
        UserCacheService.ClearCacheForUser(item.Name);
        SqlCoreHelper.Instance.Delete(item);
        return Task.CompletedTask;
    }

    private bool IsUserActive(string userName) => UserNames.Any(i => i == userName);
}