using System.Security.Claims;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Services;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Printer;
using Ws.Domain.Services.Features.User;

namespace DeviceControl.Features.Sections.Devices.Printers;

public sealed partial class PrintersDataGrid : SectionDataGridBase<PrinterEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IPrinterService PrinterService { get; set; } = null!;
    [Inject] private IUserService UserDomainService { get; set; } = null!;
    [Inject] private UserService UserService { get; set; } = null!;

    #endregion
    
    private UserEntity User { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        ClaimsPrincipal? userClaims = await UserService.GetUser();
        if (userClaims is { Identity.Name: not null })
            User = UserDomainService.GetItemByNameOrCreate(userClaims.Identity.Name);
    }
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PrintersCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(PrinterEntity item)
        => await OpenSectionModal<PrintersUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(PrinterEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPrinters}/{item.Uid.ToString()}");

    protected override IEnumerable<PrinterEntity> SetSqlSectionCast()
    {
        return User.ProductionSite == null ? [] : PrinterService.GetAllByProductionSite(User.ProductionSite);
    }
    
    protected override IEnumerable<PrinterEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [PrinterService.GetItemByUid(itemUid)];
    }
}