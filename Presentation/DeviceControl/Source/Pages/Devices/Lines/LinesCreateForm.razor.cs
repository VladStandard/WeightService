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
    private IEnumerable<WarehouseEntity> WarehousesEntities { get; set; } = [];
    private IEnumerable<WarehouseEntity> CachedWarehousesEntities { get; set; } = [];
    private IEnumerable<ProductionSiteEntity> ProductionSitesEntities { get; set; } = [];
    private IEnumerable<LineTypeEnum> LineTypesEntities { get; set; } = [];
    private ProductionSiteEntity ProductionSiteEntity { get; set; } = new();

    protected override void OnInitialized()
    {
        DialogItem.Warehouse.Name = Localizer["FormWarehouseDefaultPlaceholder"];
        DialogItem.Printer.Name = Localizer["FormPrinterDefaultPlaceholder"];
        DialogItem.Number = 10001;

        PrinterEntities = PrinterService.GetAll();
        LineTypesEntities = Enum.GetValues(typeof(LineTypeEnum)).Cast<LineTypeEnum>().ToList();
        CachedWarehousesEntities = WarehouseService.GetAll().ToList();
        ProductionSitesEntities = new HashSet<ProductionSiteEntity>(CachedWarehousesEntities.Select(warehouse => warehouse.ProductionSite)
            .Where(productionSite => !productionSite.IsNew));
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
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
        WarehousesEntities = CachedWarehousesEntities.Where(item => item.ProductionSite.Equals(ProductionSiteEntity));
        DialogItem.Warehouse = WarehousesEntities.FirstOrDefault() ?? new();
    }

    private bool IsAdmin() => AuthorizationService.AuthorizeAsync(User, PolicyEnum.Admin).GetAwaiter().GetResult().Succeeded;
}

public class LinesCreateFormValidator : AbstractValidator<LineEntity>
{
    public LinesCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Number).GreaterThanOrEqualTo(10001);
        RuleFor(item => item.PcName).NotEmpty();
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