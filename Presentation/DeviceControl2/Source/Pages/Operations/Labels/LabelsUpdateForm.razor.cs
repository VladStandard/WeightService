using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Plu;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.Operations.Labels;

public sealed partial class LabelsUpdateForm: SectionFormBase<LabelEntity>
{
    # region Injects

    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPluService PluService { get; set; } = default!;

    # endregion
    private TemplateEntity Template { get; set; } = new();

    protected override void OnInitialized()
    {
        Template = PluService.GetPluTemplate(DialogItem.Plu);
        base.OnInitialized();
    }

    private string GetPluTypeName(bool isWeight) =>
        isWeight ? WsDataLocalizer["ColPluWeight"] : WsDataLocalizer["ColPluPiece"];
}
