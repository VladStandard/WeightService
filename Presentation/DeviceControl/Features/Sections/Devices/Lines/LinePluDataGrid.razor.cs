using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.PlusLines;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinePluDataGrid: SectionDataGridBase<SqlPluLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    [Parameter, EditorRequired] public SqlLineEntity LineEntity { get; set; } = null!;

    private IEnumerable<SqlPluEntity> PluEntities { get; set; } = [];
    private IEnumerable<SqlPluEntity> SelectedPluEntities { get; set; } = [];
    
    private SqlPluLineRepository PluLineRepository { get; } = new();

    protected override void SetSqlSectionCast() =>
        SectionItems = PluLineRepository.GetListByLine(LineEntity, SqlCrudConfigSection);
    
    protected override async Task OpenItemInNewTab(SqlPluLineEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Plu.IdentityValueUid}");
}