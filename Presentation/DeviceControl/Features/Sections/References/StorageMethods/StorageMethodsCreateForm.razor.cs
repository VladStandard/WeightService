using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.StorageMethods;

namespace DeviceControl.Features.Sections.References.StorageMethods;

public sealed partial class StorageMethodsCreateForm : SectionFormBase<SqlStorageMethodEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
}