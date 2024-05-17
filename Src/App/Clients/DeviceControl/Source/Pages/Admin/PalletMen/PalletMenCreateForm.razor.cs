using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.PalletMen;

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
        RuleFor(item => item.Fio.Name).NotEmpty();
        RuleFor(item => item.Fio.Surname).NotEmpty();
        RuleFor(item => item.Fio.Patronymic).NotEmpty();
        RuleFor(item => item.Password).NotEmpty();
    }
}