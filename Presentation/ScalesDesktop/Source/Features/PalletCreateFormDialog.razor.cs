using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.Pallet;
using Ws.Domain.Services.Features.Plu;
using Ws.Labels.Service.Features.PrintLabel;
using Ws.Labels.Service.Features.PrintLabel.Piece.Dto;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Source.Features
{
    public sealed partial class PalletCreateFormDialog : ComponentBase, IDialogContentComponent
    {
        [Inject] private IToastService ToastService { get; set; } = null!;
        [Inject] private LineContext LineContext { get; set; } = null!;
        [Inject] private PalletContext PalletContext { get; set; } = null!;
        [Inject] private IPluService PluService { get; set; } = null!;
        [Inject] private ILineService LineService { get; set; } = null!;
        [Inject] private IPrintLabelService PrintLabelService { get; set; } = null!;
        [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
        [Inject] private IStringLocalizer<Resources> Localizer { get; set; } = null!;
        [Inject] private IPalletService PalletService { get; set; } = null!;
    
        [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;
        [SupplyParameterFromForm] private PalletCreateModel FormModel { get; set; } = new();

        private IEnumerable<PluNestingEntity> PluNestings { get; set; } = [];

        private IEnumerable<PluEntity> GetAllPlus() => LineService.GetLinePiecePlus(LineContext.Line);

        private void SetPluNestings()
        {
            if (FormModel.Plu == null) return;
            PluNestings = PluService.GetAllPluNestings(FormModel.Plu);
            FormModel.Nesting = PluNestings.FirstOrDefault(item => item.IsDefault);
        }

        private void HandleInvalidForm(EditContext context)
        {
            foreach (string msg in context.GetValidationMessages())
                ToastService.ShowError(msg);
        }

        private async Task OnSubmit()
        {
            PalletEntity palletEntity = new()
            {
                PalletMan = PalletContext.PalletMan,
                Weight = FormModel.PalletWeight,
                ProdDt = FormModel.CreateDt,
                Barcode = string.Empty
            };
            PalletService.Create(palletEntity);

            LabelPieceDto dto = new ()
            {
                ExpirationDt = FormModel.CreateDt.AddDays(FormModel.Plu!.ShelfLifeDays),
                Kneading = FormModel.Kneading,
                Line = LineContext.Line,
                Nesting = FormModel.Nesting!,
                ProductDt = FormModel.CreateDt,
                Template = PluService.GetPluTemplate(FormModel.Plu).Body
            };

            await Task.Run(() =>
            {
                PrintLabelService.GeneratePiecePallet(dto, palletEntity, FormModel.Count);
                PalletContext.UpdatePalletData();
            });
            
            await Dialog.CloseAsync();
        } 
    }

    public class PalletCreateModel
    {
        [Required(ErrorMessage = "Поле 'ПЛУ' обязательно для заполнения")]
        public PluEntity? Plu { get; set; }

        [Required(ErrorMessage = "Поле 'Вложенность' обязательно для заполнения")]
        public PluNestingEntity? Nesting { get; set; }

        [Range(1, 200, ErrorMessage = "Поле 'Количество' не может быть меньше 1 и больше 200")]
        public int Count { get; set; } = 1;

        [Range(0, double.MaxValue, ErrorMessage = "Поле 'Вес паллеты' не может быть меньше 0")]
        public decimal PalletWeight { get; set; }

        [Range(1, short.MaxValue, ErrorMessage = "Поле 'Вес паллеты' не может быть меньше 0")]
        public short Kneading { get; set; } = 1;

        public DateTime CreateDt { get; set; } = DateTime.Now;
    }
}