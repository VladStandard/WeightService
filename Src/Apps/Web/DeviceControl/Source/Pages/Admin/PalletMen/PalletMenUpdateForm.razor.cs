using System.Diagnostics;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.PalletMen;
using Ws.Domain.Services.Features.Warehouses;

namespace DeviceControl.Source.Pages.Admin.PalletMen;

[DebuggerDisplay("{DialogItem}")]
public sealed partial class PalletMenUpdateForm : SectionFormBase<PalletMan>
{
    #region Inject
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IPalletManService PalletManService { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;

    #endregion

    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;
    [Parameter, EditorRequired] public ProductionSite ProductionSite { get; set; } = new();

    private IEnumerable<Warehouse> Warehouses { get; set; } = [];
    private bool IsOnlyView { get; set; }
    private bool IsSeniorSupport { get; set; }


    protected override void OnInitialized()
    {
        base.OnInitialized();
        Warehouses = WarehouseService.GetAllByProductionSite(ProductionSite);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SeniorSupport)).Succeeded;
        IsOnlyView = !IsSeniorSupport && !UserProductionSite.Equals(ProductionSite);
    }

    protected override PalletMan UpdateItemAction(PalletMan item) =>
        PalletManService.Update(item);

    protected override Task DeleteItemAction(PalletMan item)
    {
        PalletManService.DeleteById(item.Uid);
        return Task.CompletedTask;
    }
}

public class PalletMenUpdateFormValidator : AbstractValidator<PalletMan>
{
    public PalletMenUpdateFormValidator(IStringLocalizer<ApplicationResources> localizer, IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Uid1C).NotEmpty().WithName("UID 1C");
        RuleFor(item => item.Fio.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Fio.Surname).NotEmpty().WithName(wsDataLocalizer["ColSurname"]);
        RuleFor(item => item.Fio.Patronymic).NotEmpty().WithName(wsDataLocalizer["ColPatronymic"]);
        RuleFor(item => item.Password).NotEmpty().WithName(wsDataLocalizer["ColPassword"]);
        RuleFor(item => item.Warehouse).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure(string.Format(localizer["FormFieldNotSelected"], wsDataLocalizer["ColWarehouse"]));
        });
    }
}