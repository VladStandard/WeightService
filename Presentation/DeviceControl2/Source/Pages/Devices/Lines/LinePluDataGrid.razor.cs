using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.Plu;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.Devices.Lines;

public sealed partial class LinePluDataGrid : SectionDataGridPageBase<PluLineEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private ILineService LineService { get; set; } = default!;
    [Inject] private IPluService PluService { get; set; } = default!;

    # endregion

    [CascadingParameter(Name = "DialogItem")] public LineEntity LineEntity { get; set; } = null!;

    private HashSet<PluEntity> SelectPluEntities { get; set; } = [];
    private HashSet<PluEntity> SelectedPluEntities { get; set; } = [];
    private HashSet<PluEntity> SelectedPluEntitiesCopy { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        SelectPluEntities = [..PluService.GetAll()];
        SelectedPluEntities = [..LineService.GetLinePlus(LineEntity)];
        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private async Task SaveSelectedPluEntities()
    {
        foreach (PluEntity itemToDelete in SelectedPluEntitiesCopy.Except(SelectedPluEntities))
        {
            PluLineEntity? pluLineItem = SectionItems.SingleOrDefault(i => i.Plu.Equals(itemToDelete));
            if (pluLineItem != null) LineService.DeletePluLine(pluLineItem);
        }

        foreach (PluEntity pluEntity in SelectedPluEntities.Except(SelectedPluEntitiesCopy))
        {
            PluLineEntity pluLine = new() { Line = LineEntity, Plu = pluEntity };
            LineService.AddPluLine(pluLine);
        }

        await UpdateData();

        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private void ResetSelectedPluEntities() => SelectedPluEntities = SelectedPluEntitiesCopy.DeepClone();

    protected override IEnumerable<PluLineEntity> SetSqlSectionCast() =>
        LineService.GetLinePlusFk(LineEntity);

    protected override async Task OpenItemInNewTab(PluLineEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Plu.Uid}");
}