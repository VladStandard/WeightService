// using Ws.Domain.Models.Entities.Print;
//
// namespace DeviceControl.Source.Pages.Operations.Labels;
//
// public sealed partial class LabelsUpdateForm : SectionFormBase<Label>
// {
//     # region Injects
//
//     [Inject] private Redirector Redirector { get; set; } = default!;
//     [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
//
//     # endregion
//
//     private string GetPluTypeName(bool isWeight) =>
//         isWeight ? WsDataLocalizer["ColPluWeight"] : WsDataLocalizer["ColPluPiece"];
// }