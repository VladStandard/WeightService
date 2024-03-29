using System.ComponentModel.DataAnnotations;
using System.Net;
using DeviceControl2.Source.Pages.References1C.Plus;
using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using FluentValidation;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;
using Ws.Domain.Services.Features.Plu;
using Ws.Domain.Services.Features.Printer;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.Template;
using Ws.Shared.Parsers;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.Devices.Printers;

public sealed partial class PrintersUpdateForm: SectionFormBase<PrinterEntity>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private Redirector Redirector { get; set; } = default!;

    private IEnumerable<ProductionSiteEntity> ProductionSites { get; set; } = [];
    private IEnumerable<PrinterTypeEnum> PrinterTypesEntities { get; set; } = new List<PrinterTypeEnum>();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        PrinterTypesEntities = Enum.GetValues(typeof(PrinterTypeEnum)).Cast<PrinterTypeEnum>().ToList();
        ProductionSites = ProductionSiteService.GetAll();
    }

    protected override PrinterEntity UpdateItemAction() =>
        PrinterService.Update(DialogItem);


    protected override Task DeleteItemAction()
    {
        PrinterService.Delete(DialogItem);
        return Task.CompletedTask;
    }
}

public class PrintersUpdateFormValidator : AbstractValidator<PrinterEntity>
{
    public PrintersUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Ip).NotEmpty();
        RuleFor(item => item.Type).IsInEnum();
        RuleFor(item => item.ProductionSite).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure("С объектом Production Site что-то не так");
        });
    }
}