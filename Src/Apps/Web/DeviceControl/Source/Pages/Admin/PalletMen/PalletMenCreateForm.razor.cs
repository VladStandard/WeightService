// using Ws.Domain.Models.Entities.Ref;
// using Ws.Domain.Models.Entities.Users;
// using Ws.Domain.Services.Features.PalletMen;
//
// namespace DeviceControl.Source.Pages.Admin.PalletMen;
//
// public sealed partial class PalletMenCreateForm : SectionFormBase<PalletMan>
// {
//     #region Inject
//
//     [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
//     [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
//     [Inject] private IPalletManService PalletManService { get; set; } = default!;
//     [Inject] private Redirector Redirector { get; set; } = default!;
//
//     #endregion
//
//     [Parameter, EditorRequired] public ProductionSite ProductionSite { get; set; } = new();
//
//     private IEnumerable<Warehouse> Warehouses { get; set; } = [];
//
//     protected override PalletMan CreateItemAction(PalletMan item) =>
//         PalletManService.Create(item);
// }
//
// public class PalletMenCreateFormValidator : AbstractValidator<PalletMan>
// {
//     public PalletMenCreateFormValidator(IStringLocalizer<ApplicationResources> localizer, IStringLocalizer<WsDataResources> wsDataLocalizer)
//     {
//         RuleFor(item => item.Uid1C).NotEmpty().WithName("UID 1C");
//         RuleFor(item => item.Fio.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
//         RuleFor(item => item.Fio.Surname).NotEmpty().WithName(wsDataLocalizer["ColSurname"]);
//         RuleFor(item => item.Fio.Patronymic).NotEmpty().WithName(wsDataLocalizer["ColPatronymic"]);
//         RuleFor(item => item.Password).NotEmpty().WithName(wsDataLocalizer["ColPassword"]);
//         RuleFor(item => item.Warehouse).Custom((obj, context) =>
//         {
//             if (obj.IsNew)
//                 context.AddFailure(string.Format(localizer["FormFieldNotSelected"], wsDataLocalizer["ColWarehouse"]));
//         });
//     }
// }