// ReSharper disable ClassNeverInstantiated.Global
using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Shared.Enums;

namespace DeviceControl.Features.Sections.References1C.Plus;

public sealed partial class PlusUpdateDialog: SectionDialogBase<PluEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    protected override List<EnumTypeModel<string>> InitializeTabList() =>
    [
        new(Localizer["SectionPLU"], "main"),
        new(Localizer["SectionPluNestings"], "nesting")
    ];
}