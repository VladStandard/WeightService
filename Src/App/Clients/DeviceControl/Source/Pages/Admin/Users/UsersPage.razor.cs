using DeviceControl.Source.Shared.Auth.ClaimsTransform.CacheProviders.Common;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.User;

namespace DeviceControl.Source.Pages.Admin.Users;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class UsersPage : SectionDataGridPageBase<User>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IClaimsCacheProvider ClaimsCacheProvider { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<UsersCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(User item)
        => await OpenSectionModal<UsersUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(User item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionUsers}/{item.Uid.ToString()}");

    protected override IEnumerable<User> SetSqlSectionCast() =>
        UserService.GetAll();

    protected override IEnumerable<User> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [UserService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(User item)
    {
        ClaimsCacheProvider.ClearCacheByUserName(item.Name);
        UserService.Delete(item);
        return Task.CompletedTask;
    }
}