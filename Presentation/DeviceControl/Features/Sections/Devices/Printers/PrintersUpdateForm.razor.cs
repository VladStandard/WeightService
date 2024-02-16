using System.Net;
using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;

namespace DeviceControl.Features.Sections.Devices.Printers;

public sealed partial class PrintersUpdateForm : SectionFormBase<PrinterEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<PrinterTypeEnum> PrinterTypesEntities { get; set; } = new List<PrinterTypeEnum>();
    
    private string PrinterIp
    {
        get => SectionEntity.Ip.ToString();
        set
        {
            IPAddress.TryParse(value, out IPAddress? ip);
            SectionEntity.Ip = ip ?? SectionEntity.Ip;
        }
    }

    protected override void OnInitialized()
    {
        PrinterTypesEntities = Enum.GetValues(typeof(PrinterTypeEnum)).Cast<PrinterTypeEnum>().ToList();
    }
}