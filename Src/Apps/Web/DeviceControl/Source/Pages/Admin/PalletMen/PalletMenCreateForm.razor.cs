using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.PalletMen;
using Ws.Domain.Services.Features.Warehouses;

namespace DeviceControl.Source.Pages.Admin.PalletMen;

public sealed partial class PalletMenCreateForm : SectionFormBase<PalletMan>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPalletManService PalletManService { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;
    [Inject] private Redirector Redirector { get; set; } = default!;

    #endregion

    [Parameter, EditorRequired] public ProductionSite ProductionSite { get; set; } = new();

    private IEnumerable<Warehouse> Warehouses { get; set; } = [];

    protected override PalletMan CreateItemAction(PalletMan item) =>
        PalletManService.Create(item);

    protected override void OnInitialized()
    {
        Warehouses = WarehouseService.GetAllByProductionSite(ProductionSite);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        DialogItem.Warehouse = Warehouses.FirstOrDefault() ?? new();
    }
}

public class PalletMenCreateFormValidator : AbstractValidator<PalletMan>
{
    public PalletMenCreateFormValidator()
    {
        RuleFor(item => item.Fio.Name).NotEmpty();
        RuleFor(item => item.Fio.Surname).NotEmpty();
        RuleFor(item => item.Fio.Patronymic).NotEmpty();
        RuleFor(item => item.Password).NotEmpty();
        RuleFor(item => item.Warehouse).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure("С объектом Warehouse что-то не так");
        });
    }
}