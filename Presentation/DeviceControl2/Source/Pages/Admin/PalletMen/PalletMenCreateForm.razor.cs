using DeviceControl2.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.PalletMan;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.Admin.PalletMen;

public sealed partial class PalletMenCreateForm: SectionFormBase<PalletManEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPalletManService PalletManService { get; set; } = default!;

    #endregion

    protected override PalletManEntity CreateItemAction(PalletManEntity item) =>
        PalletManService.Create(item);
}

public class PalletMenCreateFormValidator : AbstractValidator<PalletManEntity>
{
    public PalletMenCreateFormValidator()
    {
        RuleFor(item => item.Uid1C).NotEmpty();
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Surname).NotEmpty();
        RuleFor(item => item.Patronymic).NotEmpty();
        RuleFor(item => item.Password).NotEmpty();
    }
}
