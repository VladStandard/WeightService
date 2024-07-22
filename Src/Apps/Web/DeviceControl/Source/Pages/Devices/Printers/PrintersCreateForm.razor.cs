// using System.Net;
// using TscZebra.Plugin.Abstractions.Enums;
// using Ws.Domain.Models.Entities.Devices;
// using Ws.Domain.Models.Entities.Ref;
//
// namespace DeviceControl.Source.Pages.Devices.Printers;
//
// public sealed partial class PrintersCreateForm : SectionFormBase<Printer>
// {
//     # region Injects
//
//     [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
//     [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
//     [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
//
//     # endregion
//
//     [Parameter, EditorRequired] public ProductionSite ProductionSite { get; set; } = new();
//
//     private IEnumerable<PrinterTypes> PrinterTypes { get; set; } = new List<PrinterTypes>();
//     private bool IsSeniorSupport { get; set; }
//
//     protected override void OnInitialized()
//     {
//         base.OnInitialized();
//         PrinterTypes = Enum.GetValues(typeof(PrinterTypes)).Cast<PrinterTypes>().ToList();
//         DialogItem.ProductionSite.Name = Localizer["FormProductionSiteDefaultPlaceholder"];
//     }
//
//     protected override async Task OnInitializedAsync()
//     {
//         await base.OnInitializedAsync();
//         DialogItem.ProductionSite = ProductionSite;
//         IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(UserPrincipal, PolicyEnum.SeniorSupport))
//             .Succeeded;
//     }
//
//     protected override Printer CreateItemAction(Printer item) =>
//         throw new NotImplementedException();
// }
//
// public class PrintersCreateFormValidator : AbstractValidator<Printer>
// {
//     public PrintersCreateFormValidator(IStringLocalizer<ApplicationResources> localizer, IStringLocalizer<WsDataResources> wsDataLocalizer)
//     {
//         RuleFor(item => item.Name).NotEmpty().Matches("^[A-Z0-9-]*$").WithName(wsDataLocalizer["ColName"]);
//         RuleFor(item => item.Ip).NotEmpty().NotEqual(IPAddress.Parse("127.0.0.1"));
//         RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
//         RuleFor(item => item.ProductionSite).Custom((obj, context) =>
//         {
//             if (obj.IsNew)
//                 context.AddFailure(string.Format(localizer["FormFieldNotSelected"], wsDataLocalizer["ColProductionSite"]));
//         });
//     }
// }