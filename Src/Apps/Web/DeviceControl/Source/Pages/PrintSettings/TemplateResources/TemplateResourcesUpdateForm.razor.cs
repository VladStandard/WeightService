// using DeviceControl.Source.Shared.Api;
// using DeviceControl.Source.Shared.Services;
// using Ws.Domain.Models.Entities.Print;
// using Ws.Domain.Services.Features.ZplResources;
//
// namespace DeviceControl.Source.Pages.PrintSettings.TemplateResources;
//
// public sealed partial class TemplateResourcesUpdateForm : SectionFormBase<ZplResource>
// {
//     #region Inject
//
//     [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
//     [Inject] private IZplResourceService ZplResourceService { get; set; } = default!;
//     [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
//     [Inject] private ZplEndpoints ZplEndpoints { get; set; } = default!;
//
//     #endregion
//
//     private IList<ZplResourceType> ZplResourceTypes { get; set; } = [];
//
//     protected override void OnInitialized()
//     {
//         base.OnInitialized();
//         ZplResourceTypes = Enum.GetValues(typeof(ZplResourceType)).Cast<ZplResourceType>().ToList();
//     }
//
//     protected override ZplResource UpdateItemAction(ZplResource item) =>
//         ZplResourceService.Update(item);
//
//     protected override Task DeleteItemAction(ZplResource item)
//     {
//         ZplResourceService.Delete(DialogItemCopy.Uid, DialogItemCopy.Name);
//         return Task.CompletedTask;
//     }
// }
//
// public class TemplateResourcesUpdateFormValidator : AbstractValidator<ZplResource>
// {
//     public TemplateResourcesUpdateFormValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
//     {
//         RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
//         RuleFor(item => item.Zpl).NotEmpty().WithName(wsDataLocalizer["ColData"]);
//         RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
//     }
// }