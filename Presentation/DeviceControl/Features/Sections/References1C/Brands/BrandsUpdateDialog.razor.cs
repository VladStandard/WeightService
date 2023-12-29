using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Shared.Enums;
using Ws.StorageCore.Entities.SchemaRef1c.Brands;

namespace DeviceControl.Features.Sections.References1C.Brands;


public sealed partial class BrandsUpdateDialog: SectionDialogBase<SqlBrandEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override List<EnumTypeModel<string>> InitializeTabList() =>
        new() { new(Localizer["SectionBrands"], "main") };
}
