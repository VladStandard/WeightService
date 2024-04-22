using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelDisplayDate : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> Localizer { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;

    # endregion

    private void IncreaseDate() =>
        LabelContext.KneadingModel.ProductDate = LabelContext.KneadingModel.ProductDate.AddDays(1);

    private void DecreaseDate() =>
        LabelContext.KneadingModel.ProductDate = LabelContext.KneadingModel.ProductDate.AddDays(-1);

    private void ResetDate() =>
        LabelContext.KneadingModel.ProductDate = DateTime.Now;
}