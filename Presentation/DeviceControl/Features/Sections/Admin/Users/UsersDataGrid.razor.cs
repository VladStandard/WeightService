using DeviceControl.Auth.ClaimsTransform.CacheProviders.Common;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.User;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersDataGrid : SectionDataGridBase<UserEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IUserService UserService { get; set; } = null!;
    [Inject] private IClaimsCacheProvider ClaimsCacheProvider { get; set; } = null!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<UsersCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(UserEntity item)
        => await OpenSectionModal<UsersUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(UserEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionUsers}/{item.Uid.ToString()}");

    protected override IEnumerable<UserEntity> SetSqlSectionCast()
    {
        return UserService.GetAll();
    }

    protected override IEnumerable<UserEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [UserService.GetItemByUid(itemUid)];
    }

    private Task DeleteUserWithRelogin(UserEntity item)
    {
        UserService.Delete(item);
        ClaimsCacheProvider.ClearCacheByUserName(item.Name);
        return Task.CompletedTask;
    }
}