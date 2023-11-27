using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;
using ScalesHybrid.Resources;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogCalculator: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    [Inject] private DialogService DialogService { get; set; }
    [Parameter] public Action<int> CallbackFunction { get; set; }
    [Parameter] public string Number { get; set; } = string.Empty;
    [Parameter] public int MaxDigitCount { get; set; } = 3;

    private List<CalculatorControl> CalculatorControls { get; set; } = new();

    protected override void OnInitialized()
    {
        CalculatorControls = new()
        {
            new() { Title = "1", CalculatorAction = () => SetNumber(1) },
            new() { Title = "2", CalculatorAction = () => SetNumber(2) },
            new() { Title = "3", CalculatorAction = () => SetNumber(3) },
            new() { Title = "4", CalculatorAction = () => SetNumber(4) },
            new() { Title = "5", CalculatorAction = () => SetNumber(5) },
            new() { Title = "6", CalculatorAction = () => SetNumber(6) },
            new() { Title = "7", CalculatorAction = () => SetNumber(7) },
            new() { Title = "8", CalculatorAction = () => SetNumber(8) },
            new() { Title = "9", CalculatorAction = () => SetNumber(9) },
            new() { Title = "C", CalculatorAction = ClearNumber },
            new() { Title = "0", CalculatorAction = () => SetNumber(0) },
            new() { Title = Localizer["ButtonCalculatorEnter"], CalculatorAction = SubmitInput },
        };
    }

    private void SubmitInput()
    {
        int.TryParse(Number, out int resultInt);
        CallbackFunction.Invoke(resultInt);
        DialogService.Close();
    }

    private void SetNumber(int newDigit)
    {
        if (Number.Length >= MaxDigitCount) return;
        Number += newDigit;
        StateHasChanged();
    }
    
    private void ClearNumber()
    {
        Number = string.Empty;
        StateHasChanged();
    }
}

internal class CalculatorControl
{
    public string Title { get; set; } = string.Empty;
    public Action CalculatorAction { get; set; }
}