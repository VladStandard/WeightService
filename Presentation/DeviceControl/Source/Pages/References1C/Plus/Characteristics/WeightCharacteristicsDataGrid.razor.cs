using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.References1C.Plus.Characteristics;

public sealed partial class WeightCharacteristicsDataGrid : SectionDataGridPageBase<CharacteristicEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;

    # endregion

    [CascadingParameter(Name = "DialogItem")] public PluEntity Plu { get; set; } = null!;

    protected override IEnumerable<CharacteristicEntity> SetSqlSectionCast() => [Plu.Nesting.ToCharacteristic()];
}