// // ReSharper disable ClassNeverInstantiated.Global
//
// using Blazorise;
// using Microsoft.AspNetCore.Components;
// using Microsoft.Extensions.Localization;
// using ScalesDesktop.Features.Shared;
// using ScalesDesktop.Resources;
// using ScalesDesktop.Services;
// using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
//
// namespace ScalesDesktop.Features.Pallet.Dialogs;
//
// public sealed partial class PluNestingSelect : DataGridBase<SqlPluNestingFkEntity>
// {
//     [Inject] private IModalService ModalService { get; set; } = null!;
//     [Inject] private LineContext LineContext { get; set; } = null!;
//     [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
//
//     protected override void GetGridData() => GridData = LineContext.PluNestingEntities;
//
//     protected override async Task OnItemSelect(SqlPluNestingFkEntity obj)
//     {
//         LineContext.ChangePluNesting(obj);
//         await ModalService.Hide();
//     }
// }
