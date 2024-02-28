using DeviceControl.Auth.Common;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Database.Core.Utils;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.User;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersDataGrid : SectionDataGridBase<UserEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IUserCacheService UserCacheService { get; set; } = null!;
    [Inject] private IUserService UserService { get; set; } = null!;

    #endregion

    private IEnumerable<string> UserNames { get; set; } = [];

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<UsersCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(UserEntity item)
        => await OpenSectionModal<UsersUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(UserEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionUsers}/{item.Uid.ToString()}");

    protected override IEnumerable<UserEntity> SetSqlSectionCast()
    {
        UserNames = UserCacheService.GetCachedUsernames();
        return UserService.GetAll();
    }

    protected override IEnumerable<UserEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [UserService.GetItemByUid(itemUid)];
    }

    private Task DeleteUserWithRelogin(UserEntity item)
    {
        UserCacheService.ClearCacheForUser(item.Name);
        SqlCoreHelper.Delete(item);
        return Task.CompletedTask;
    }

    private bool IsUserActive(string userName) =>
        UserNames.Any(i => i.Equals(userName, StringComparison.OrdinalIgnoreCase));
}