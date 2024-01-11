using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;

namespace DeviceControl.Features.Sections.References1C.Plus;

public sealed partial class PlusNestingDataGrid : SectionDataGridBase<SqlPluNestingFkEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    [Parameter, EditorRequired] public SqlPluEntity PluEntity { get; set; } = null!;
    
    private SqlPluNestingFkRepository PluNestingFkRepository { get; } = new();

    protected override void SetSqlSectionCast() =>
        SectionItems = PluNestingFkRepository.GetEnumerableByPluUidActual(PluEntity).ToList();
}
