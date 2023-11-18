using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;

namespace ScalesHybrid.Components.Controls;

public sealed partial class IndexControlBar : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private List<ControlBarButton> PluConfigButtonList { get; set; }
    private List<ControlBarButton> PluPrintButtonList { get; set; }
    

    protected override void OnInitialized()
    {
        PluConfigButtonList = new List<ControlBarButton>
        {
            new() { Title = Localizer["ButtonLineChange"] },
            new() { Title = Localizer["ButtonPLUChange"] },
            new() { Title = Localizer["ButtonPLUNestingChange"] },
        };
        PluPrintButtonList = new List<ControlBarButton>
        {
            new() { Title = Localizer["ButtonKneadingChange"] },
            new() { Title = Localizer["ButtonLabelPrint"] },
            new() { Title = Localizer["ButtonScaleTerminal"], OnClickAction = OpenScalesTerminal},
        };
    }

    private static void OpenScalesTerminal()
    {
        try
        {
            Process process = new()
            {
                StartInfo = new ProcessStartInfo(
                    @"C:\Program Files (x86)\Massa-K\ScalesTerminal 100\ScalesTerminal.exe")
            };
            process.Start();
            process.WaitForExit();
        }
        catch
        {
            // TODO: Handle error
        }
    }
}

internal class ControlBarButton
{
    public string Title { get; init; } = string.Empty;
    public Action OnClickAction { get; init; }
}