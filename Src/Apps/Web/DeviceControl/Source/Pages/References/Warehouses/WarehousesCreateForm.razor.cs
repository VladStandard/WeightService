// using Ws.Domain.Models.Entities.Ref;
//
// namespace DeviceControl.Source.Pages.References.Warehouses;
//
// public sealed partial class WarehousesCreateForm : SectionFormBase<Warehouse>
// {
//     #region Inject
//
//     [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
//     [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
//
//     #endregion
//
//     [Parameter, EditorRequired] public ProductionSite ProductionSite { get; set; } = new();
//
//     protected override void OnInitialized()
//     {
//         base.OnInitialized();
//         DialogItem.ProductionSite = ProductionSite;
//     }
//
//     protected override Warehouse CreateItemAction(Warehouse item) =>
//         throw new NotImplementedException();
// }
//
// public class WarehousesCreateFormValidator : AbstractValidator<Warehouse>
// {
//     public WarehousesCreateFormValidator(IStringLocalizer<ApplicationResources> localizer, IStringLocalizer<WsDataResources> wsDataLocalizer)
//     {
//         RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
//         RuleFor(item => item.Uid1C).NotEmpty().WithName("UID 1C");
//         RuleFor(item => item.ProductionSite).Custom((obj, context) =>
//         {
//             if (obj.IsNew)
//                 context.AddFailure(string.Format(localizer["FormFieldNotSelected"], wsDataLocalizer["ColProductionSite"]));
//         });
//     }
// }