using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelDisplayDate : ComponentBase
{
    [Inject] private IStringLocalizer<WsDataResources> Localizer { get; set; } = null!;

    [Inject] private LabelContext LabelContext { get; set; } = null!;

    private void IncreaseDate() =>
        LabelContext.KneadingModel.ProductDate = LabelContext.KneadingModel.ProductDate.AddDays(1);

    private void DecreaseDate() =>
        LabelContext.KneadingModel.ProductDate = LabelContext.KneadingModel.ProductDate.AddDays(-1);

    private void ResetDate() =>
        LabelContext.KneadingModel.ProductDate = DateTime.Now;
}