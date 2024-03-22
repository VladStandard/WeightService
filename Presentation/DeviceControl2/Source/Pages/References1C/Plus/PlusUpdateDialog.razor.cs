using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Shared.Enums;

namespace DeviceControl2.Source.Pages.References1C.Plus;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class PlusUpdateDialog : SectionDialogBase<PluEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    protected override List<EnumTypeModel<string>> InitializeTabList() =>
    [
        new(Localizer["SectionPLU"], "main"),
        new(Localizer["SectionPluNestings"], "nesting")
    ];
}