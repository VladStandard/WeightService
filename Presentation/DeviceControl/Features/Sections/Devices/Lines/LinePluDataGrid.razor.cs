using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Services.Services.Line;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.PlusLines;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinePluDataGrid: SectionDataGridBase<SqlPluLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private ILineService LineService { get; set; } = null!;

    [Parameter, EditorRequired] public SqlLineEntity LineEntity { get; set; } = null!;

    private IEnumerable<SqlPluEntity> SelectPluEntities { get; set; } = [];
    private IEnumerable<SqlPluEntity> SelectedPluEntities { get; set; } = [];
    private IEnumerable<SqlPluEntity> SelectedPluEntitiesCopy { get; set; } = [];
    
    private SqlPluLineRepository PluLineRepository { get; } = new();
    private SqlPluRepository PluRepository { get; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        SelectPluEntities = PluRepository.GetEnumerableNotGroup(new());
        SelectedPluEntities = LineService.GetLinePlus(LineEntity);
        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }
    
    private async Task SaveSelectedPluEntities()
    {
        foreach (SqlPluEntity itemToDelete in SelectedPluEntitiesCopy.Except(SelectedPluEntities))
        {
            SqlPluLineEntity? pluLineItem = SectionItems.SingleOrDefault(i => i.Plu.Equals(itemToDelete));
            if (pluLineItem != null) SqlCoreHelper.Instance.Delete(pluLineItem);
        }
        
        foreach (SqlPluEntity pluEntity in SelectedPluEntities.Except(SelectedPluEntitiesCopy))
        {
            SqlPluLineEntity pluLine = new() { Line = LineEntity, Plu = pluEntity};
            SqlCoreHelper.Instance.SaveOrUpdate(pluLine);
        }

        await DataGridWrapperRef.ReloadData();
        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private void ResetSelectedPluEntities() => SelectedPluEntities = SelectedPluEntitiesCopy.DeepClone();
    

    protected override void SetSqlSectionCast() =>
        SectionItems = PluLineRepository.GetListByLine(LineEntity, SqlCrudConfigSection);
    
    protected override async Task OpenItemInNewTab(SqlPluLineEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Plu.IdentityValueUid}");
}