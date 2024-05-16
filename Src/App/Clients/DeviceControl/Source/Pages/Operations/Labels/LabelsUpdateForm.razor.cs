using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Plus;

namespace DeviceControl.Source.Pages.Operations.Labels;

public sealed partial class LabelsUpdateForm : SectionFormBase<Label>
{
    # region Injects

    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;

    [Inject] private IPluService PluService { get; set; } = default!;

    # endregion
    private Template Template { get; set; } = new();

    private string GetPluTypeName(bool isWeight) =>
        isWeight ? WsDataLocalizer["ColPluWeight"] : WsDataLocalizer["ColPluPiece"];
}