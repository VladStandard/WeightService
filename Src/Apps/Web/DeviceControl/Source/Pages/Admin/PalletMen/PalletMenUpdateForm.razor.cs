using System.Diagnostics;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.PalletMen;
using Ws.Domain.Services.Features.Users;
using Ws.Domain.Services.Features.Warehouses;

namespace DeviceControl.Source.Pages.Admin.PalletMen;

[DebuggerDisplay("{DialogItem}")]
public sealed partial class PalletMenUpdateForm : SectionFormBase<PalletMan>
{
    #region Inject
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPalletManService PalletManService { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;

    #endregion

    [Parameter, EditorRequired] public ProductionSite ProductionSite { get; set; } = new();
    private IEnumerable<Warehouse> Warehouses { get; set; } = [];
    private User User { get; set; } = new();
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

        if (UserPrincipal is { Identity.Name: not null })
            User = UserService.GetItemByNameOrCreate(UserPrincipal.Identity.Name);

        ProductionSite productionSite = User.ProductionSite ?? new();

        IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SupportSenior)).Succeeded;

        IsOnlyView = !IsSeniorSupport && !productionSite.Equals(ProductionSite);
    }

    protected override PalletMan UpdateItemAction(PalletMan item) =>
        PalletManService.Update(item);

    protected override Task DeleteItemAction(PalletMan item)
    {
        PalletManService.Delete(item);
        return Task.CompletedTask;
    }
}

public class PalletMenUpdateFormValidator : AbstractValidator<PalletMan>
{
    public PalletMenUpdateFormValidator()
    {
        RuleFor(item => item.Uid1C).NotEmpty();
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