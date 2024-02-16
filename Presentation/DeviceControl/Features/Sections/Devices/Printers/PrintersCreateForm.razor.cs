using System.Net;
using System.Text.RegularExpressions;
using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;
using Ws.Shared.Parsers;

namespace DeviceControl.Features.Sections.Devices.Printers;

public sealed partial class PrintersCreateForm : SectionFormBase<PrinterEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private string PrinterIp
    {
        get => SectionEntity.Ip.ToString();
        set =>  SectionEntity.Ip = IpAddressParser.Parse(value, SectionEntity.Ip);
    }

    private IEnumerable<PrinterTypeEnum> PrinterTypesEntities { get; set; } = new List<PrinterTypeEnum>();

    protected override void OnInitialized()
    {
        PrinterTypesEntities = Enum.GetValues(typeof(PrinterTypeEnum)).Cast<PrinterTypeEnum>().ToList();
    }
}