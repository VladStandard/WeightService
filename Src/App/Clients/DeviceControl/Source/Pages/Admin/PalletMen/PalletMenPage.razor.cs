using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.PalletMan;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.Admin.PalletMen;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class PalletMenPage : SectionDataGridPageBase<PalletManEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPalletManService PalletManService { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PalletMenCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(PalletManEntity item)
        => await OpenSectionModal<PalletMenUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(PalletManEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPalletMen}/{item.Uid.ToString()}");

    protected override IEnumerable<PalletManEntity> SetSqlSectionCast() => PalletManService.GetAll();

    protected override IEnumerable<PalletManEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [PalletManService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(PalletManEntity item)
    {
        PalletManService.Delete(item);
        return Task.CompletedTask;
    }
}