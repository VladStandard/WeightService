using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.PalletMen;

namespace DeviceControl.Source.Pages.Admin.PalletMen;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class PalletMenPage : SectionDataGridPageBase<PalletMan>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPalletManService PalletManService { get; set; } = default!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PalletMenCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(PalletMan item)
        => await OpenSectionModal<PalletMenUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(PalletMan item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPalletMen}/{item.Uid.ToString()}");

    protected override IEnumerable<PalletMan> SetSqlSectionCast() => PalletManService.GetAll();

    protected override IEnumerable<PalletMan> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [PalletManService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(PalletMan item)
    {
        PalletManService.Delete(item);
        return Task.CompletedTask;
    }
}