using System.Net;
using DeviceControl.Source.Shared.Auth.Policies;
using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
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

public sealed partial class PrintersUpdateForm : SectionFormBase<PrinterEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;

    # endregion

    private List<ProductionSiteEntity> ProductionSites { get; set; } = [];
    private IEnumerable<PrinterTypeEnum> PrinterTypesEntities { get; set; } = new List<PrinterTypeEnum>();
    private UserEntity User { get; set; } = new();
    private bool IsOnlyView { get; set; }
    private bool IsSeniorSupport { get; set; }
    private bool IsDeveloper { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        ProductionSites = ProductionSiteService.GetAll().ToList();
        PrinterTypesEntities = Enum.GetValues(typeof(PrinterTypeEnum)).Cast<PrinterTypeEnum>().ToList();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (UserPrincipal is { Identity.Name: not null })
            User = UserService.GetItemByNameOrCreate(UserPrincipal.Identity.Name);

        IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SupportSenior)).Succeeded;
        IsDeveloper = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.Developer)).Succeeded;
        IsOnlyView = !IsSeniorSupport && !(User.ProductionSite != null && User.ProductionSite.Equals(DialogItem.ProductionSite));

        if (!IsDeveloper)
            ProductionSites.RemoveAll(i => i.Uid.IsMax());
    }

    protected override PrinterEntity UpdateItemAction(PrinterEntity item) =>
        PrinterService.Update(item);

    protected override Task DeleteItemAction(PrinterEntity item)
    {
        PrinterService.Delete(item);
        return Task.CompletedTask;
    }
}

public class PrintersUpdateFormValidator : AbstractValidator<PrinterEntity>
{
    public PrintersUpdateFormValidator()
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