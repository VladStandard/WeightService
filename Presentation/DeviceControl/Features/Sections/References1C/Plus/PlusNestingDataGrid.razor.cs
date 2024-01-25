using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Plu;

namespace DeviceControl.Features.Sections.References1C.Plus;

public sealed partial class PlusNestingDataGrid : SectionDataGridBase<PluNestingEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IPluService PluService { get; set; } = null!;

    #endregion
    
    [Parameter, EditorRequired] public PluEntity PluEntity { get; set; } = null!;

    protected override void SetSqlSectionCast() =>
        SectionItems = PluService.GetAllPluNestings(PluEntity);
}
