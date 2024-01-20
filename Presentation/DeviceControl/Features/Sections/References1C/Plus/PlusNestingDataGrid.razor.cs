using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.StorageCore.Entities.Scales.PlusNestingFks;

namespace DeviceControl.Features.Sections.References1C.Plus;

public sealed partial class PlusNestingDataGrid : SectionDataGridBase<PluNestingEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    [Parameter, EditorRequired] public PluEntity PluEntity { get; set; } = null!;
    
    private SqlPluNestingFkRepository PluNestingFkRepository { get; } = new();

    protected override void SetSqlSectionCast() =>
        SectionItems = PluNestingFkRepository.GetEnumerableByPluUidActual(PluEntity).ToList();
}
