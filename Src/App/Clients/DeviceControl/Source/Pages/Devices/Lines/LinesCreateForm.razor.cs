using System.Security.Claims;
using DeviceControl.Source.Shared.Auth.Policies;
using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.Printer;
using Ws.Domain.Services.Features.User;
using Ws.Domain.Services.Features.Warehouse;
using Ws.Shared.Resources;
using Ws.Shared.TypeUtils;

namespace DeviceControl.Source.Pages.Devices.Lines;

public sealed partial class LinesCreateForm : SectionFormBase<LineEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;
    [Inject] private ILineService LineService { get; set; } = default!;
    [Inject] private Redirector Redirector { get; set; } = default!;

    # endregion

    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;

    private IEnumerable<PrinterEntity> PrinterEntities { get; set; } = [];
    private List<WarehouseEntity> CachedWarehousesEntities { get; set; } = [];
    private IEnumerable<LineTypeEnum> LineTypesEntities { get; set; } = [];
    private ProductionSiteEntity ProductionSiteEntity { get; set; } = new();
    private bool IsSeniorSupport { get; set; }
    private bool IsDeveloper { get; set; }

    private IEnumerable<WarehouseEntity> WarehousesEntities =>
        CachedWarehousesEntities.Where(item => item.ProductionSite.Equals(ProductionSiteEntity));

    private IEnumerable<ProductionSiteEntity> ProductionSitesEntities =>
        CachedWarehousesEntities.Select(warehouse => warehouse.ProductionSite)
            .Where(productionSite => !productionSite.IsNew).ToHashSet();

    protected override void OnInitialized()
    {
        DialogItem.Warehouse.Name = Localizer["FormWarehouseDefaultPlaceholder"];
        DialogItem.Printer.Name = Localizer["FormPrinterDefaultPlaceholder"];
        GenerateLineNumber();

        LineTypesEntities = Enum.GetValues(typeof(LineTypeEnum)).Cast<LineTypeEnum>().ToList();
        CachedWarehousesEntities = WarehouseService.GetAll().ToList();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        IsDeveloper = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.Developer)).Succeeded;
        IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SupportSenior)).Succeeded;

        ClaimsPrincipal userClaims = (await AuthState).User;
        if (userClaims is { Identity.Name: not null })
        {
            UserEntity userEntity = UserService.GetItemByNameOrCreate(userClaims.Identity.Name);
            ProductionSiteEntity = userEntity.ProductionSite ?? ProductionSitesEntities.FirstOrDefault() ?? new();
            UpdateCurrentWarehouses();
        }
    }

    protected override LineEntity CreateItemAction(LineEntity item) =>
        LineService.Create(item);

    private void UpdateCurrentWarehouses()
    {
        PrinterEntities = PrinterService.GetAllByProductionSite(ProductionSiteEntity);
        DialogItem.Printer = PrinterEntities.FirstOrDefault() ?? new() { Name = Localizer["FormPrinterDefaultPlaceholder"] };

        DialogItem.Warehouse = WarehousesEntities.FirstOrDefault() ?? new();

        if (!IsDeveloper)
            CachedWarehousesEntities.RemoveAll(i => i.Uid.IsMax() || i.ProductionSite.Uid.IsMax());
    }

    private void GenerateLineNumber() => DialogItem.Number = new Random().Next(10001, 100000);
}

public class LinesCreateFormValidator : AbstractValidator<LineEntity>
{
    public LinesCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty().MaximumLength(64);
        RuleFor(item => item.Number).GreaterThan(10000).LessThan(100000);
        RuleFor(item => item.PcName).NotEmpty().Matches("^[A-Z0-9-]*$");
        RuleFor(item => item.Type).IsInEnum();
        RuleFor(item => item.Printer).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure("С объектом Printer что-то не так");
        });
        RuleFor(item => item.Warehouse).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure("С объектом Warehouse что-то не так");
        });
    }
}