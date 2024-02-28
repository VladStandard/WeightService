using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.StorageMethod;

namespace DeviceControl.Features.Sections.References.StorageMethods;

public sealed partial class StorageMethodsUpdateForm : SectionFormBase<StorageMethodEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IStorageMethodService StorageMethodService { get; set; } = null!;
}