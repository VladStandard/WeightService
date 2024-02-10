using System.ComponentModel.DataAnnotations;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ScalesDesktop.Services;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Plu;

namespace ScalesDesktop.Features.Pallet.Create;

public sealed partial class CreateForm : ComponentBase
{
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    [Inject] private LineContext LineContext { get; set; } = null!;
    [Inject] private IPluService PluService { get; set; } = null!;

    [SupplyParameterFromForm] private PalletCreateModel FormModel { get; set; } = new();

    private IEnumerable<PluNestingEntity> PluNestings { get; set; } = [];

    private IEnumerable<PluEntity> GetAllPlus() => PluService.GetAll();

    private void SetPluNestings()
    {
        if (FormModel.Plu == null) return;
        PluNestings = PluService.GetAllPluNestings(FormModel.Plu);
        FormModel.Nesting = PluNestings.FirstOrDefault(item => item.IsDefault);
    }

    private void HandleInvalidForm(EditContext context)
    {
        foreach (string msg in context.GetValidationMessages())
            NotificationService.Error(msg);
    }

    private void OnSubmit()
    {
        string msg = $"{FormModel.Nesting} {FormModel.Plu} {FormModel.CreateDt} {FormModel.Count} " +
                     $"{FormModel.PalletWeight}";
        NotificationService.Info(msg);
    } 
}

public class PalletCreateModel
{
    [Required(ErrorMessage = "Поле 'ПЛУ' обязательно для заполнения")]
    public PluEntity? Plu { get; set; }

    [Required(ErrorMessage = "Поле 'Вложенность' обязательно для заполнения")]
    public PluNestingEntity? Nesting { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Поле 'Количество' не может быть меньше 1")]
    public int Count { get; set; } = 1;

    [Range(0, double.MaxValue, ErrorMessage = "Поле 'Вес паллеты' не может быть меньше 0")]
    public decimal PalletWeight { get; set; }

    public DateOnly CreateDt { get; set; } = DateOnly.FromDateTime(DateTime.Today);
}