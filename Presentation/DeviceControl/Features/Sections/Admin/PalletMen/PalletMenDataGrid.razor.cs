using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.PalletMan;

namespace DeviceControl.Features.Sections.Admin.PalletMen;

public sealed partial class PalletMenDataGrid: SectionDataGridBase<PalletManEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IPalletManService PalletManService { get; set; } = null!;
    
    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PalletMenCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(PalletManEntity item)
        => await OpenSectionModal<PalletMenUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(PalletManEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPalletMen}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() => SectionItems = PalletManService.GetAll();

    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [PalletManService.GetByUid(itemUid)];
    }

    private static string GetFullName(PalletManEntity item) => item.Fio;
}