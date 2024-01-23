using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.Plu;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinePluDataGrid: SectionDataGridBase<PluLineEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private ILineService LineService { get; set; } = null!;
    [Inject] private IPluService PluService { get; set; } = null!;

    #endregion
    
    [Parameter, EditorRequired] public LineEntity LineEntity { get; set; } = null!;

    private IEnumerable<PluEntity> SelectPluEntities { get; set; } = [];
    private IEnumerable<PluEntity> SelectedPluEntities { get; set; } = [];
    private IEnumerable<PluEntity> SelectedPluEntitiesCopy { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        SelectPluEntities = PluService.GetAllNotGroup();
        SelectedPluEntities = LineService.GetLinePlus(LineEntity);
        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }
    
    private async Task SaveSelectedPluEntities()
    {
        foreach (PluEntity itemToDelete in SelectedPluEntitiesCopy.Except(SelectedPluEntities))
        {
            PluLineEntity? pluLineItem = SectionItems.SingleOrDefault(i => i.Plu.Equals(itemToDelete));
            if (pluLineItem != null) SqlCoreHelper.Instance.Delete(pluLineItem);
        }
        
        foreach (PluEntity pluEntity in SelectedPluEntities.Except(SelectedPluEntitiesCopy))
        {
            PluLineEntity pluLine = new() { Line = LineEntity, Plu = pluEntity };
            SqlCoreHelper.Instance.SaveOrUpdate(pluLine);
        }

        await DataGridWrapperRef.ReloadData();
        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private void ResetSelectedPluEntities() => SelectedPluEntities = SelectedPluEntitiesCopy.DeepClone();
    

    protected override void SetSqlSectionCast() =>
        SectionItems = LineService.GetLinePlusFk(LineEntity);
    
    protected override async Task OpenItemInNewTab(PluLineEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Plu.IdentityValueUid}");
}