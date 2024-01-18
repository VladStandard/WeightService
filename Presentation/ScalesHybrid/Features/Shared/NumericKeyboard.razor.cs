using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;

namespace ScalesHybrid.Features.Shared;

public sealed partial class NumericKeyboard: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;

    [Parameter] public Action<int> CallbackFunction { get; set; } = _ => { };
    [Parameter] public string Number { get; set; } = string.Empty;
    [Parameter] public int MaxDigitCount { get; set; } = 3;
    
    private List<CalculatorControl> CalculatorControls { get; set; } = [];

    protected override void OnInitialized()
    {
        CalculatorControls =
        [
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
            new() { Title = Localizer["ButtonCalculatorEnter"], CalculatorAction = SubmitInput }
        ];
    }

    private void SubmitInput()
    {
        int.TryParse(Number, out int resultInt);
        CallbackFunction.Invoke(int.Max(resultInt, 1));
        ModalService.Hide();
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

    private void ClearLastNumber()
    {
        if (string.IsNullOrEmpty(Number)) return;
        Number = Number[..^1];
        StateHasChanged();
    }
}

internal class CalculatorControl
{
    public string Title { get; init; } = string.Empty;
    public Action CalculatorAction { get; init; } = () => { };
}