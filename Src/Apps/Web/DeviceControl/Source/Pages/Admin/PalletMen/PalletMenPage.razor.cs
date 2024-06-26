using DeviceControl.Source.Widgets.Section.Dialogs;
using Ws.Domain.Models.Entities.Ref;
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


    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;

    private ProductionSite ProductionSite { get; set; } = new();

    protected override void OnInitialized()
    {
        ProductionSite = UserProductionSite;
        base.OnInitialized();
    }

    protected override async Task OpenSectionCreateForm()
        => await DialogService.ShowDialogAsync<PalletMenCreateDialog>(
            new SectionDialogContentWithProductionSite<PalletMan> { Item = new(), ProductionSite = ProductionSite }
            , DialogParameters);

    protected override async Task OpenDataGridEntityModal(PalletMan item)
        => await DialogService.ShowDialogAsync<PalletMenUpdateDialog>(
            new SectionDialogContentWithProductionSite<PalletMan> { Item = item, ProductionSite = ProductionSite }
            , DialogParameters);

    protected override async Task OpenItemInNewTab(PalletMan item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPalletMen}/{item.Uid.ToString()}");

    protected override IEnumerable<PalletMan> SetSqlSectionCast() =>
        ProductionSite.IsNew ? [] : PalletManService.GetAllByProductionSite(ProductionSite);

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