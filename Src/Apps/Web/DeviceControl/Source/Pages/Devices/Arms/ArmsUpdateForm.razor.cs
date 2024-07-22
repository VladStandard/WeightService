using DeviceControl.Source.Shared.Services;
using Fluxor;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Queries;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;

namespace DeviceControl.Source.Pages.Devices.Arms;

public sealed partial class ArmsUpdateForm : SectionFormBase<ArmDto>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IState<ProductionSiteState> ProductionSiteState { get; set; } = default!;
    [Inject] private DevicesEndpoints DevicesEndpoints { get; set; } = default!;
    [Inject] private ReferencesEndpoints ReferencesEndpoints { get; set; } = default!;

    # endregion

    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;

    private IEnumerable<Warehouse> Warehouses { get; set; } = [];
    private IEnumerable<Printer> Printers { get; set; } = [];
    private IEnumerable<ArmType> LineTypes { get; set; } = Enum.GetValues(typeof(ArmType)).Cast<ArmType>().ToList();
    private bool IsOnlyView { get; set; }
    private bool IsSeniorSupport { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SeniorSupport)).Succeeded;
        IsOnlyView = !IsSeniorSupport && !UserProductionSite.Equals(ProductionSiteState.Value.ProductionSite);
    }

    protected override ArmDto UpdateItemAction(ArmDto item) =>
        throw new NotImplementedException();

    protected override Task DeleteItemAction(ArmDto item) =>
        throw new NotImplementedException();
}

public class LinesUpdateFormValidator : AbstractValidator<ArmDto>
{
    public LinesUpdateFormValidator(IStringLocalizer<ApplicationResources> localizer, IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().MaximumLength(64).WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Number).GreaterThan(10000).LessThan(100000).WithName(wsDataLocalizer["ColNumber"]);
        RuleFor(item => item.PcName).NotEmpty().Matches("^[A-Z0-9-]*$").WithName(wsDataLocalizer["ColPcName"]);
        RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
        RuleFor(item => item.Counter).GreaterThanOrEqualTo((ushort)0).WithName(wsDataLocalizer["ColCounter"]);
        // RuleFor(item => item.Printer).Custom((obj, context) =>
        // {
        //     if (obj.IsNew)
        //         context.AddFailure(string.Format(localizer["FormFieldNotSelected"], wsDataLocalizer["ColPrinter"]));
        // });
        // RuleFor(item => item.Warehouse).Custom((obj, context) =>
        // {
        //     if (obj.IsNew)
        //         context.AddFailure(string.Format(localizer["FormFieldNotSelected"], wsDataLocalizer["ColWarehouse"]));
        // });
    }
}