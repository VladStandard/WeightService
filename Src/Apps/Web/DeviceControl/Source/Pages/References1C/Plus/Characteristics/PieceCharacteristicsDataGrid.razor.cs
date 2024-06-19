using Ws.Domain.Models.Entities.Ref1c.Plu;

namespace DeviceControl.Source.Pages.References1C.Plus.Characteristics;

public sealed partial class PieceCharacteristicsDataGrid : SectionDataGridPageBase<PluCharacteristic>
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;

    # endregion

    [CascadingParameter(Name = "DialogItem")] public Plu Plu { get; set; } = null!;

    protected override IEnumerable<PluCharacteristic> SetSqlSectionCast() => Plu.CharacteristicsWithNesting;
}