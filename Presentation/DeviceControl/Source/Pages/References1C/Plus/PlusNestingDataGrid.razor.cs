using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Plu;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.References1C.Plus;

public sealed partial class PlusNestingDataGrid : SectionDataGridPageBase<PluNestingEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPluService PluService { get; set; } = default!;

    # endregion

    [CascadingParameter(Name = "DialogItem")] public PluEntity PluEntity { get; set; } = null!;

    protected override IEnumerable<PluNestingEntity> SetSqlSectionCast() =>
        SectionItems = PluService.GetAllPluNestings(PluEntity);
}