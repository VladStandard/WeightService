using DeviceControl.Features.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;

namespace DeviceControl.Features.Sections.References.PluStorages;

public sealed partial class PluStoragesUpdateDialog : SectionDialogBase<SqlPluStorageMethodEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        TabsList = new()
        {
            new(Localizer["SectionPluStorages"], "main"),
        };
    }
}