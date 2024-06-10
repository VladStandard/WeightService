// using FluentValidation;
// using Microsoft.AspNetCore.Components;
// using ScalesDesktop.Source.Shared.Services;
// using Ws.Domain.Models.Entities.Ref1c.Plu;
// using Ws.Domain.Services.Features.Arms;
// using Ws.Domain.Services.Features.Plus;
//
// namespace ScalesDesktop.Source.Features.PalletCreate;
//
// public sealed partial class PalletFirstStageForm : ComponentBase
// {
//     # region Injects
//     [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
//     [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
//     [Inject] private IPluService PluService { get; set; } = default!;
//     [Inject] private IArmService ArmService { get; set; } = default!;
//     [Inject] private LineContext LineContext { get; set; } = default!;
//
//     # endregion
//
//     [Parameter, EditorRequired] public PalletCreateModel FormModel { get; set; } = default!;
//     [Parameter] public EventCallback OnValidSubmit { get; set; }
//     [Parameter] public EventCallback OnCancelAction { get; set; }
//
//     private IEnumerable<Plu> PluEntities { get; set; } = [];
//
//
//     protected override void OnInitialized() => PluEntities = [];
//     // protected override void OnInitialized() => PluEntities = ArmService.GetArmPiecePlus(LineContext.Line);
//
//     private void SetPluNestings()
//     {
//         if (FormModel.Plu == null) return;
//         FormModel.Nesting = FormModel.Plu.CharacteristicsWithNesting.FirstOrDefault() ?? new();
//     }
// }
//
// public class PalletPluStageFormValidator : AbstractValidator<PalletCreateModel>
// {
//     public PalletPluStageFormValidator()
//     {
//         RuleFor(item => item.Plu).Custom((obj, context) =>
//         {
//             if (obj == null || obj.IsNew)
//                 context.AddFailure("С объектом Plu что-то не так");
//         });
//         RuleFor(item => item.Nesting).Custom((obj, context) =>
//         {
//             if (obj == null || obj.IsNew)
//                 context.AddFailure("С объектом Nesting что-то не так");
//         });
//     }
// }