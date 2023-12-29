using DeviceControl.Features.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;

namespace DeviceControl.Features.Sections.References.PluStorages;

public sealed partial class PluStoragesUpdateForm : SectionFormBase<SqlPluStorageMethodEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
}