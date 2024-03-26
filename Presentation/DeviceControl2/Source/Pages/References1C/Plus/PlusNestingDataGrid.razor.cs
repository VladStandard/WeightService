using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Plu;

namespace DeviceControl2.Source.Pages.References1C.Plus;

public sealed partial class PlusNestingDataGrid : SectionDataGridPageBase<PluNestingEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IPluService PluService { get; set; } = null!;

    [CascadingParameter(Name = "DialogItem")] public PluEntity PluEntity { get; set; } = null!;

    protected override IEnumerable<PluNestingEntity> SetSqlSectionCast() =>
        SectionItems = PluService.GetAllPluNestings(PluEntity);
}