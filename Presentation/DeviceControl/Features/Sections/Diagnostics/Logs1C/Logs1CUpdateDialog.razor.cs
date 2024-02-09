// ReSharper disable ClassNeverInstantiated.Global
using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Diag;
using Ws.Shared.Enums;

namespace DeviceControl.Features.Sections.Diagnostics.Logs1C;

public sealed partial class Logs1CUpdateDialog : SectionDialogBase<LogWebEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    protected override List<EnumTypeModel<string>> InitializeTabList() =>
        [new(Localizer["Section1CLogs"], "main")];
}