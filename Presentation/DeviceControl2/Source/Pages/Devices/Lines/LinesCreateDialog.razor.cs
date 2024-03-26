// using DeviceControl2.Source.Shared.Localization;
// using DeviceControl2.Source.Widgets.Section;
// using Microsoft.AspNetCore.Components;
// using Microsoft.Extensions.Localization;
// using Ws.Domain.Models.Entities.Ref;
// using Ws.Shared.Enums;
//
// namespace DeviceControl2.Source.Pages.Devices.Lines;
//
// // ReSharper disable ClassNeverInstantiated.Global
// public sealed partial class LinesCreateDialog : SectionDialogBase<PrinterEntity>
// {
//     [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
//
//     protected override List<EnumTypeModel<string>> InitializeTabList() =>
//     [
//         new(Localizer["SectionPrinters"], "main"),
//     ];
// }