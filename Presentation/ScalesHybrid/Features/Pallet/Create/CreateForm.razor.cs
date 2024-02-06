using Blazorise;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;

namespace ScalesHybrid.Features.Pallet.Create;

public sealed partial class CreateForm: ComponentBase
{
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    [Inject] private LineContext LineContext { get; set; } = null!;
    
    [SupplyParameterFromForm]
    private PalletCreateModel FormModel { get; set; } = new();

    private void OnSubmit() => NotificationService.Info($"{FormModel.Nesting} {FormModel.Plu}" +
                                                        $" {FormModel.CreateDt} {FormModel.Count} {FormModel.PalletWeight}");
}

public class PalletCreateModel
{
    public string Plu { get; set; } = string.Empty;
    public string Nesting { get; set; } = string.Empty;
    public DateOnly CreateDt { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public decimal PalletWeight { get; set; }
    public int Count { get; set; }
}