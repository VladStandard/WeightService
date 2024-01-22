using System.Xml.Linq;
using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Diag;

namespace DeviceControl.Features.Sections.Diagnostics.Logs1C;

public sealed partial class Logs1CUpdateForm: SectionFormBase<LogWebEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private static string GetFormattedXml(string xmlCode)
    {
        string formattedXml;
        try
        {
            formattedXml = XDocument.Parse(xmlCode).ToString();
        }
        catch
        {
            formattedXml = xmlCode;
        }
        return formattedXml;
    }
}
