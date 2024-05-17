using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.PalletMen;

namespace DeviceControl.Source.Pages.Admin.PalletMen;

public sealed partial class PalletMenUpdateForm : SectionFormBase<PalletMan>
{
    #region Inject
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPalletManService PalletManService { get; set; } = default!;

    #endregion

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
        RuleFor(item => item.Fio.Name).NotEmpty();
        RuleFor(item => item.Fio.Surname).NotEmpty();
        RuleFor(item => item.Fio.Patronymic).NotEmpty();
        RuleFor(item => item.Password).NotEmpty();
    }
}