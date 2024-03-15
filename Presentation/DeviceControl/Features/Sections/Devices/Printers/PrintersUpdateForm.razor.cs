using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;
using Ws.Domain.Services.Features.Printer;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Shared.Parsers;

namespace DeviceControl.Features.Sections.Devices.Printers;

public sealed partial class PrintersUpdateForm : SectionFormBase<PrinterEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IPrinterService PrinterService { get; set; } = null!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = null!;
    private IEnumerable<ProductionSiteEntity> ProductionSites { get; set; } = [];
    private IEnumerable<PrinterTypeEnum> PrinterTypesEntities { get; set; } = new List<PrinterTypeEnum>();
    
    private string PrinterIp
    {
        get => SectionEntity.Ip.ToString();
        set => SectionEntity.Ip = IpAddressParser.Parse(value, SectionEntity.Ip);
    }
    
    protected override void OnInitialized()
    {
        PrinterTypesEntities = Enum.GetValues(typeof(PrinterTypeEnum)).Cast<PrinterTypeEnum>().ToList();
        if (SectionEntity.ProductionSite != null)
        {
            SectionEntity.ProductionSite.Name = Localizer["SectionFormProductionSiteDefaultName"];
        }
        ProductionSites = ProductionSiteService.GetAll();
    }
}