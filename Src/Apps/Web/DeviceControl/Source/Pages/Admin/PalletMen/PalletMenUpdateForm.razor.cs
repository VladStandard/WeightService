// using DeviceControl.Source.Shared.Services;
// using Fluxor;
// using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;
// using Ws.Domain.Models.Entities.Ref;
// using Ws.Domain.Models.Entities.Users;
//
// namespace DeviceControl.Source.Pages.Admin.PalletMen;
//
// public sealed partial class PalletMenUpdateForm : SectionFormBase<PalletManDto>
// {
//     #region Inject
//     [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
//     [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
//     [Inject] private IState<ProductionSiteState> ProductionSiteState { get; set; } = default!;
//     [Inject] private Redirector Redirector { get; set; } = default!;
//     [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
//
//     #endregion
//
//     [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;
//
//     private bool IsOnlyView { get; set; }
//     private bool IsSeniorSupport { get; set; }
//
//     protected override async Task OnInitializedAsync()
//     {
//         await base.OnInitializedAsync();
//         IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SeniorSupport)).Succeeded;
//         IsOnlyView = !IsSeniorSupport && !UserProductionSite.Equals(ProductionSiteState.Value.ProductionSite);
//     }
//
//     protected override PalletManDto UpdateItemAction(PalletManDto item) =>
//         throw new NotImplementedException();
//
//     protected override Task DeleteItemAction(PalletManDto item) =>
//         throw new NotImplementedException();
// }
//
// public class PalletMenUpdateFormValidator : AbstractValidator<PalletMan>
// {
//     public PalletMenUpdateFormValidator(IStringLocalizer<ApplicationResources> localizer, IStringLocalizer<WsDataResources> wsDataLocalizer)
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