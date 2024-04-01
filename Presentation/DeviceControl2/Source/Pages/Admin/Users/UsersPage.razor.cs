using DeviceControl2.Source.Shared.Auth.ClaimsTransform.CacheProviders.Common;
using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.User;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.Admin.Users;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class UsersPage : SectionDataGridPageBase<UserEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IClaimsCacheProvider ClaimsCacheProvider { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<UsersCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(UserEntity item)
        => await OpenSectionModal<UsersUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(UserEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionUsers}/{item.Uid.ToString()}");

    protected override IEnumerable<UserEntity> SetSqlSectionCast() =>
        UserService.GetAll();

    protected override IEnumerable<UserEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [UserService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(UserEntity item) {
        ClaimsCacheProvider.ClearCacheByUserName(item.Name);
        UserService.Delete(item);
        return Task.CompletedTask;
    }
}
