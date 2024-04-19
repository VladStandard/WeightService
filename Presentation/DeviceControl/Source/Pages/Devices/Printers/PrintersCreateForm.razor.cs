using System.Net;
using DeviceControl.Source.Shared.Auth.Policies;
using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;
using Ws.Domain.Services.Features.Printer;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.User;
using Ws.Shared.Resources;
using Ws.Shared.TypeUtils;

namespace DeviceControl.Source.Pages.Devices.Printers;

public sealed partial class PrintersCreateForm : SectionFormBase<PrinterEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;

    # endregion

    private List<ProductionSiteEntity> ProductionSites { get; set; } = [];
    private IEnumerable<PrinterTypeEnum> PrinterTypesEntities { get; set; } = new List<PrinterTypeEnum>();
    private UserEntity User { get; set; } = new();
    private bool IsSeniorSupport { get; set; }
    private bool IsDeveloper { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        PrinterTypesEntities = Enum.GetValues(typeof(PrinterTypeEnum)).Cast<PrinterTypeEnum>().ToList();
        DialogItem.ProductionSite.Name = Localizer["FormProductionSiteDefaultPlaceholder"];
        ProductionSites = ProductionSiteService.GetAll().ToList();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (UserPrincipal is { Identity.Name: not null })
            User = UserService.GetItemByNameOrCreate(UserPrincipal.Identity.Name);
        DialogItem.ProductionSite = User.ProductionSite ?? ProductionSites.FirstOrDefault() ?? new();
        IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SupportSenior)).Succeeded;
        IsDeveloper = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.Developer)).Succeeded;

        if (!IsDeveloper)
            ProductionSites.RemoveAll(i => i.Uid.IsMax());
    }

    protected override PrinterEntity CreateItemAction(PrinterEntity item) =>
        PrinterService.Create(item);
}

public class PrintersCreateFormValidator : AbstractValidator<PrinterEntity>
{
    public PrintersCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty().Matches("^[A-Z0-9-]*$");
        RuleFor(item => item.Ip).NotEmpty().NotEqual(IPAddress.Parse("127.0.0.1"));
        RuleFor(item => item.Type).IsInEnum();
        RuleFor(item => item.ProductionSite).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure("С объектом Production Site что-то не так");
        });
    }
}