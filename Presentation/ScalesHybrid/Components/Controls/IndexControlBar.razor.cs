using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Utils;

namespace ScalesHybrid.Components.Controls;

public sealed partial class IndexControlBar : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    private List<ControlBarButton> PluConfigButtonList { get; set; }
    private List<ControlBarButton> PluPrintButtonList { get; set; }

    protected override void OnInitialized()
    {
        PluConfigButtonList = new()
        {
            new() { Title = Localizer["ButtonLineChange"] },
            new() { Title = Localizer["ButtonPLUChange"], OnClickAction = () => RedirectTo(RouterUtils.PluSelect)},
            new() { Title = Localizer["ButtonPLUNestingChange"] },
        };
        PluPrintButtonList = new()
        {
            new() { Title = Localizer["ButtonKneadingChange"] },
            new() { Title = Localizer["ButtonLabelPrint"] },
            new() { Title = Localizer["ButtonScaleTerminal"], OnClickAction = OpenScalesTerminal},
        };
    }

    private void RedirectTo(string url) => NavigationManager.NavigateTo(url);

    private static void OpenScalesTerminal()
    {
        try
        {
            Process process = new()
            {
                StartInfo = new(@"C:\Program Files (x86)\Massa-K\ScalesTerminal 100\ScalesTerminal.exe")
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