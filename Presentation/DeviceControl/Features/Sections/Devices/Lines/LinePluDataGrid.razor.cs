using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinePluDataGrid: SectionDataGridBase<SqlPluScaleEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    [Parameter, EditorRequired] public SqlLineEntity LineEntity { get; set; } = null!;
    
    private SqlPluLineRepository PluLineRepository { get; } = new();

    protected override void SetSqlSectionCast()
    {
        SectionItems = PluLineRepository.GetListByLine(LineEntity, SqlCrudConfigSection);
    }
        
}