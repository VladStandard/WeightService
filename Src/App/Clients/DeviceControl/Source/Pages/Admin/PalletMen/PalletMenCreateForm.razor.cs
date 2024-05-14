using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.PalletMan;

namespace DeviceControl.Source.Pages.Admin.PalletMen;

public sealed partial class PalletMenCreateForm : SectionFormBase<PalletMan>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPalletManService PalletManService { get; set; } = default!;

    #endregion

    protected override PalletMan CreateItemAction(PalletMan item) =>
        PalletManService.Create(item);
}

public class PalletMenCreateFormValidator : AbstractValidator<PalletMan>
{
    public PalletMenCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Surname).NotEmpty();
        RuleFor(item => item.Patronymic).NotEmpty();
        RuleFor(item => item.Password).NotEmpty();
    }
}